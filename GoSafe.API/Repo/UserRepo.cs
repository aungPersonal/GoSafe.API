using GoSafe.API.Common;
using GoSafe.API.Interfaces;
using GoSafe.API.Models;
using GoSafe.API.Utility;
using GoSafe.Dto.Common;
using GoSafe.Dto.User;
using GoSafe.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace GoSafe.API.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext context;

        public UserRepo(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<LoginResponse> Login(LoginRequest req)
        {
            try
            {
                var res = new LoginResponse();
                DateTime expirationDateTime = DateTime.UtcNow;
                var account = await context.TblUsers.Where(x => x.LoginName == req.LoginName && x.IsDeleted == false && x.IsDeleted == false).SingleOrDefaultAsync();
                if (account == null)
                {
                    res.Result.StatusCode = StatusCodes.Status401Unauthorized;
                    res.Result.AddErrorMessage("Login name or password is wrong!");
                    return res;
                }
                else
                {
                    var encodedPass = PasswordHelper.EncodePassword(req.Password, account.VCode);
                    if (account.PasswordHash != encodedPass)
                    {
                        res.Result.StatusCode = StatusCodes.Status401Unauthorized;
                        res.Result.AddErrorMessage("Login name or password is wrong!");
                        return res;
                    }
                }

                var token = GenerateToken(account, out expirationDateTime);

                await UpdateToken(token, expirationDateTime, account.Id);

                res.Token = token;
                res.Token.FullName = account.FullName;
                res.Token.Id = account.Id;
                res.Token.Role = ((RoleEnum)account.RoleId!.Value)!.ToString();
                res.Token.ExpirationDateTime = DateTimeTool.ConvertIntoMyanTime(expirationDateTime);
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task UpdateToken(TokenRes token, DateTime expirationDateTime, long accountId)
        {
            var tokenEntity = new TblToken()
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = accountId,
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                ExpirationTime = expirationDateTime,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = accountId,
            };
            context.TblTokens.Add(tokenEntity);
            await context.SaveChangesAsync();
        }

        public async Task<RegisterResponse> Register(RegisterRequest req)
        {
            try
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var res = new RegisterResponse();

                        var isDuplicate = await context.TblUsers.AnyAsync(x => x.IsDeleted == false && x.LoginName == req.LoginName);
                        if (isDuplicate)
                        {
                            res.Result.StatusCode = StatusCodes.Status400BadRequest;
                            res.Result.AddErrorMessage("Login name is already exist.");
                            return res;
                        }
                        var vCode = PasswordHelper.GenerateSalt(CommonConstants.PASSWORD_SALT_LENGTH);
                        var encodedPass = PasswordHelper.EncodePassword(req.Password, vCode);
                        var account = new TblUser()
                        {
                            LoginName = req.LoginName,
                            VCode = vCode,
                            PasswordHash = encodedPass,
                            FullName = req.FullName,
                            CreatedAt = DateTime.UtcNow,
                            IsDeleted = false,
                            RoleId = (int)RoleEnum.Admin,
                            CreatedBy = 0
                        };

                        await context.TblUsers.AddAsync(account);
                        await context.SaveChangesAsync();

                        DateTime expirationDateTime = DateTime.UtcNow;

                        var token = GenerateToken(account, out expirationDateTime);

                        var tokenEntity = new TblToken()
                        {
                            Id = Guid.NewGuid().ToString(),
                            AccountId = account.Id,
                            AccessToken = token.AccessToken,
                            RefreshToken = token.RefreshToken,
                            ExpirationTime = expirationDateTime,
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = account.Id
                        };
                        await context.TblTokens.AddAsync(tokenEntity);
                        await context.SaveChangesAsync();
                        await tran.CommitAsync();

                        res.Token = token;
                        return res;
                    }
                    catch (Exception)
                    {
                        await tran.RollbackAsync();
                        throw;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private TokenRes GenerateToken(TblUser account, out DateTime expirationDateTime)
        {

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Id.ToString()),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = CreateToken(authClaims, out expirationDateTime);
            var refreshToken = GenerateRefreshToken();
            var res = new TokenRes
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                ExpirationDateTime = expirationDateTime,
            };
            return res;
        }

        private string CreateToken(List<Claim> authClaims, out DateTime expirationDateTime)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CommonConstants.TokenSecretKey));
            expirationDateTime = DateTime.UtcNow.AddMinutes(CommonConstants.TokenValidationInMinutes);
            var token = new JwtSecurityToken(
                issuer: CommonConstants.TokenIssuer,
                audience: CommonConstants.TokenValidAudience,
                expires: expirationDateTime,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest req)
        {
            try
            {
                var res = new RefreshTokenResponse();
                var dbtoken = await context.TblTokens.Where(x => x.AccessToken == req.AccessToken && x.RefreshToken == req.RefreshToken).SingleOrDefaultAsync();
                if (dbtoken is null)
                {
                    res.Result.StatusCode = StatusCodes.Status401Unauthorized;
                    res.Result.AddErrorMessage("Failed to refresh token, please login again!");
                    return res;
                }

                var account = await context.TblUsers.Where(x => x.Id == dbtoken.AccountId && x.IsDeleted == false).SingleOrDefaultAsync();
                if (account is null)
                {
                    res.Result.StatusCode = StatusCodes.Status401Unauthorized;
                    res.Result.AddErrorMessage("Failed to refresh token, please login again!");
                    return res;
                }

                context.TblTokens.Remove(dbtoken);
                await context.SaveChangesAsync();

                var expirationDateTime = DateTime.UtcNow;
                var token = GenerateToken(account, out expirationDateTime);

                await UpdateToken(token, expirationDateTime, account.Id);

                res.Token = token;
                res.Token.FullName = account.FullName;
                res.Token.Id = account.Id;
                res.Token.Role = ((RoleEnum)account.RoleId!.Value)!.ToString();
                res.Token.ExpirationDateTime = DateTimeTool.ConvertIntoMyanTime(expirationDateTime);
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }


        #region CRUD
        public async Task<CommonResult> SaveUser(SaveUserRequest req, long loginUserId)
        {
            try
            {
                var res = new CommonResult();

                var isInsert = req.Id == 0;

                if(isInsert && string.IsNullOrEmpty(req.Password))
                {
                    throw new AppException("Password is required for new user.");
                }

                var duplicate = await context.TblUsers.AnyAsync(x =>
                        x.LoginName == req.LoginName &&
                        (isInsert || x.Id != req.Id));

                if (duplicate)
                    throw new AppException("Login Name already exists.");

                TblUser? user = isInsert
                    ? new TblUser()
                    : await context.TblUsers.SingleOrDefaultAsync(x => x.Id == req.Id);

                if (!isInsert && user == null)
                    throw new AppException("User not found.");

                user!.LoginName = req.LoginName;
                user.FullName = req.FullName;
                user.Phone = req.Phone;
                user.RoleId = req.RoleId;

                #region password
                if (string.IsNullOrEmpty(req.Password) == false)
                {
                    var vCode = PasswordHelper.GenerateSalt(CommonConstants.PASSWORD_SALT_LENGTH);
                    var encodedPass = PasswordHelper.EncodePassword(req.Password, vCode);
                    user.VCode = vCode;
                    user.PasswordHash = encodedPass;
                }
               
                #endregion

                if (isInsert)
                {
                    user.CreatedAt = DateTime.UtcNow;
                    user.CreatedBy = loginUserId;

                    context.TblUsers.Add(user);
                }
                else
                {
                    user.UpdatedAt = DateTime.UtcNow;
                    user.UpdatedBy = loginUserId;

                    context.TblUsers.Update(user);
                }

                await context.SaveChangesAsync();

                return res;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CommonResult> DeleteUser(long Id, long loginUserId)
        {
            try
            {
                var res = new CommonResult();

                var user = await context.TblUsers
                    .SingleOrDefaultAsync(x => x.Id == Id);

                if (user == null)
                    throw new AppException("User not found.");

                context.TblUsers.Remove(user);

                await context.SaveChangesAsync();

                return res;
            }
            catch
            {
                throw;
            }
        }

        public async Task<GetUserListResponse> GetUserList(GetUserListRequest req)
        {
            try
            {
                if (req.RoleId == 0)
                    req.RoleId = null;

                var res = new GetUserListResponse();

                var query =
                    from u in context.TblUsers
                    where
                        (string.IsNullOrEmpty(req.LoginName) || u.LoginName.Contains(req.LoginName))
                        && (string.IsNullOrEmpty(req.FullName) || u.FullName.Contains(req.FullName))
                        && (string.IsNullOrEmpty(req.Phone) || u.Phone!.Contains(req.Phone))
                        && (req.RoleId == null || u.RoleId == req.RoleId)
                        && (
                            string.IsNullOrEmpty(req.Filter)
                            || u.LoginName.Contains(req.Filter)
                            || u.FullName.Contains(req.Filter)
                            || (u.Phone != null && u.Phone.Contains(req.Filter))
                        )
                    select u;

                res.TotalItem = await query.CountAsync();

                res.Items = await query
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip(req.PageNumber * req.PageSize)
                    .Take(req.PageSize)
                    .Select(x => new UserModel
                    {
                        Id = x.Id,
                        LoginName = x.LoginName,
                        FullName = x.FullName,
                        Phone = x.Phone,
                        RoleId = x.RoleId,
                        CreatedAt = x.CreatedAt
                    })
                    .ToListAsync();

                return res;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
