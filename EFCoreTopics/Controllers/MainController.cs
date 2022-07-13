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

        [HttpPost("AddSpecialProducts")]
        public async Task<IActionResult> AddSpecialProducts()
        {
            var specialProduct = new SpecialProduct()
            {
                Name = "SomeProduct",
            };

            await _db.AddAsync(specialProduct);

            var specialProductPrice = new SpecialProductPrice()
            {
                PriceDate = DateTime.Now,
                SpecialProductId = specialProduct.Id,
                Price = 2000,
            };

            _db.Add(specialProductPrice);

            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
