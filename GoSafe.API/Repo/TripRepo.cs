using GoSafe.API.Dtos.Trip;
using GoSafe.API.Interfaces;
using GoSafe.API.Models;
using GoSafe.API.Utility;
using GoSafe.Dto.Common;
using Microsoft.EntityFrameworkCore;

namespace GoSafe.API.Repo
{
    public class TripRepo : ITripRepo
    {
        private readonly AppDbContext context;

        public TripRepo(AppDbContext context)
        {
            this.context = context;
        }

        #region Bus

        public async Task<CommonResult> SaveBus(SaveBusRequest req, long loginUserId)
        {
            try
            {
                var res = new CommonResult();

                var isInsert = req.Id == 0;

                var duplicate = await context.TblBuses
                    .AnyAsync(x =>
                        x.PlateNo == req.PlateNo &&
                        (isInsert || x.Id != req.Id));

                if (duplicate)
                    throw new AppException("Plate No already exists.");

                TblBus? bus = isInsert
                    ? new TblBus()
                    : await context.TblBuses
                        .SingleOrDefaultAsync(x => x.Id == req.Id);

                if (!isInsert && bus == null)
                    throw new AppException("Bus not found.");

                bus!.Name = req.Name;
                bus.PlateNo = req.PlateNo;
                bus.Description = req.Description;

                if (isInsert)
                {
                    bus.CreatedAt = DateTime.UtcNow;
                    bus.CreatedBy = loginUserId;

                    context.TblBuses.Add(bus);
                }
                else
                {
                    bus.UpdatedAt = DateTime.UtcNow;
                    bus.UpdatedBy = loginUserId;

                    //context.TblBuses.Update(bus);
                }

                await context.SaveChangesAsync();

                return res;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CommonResult> DeleteBus(long Id, long loginUserId)
        {
            try
            {
                var res = new CommonResult();

                var bus = await context.TblBuses
                    .SingleOrDefaultAsync(x => x.Id == Id);

                if (bus == null)
                    throw new AppException("Bus not found.");

                context.TblBuses.Remove(bus);

                await context.SaveChangesAsync();

                return res;
            }
            catch
            {
                throw;
            }
        }

        public async Task<GetBusListResponse> GetBusList(GetBusListRequest req)
        {
            try
            {
                var res = new GetBusListResponse();

                var query =
                    from b in context.TblBuses
                    where
                        (string.IsNullOrEmpty(req.Name) || b.Name.Contains(req.Name))
                        && (string.IsNullOrEmpty(req.PlateNo) || b.PlateNo.Contains(req.PlateNo))
                    select b;

                res.TotalItem = await query.CountAsync();

                TimeZoneInfo myanmarTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");

                res.Items = await query
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip(req.PageNumber * req.PageSize)
                    .Take(req.PageSize)
                    .Select(x => new BusModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        PlateNo = x.PlateNo,
                        Description = x.Description,
                        CreatedAt = DateTimeTool.ConvertIntoMyanTime(x.CreatedAt),
                        UpdatedAt = x.UpdatedAt == null ? null : DateTimeTool.ConvertIntoMyanTime(x.UpdatedAt.Value),
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
