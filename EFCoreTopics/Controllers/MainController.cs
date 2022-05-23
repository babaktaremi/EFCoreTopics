using EFCoreTopics.Database.Data;
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

        #region New LINQ Features

        [HttpGet("GetProductWithMostWeight")]
        public  async Task<IActionResult> GetProductWithMaxWeight()
        {
            var products = await _db.Products.ToListAsync();

            var product = products.MaxBy(c => c.Weight);

            return Ok(product);
        }

        [HttpGet("GetProductWithMinWeight")]
        public async Task<IActionResult> GetProductWithMinWeight()
        {
            var products = await _db.Products.ToListAsync();

            var product = products.MinBy(c => c.Weight);

            return Ok(product);
        }

        [HttpGet("GetProductWithDistinctSize")]
        public async Task<IActionResult> GetProductWithDistinctSize()
        {
            var products =await  _db.Products.ToListAsync();

            var distinctProducts = products.DistinctBy(c => c.Size).ToList(); 

            return Ok(distinctProducts);
        }

        [HttpGet("GetRedAndHeavyProducts")]
        public async Task<IActionResult> GetBlueAndHeavyProducts()
        {
            var maximumWeight = await _db.Products.MaxAsync(c => c.Weight);
            var minimumWeight = await _db.Products.MinAsync(c => c.Weight);

            var heavyProducts = await _db.Products.Where(c => c.Weight < maximumWeight && c.Weight> minimumWeight).ToListAsync();

            var redProducts = await _db.Products.Where(c => c.Color.Equals("Red")).ToListAsync();

            var result = redProducts.IntersectBy(heavyProducts.Select(c=>c.ProductId),product => product.ProductId);
            return Ok(result);
        }

        [HttpGet("GetChunkProducts")]
        public async Task<IActionResult> GetChunkProducts(int chunk)
        {
            var products = await _db.Products.ToListAsync();

            var result = products.Chunk(chunk).ToList();
            return Ok(result);
        }

        [HttpGet("GetNthFromLastProduct")]
        public async Task<IActionResult> GetNthFromLastProduct(int count)
        {
            var products = await _db.Products.OrderBy(c=>c.ProductId).ToListAsync();

            var result = products.ElementAt(^count);

            return Ok(result);
        }

        [HttpGet("GetRangeOfProducts")]
        public async Task<IActionResult> GetRangeOfProducts(int skip, int take)
        {
            //var products = await _db.Products.OrderBy(c => c.ProductId).Skip(skip).Take(take).ToListAsync();

            var products =  _db.Products.OrderBy(c=>c.ProductId).AsEnumerable().Take(skip..(take+skip)).ToList();

            return Ok(products);
        }


        #endregion
    }
}
