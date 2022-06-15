using EFCoreTopics.Database.Data;
using EFCoreTopics.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("UpdateCity")]
        public async Task<IActionResult> UpdateCity(int id, string city)
        {
            var result = await _db.UpdateCityAddressAsync(id, city);

            return Ok(result);
        }

        [HttpGet("GetCityAndProvince")]
        public async Task<IActionResult> GetCityAndProvince()
        {
            var result = await _db.GetCitiesAndProvinceFromAddressAsync();

            return Ok(result);
        }

        #endregion

        #region UpdateMechanism

        [HttpPost("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(int addressId)
        {
            var address = new Address()
            {
                AddressLine2 = "Update Address Line 2",
                City = $"Eslam Shahr",
                StateProvince = $"Tehran",
                CountryRegion = "Iran",
                PostalCode = "33136",
                AddressLine1 = "Update Address Line",
                AddressId = addressId,
                ModifiedDate = DateTime.Now,
                Rowguid = Guid.NewGuid()
            };

            _db.Addresses.Update(address);
            await _db.SaveChangesAsync();

            return Ok(address.AddressId);
        }

        #endregion
    }
}
