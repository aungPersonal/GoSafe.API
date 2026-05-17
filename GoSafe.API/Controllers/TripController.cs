using GoSafe.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoSafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TripController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateTrip([FromBody] tblRouteTemplate trip)
        {
            if (trip == null)
            {
                return BadRequest("Trip data is null.");
            }
            // You can add additional validation here (e.g., check for required fields)
            _context.tblRouteTemplates.Add(trip);
            _context.SaveChanges();
            return Ok(trip.Id);
        }

        [HttpGet]
        public IActionResult GetAllTrips()
        {
            var trips = _context.tblRouteTemplates.ToList();
            return Ok(trips);
        }
    }
}
