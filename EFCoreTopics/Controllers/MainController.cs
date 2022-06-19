using EFCoreTopics.Database.Data;
using EFCoreTopics.Database.Models;
using EFCoreTopics.Database.ValueObjects;
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

        #region Conversion
        [HttpPost("AddProductPrice")]
        public async Task<IActionResult> AddProduct(string name, decimal price, MoneyType moneyType)
        {
            var product = new ProductPrice(Guid.NewGuid(), name, DateTime.Now, new Money(price, moneyType));

            _db.ProductPrices.Add(product);

            await _db.SaveChangesAsync();

            return Ok(product.Id);
        }

        [HttpPost("GetProductPrice")]
        public async Task<IActionResult> GetProductByPriceType(MoneyType money)
        {
            var products = await _db.ProductPrices.Where(c => c.Money.Unit == money).ToListAsync();

            return Ok(products);
        }

        #endregion
    }
}
