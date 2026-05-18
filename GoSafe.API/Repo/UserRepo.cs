using GoSafe.API.Common;
using GoSafe.API.Interfaces;
using GoSafe.API.Models;
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

        public async Task<RegisterResponse> Register(RegisterRequest req)
        {
            try
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var res = new RegisterResponse();

                        var isDuplicate = await context.tblUsers.AnyAsync(x => x.IsDeleted == false && x.LoginName == req.LoginName);
                        if (isDuplicate)
                        {
                            res.Result.StatusCode = StatusCodes.Status400BadRequest;
                            res.Result.AddErrorMessage("Login name is already exist.");
                            return res;
                        }
                        var vCode = PasswordHelper.GenerateSalt(CommonConstants.PASSWORD_SALT_LENGTH);
                        var encodedPass = PasswordHelper.EncodePassword(req.Password, vCode);
                        var account = new tblUser()
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

                        await context.tblUsers.AddAsync(account);
                        await context.SaveChangesAsync();

                        DateTime expirationDateTime = DateTime.UtcNow;

                        var token = GenerateToken(account, out expirationDateTime);

                        var tokenEntity = new tblToken()
                        {
                            Id = Guid.NewGuid().ToString(),
                            AccountId = account.Id,
                            AccessToken = token.AccessToken,
                            RefreshToken = token.RefreshToken,
                            ExpirationTime = expirationDateTime,
                            CreatedDateTime = DateTime.UtcNow
                        };
                        await context.tblTokens.AddAsync(tokenEntity);
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

        private TokenRes GenerateToken(tblUser account, out DateTime expirationDateTime)
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

    }
}
