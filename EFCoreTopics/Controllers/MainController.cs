using EFCoreTopics.Database.Data;
using EFCoreTopics.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTopics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly AdventureWorksLContext _db;

        public MainController(AdventureWorksLContext db)
        {
            _db = db;
        }

        #region Raw Sql Queries

        [HttpGet("GetNonInterpolatedAddresses")]
        public async Task<IActionResult> GetNonInterpolatedAddresses(int id)
        {
            var result = await _db.GetAddressNonInterpolatedAsync(id);

            return Ok(result);
        }

        [HttpGet("GetInterpolatedAddresses")]
        public async Task<IActionResult> GetInterpolatedAddresses(int id)
        {
            var result = await _db.GetAddressInterpolatedAsync(id);

            return Ok(result);
        }

        #endregion

        #region Change Tracker Switch

        [HttpPost("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress()
        {
            _db.ChangeTracker.AutoDetectChangesEnabled = false;

            var addresses = await _db.Addresses.ToListAsync();

            foreach (var address in addresses)
            {
                if (address.AddressId % 2 == 0)
                {
                    _db.Entry(address).State = EntityState.Modified;
                    address.City = "Tehran";
                }

            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        #endregion
    }
}
