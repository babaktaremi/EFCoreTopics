using System.Data;
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

        [HttpPost("AddSomeProducts")]
        public async Task<IActionResult> AddSomeProducts()
        {
            await using var transaction =await _db.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            _db.Database.SetCommandTimeout(TimeSpan.FromSeconds(100));
            try
            {
                

                var productModel1 = new ProductModel()
                    {  Name = $"New Custom Product 1 {Guid.NewGuid().ToString().Substring(10)}" };
                var productModel2 = new ProductModel()
                    { Name = $"New Custom Product 2 {Guid.NewGuid().ToString().Substring(10)}" };

                var productDescription1 = new ProductDescription() { Description = $"New Description 1 {Guid.NewGuid().ToString().Substring(10)}" };
                var productDescription2 = new ProductDescription() { Description = $"New Description 2 {Guid.NewGuid().ToString().Substring(10)}" };

                _db.ProductModels.AddRange(productModel1,productModel2);
                _db.ProductDescriptions.AddRange(productDescription1,productDescription2);
                await _db.SaveChangesAsync();

                await transaction.CreateSavepointAsync("Saving Product Models");

                await Task.Delay(4_000);

                var currentProductDescriptionCount = await _db.ProductModelProductDescriptions.CountAsync();

                var productModelDescription1 = new ProductModelProductDescription()
                {
                    Culture = Guid.NewGuid().ToString().Substring(0, 2),
                    ProductDescriptionId = productDescription1.ProductDescriptionId,
                    ProductModelId = productModel1.ProductModelId
                };

                var productModelDescription2 = new ProductModelProductDescription()
                {
                    Culture = Guid.NewGuid().ToString().Substring(0, 2),
                    ProductDescriptionId = productDescription2.ProductDescriptionId,
                    ProductModelId = productModel2.ProductModelId
                };

                _db.ProductModelProductDescriptions.AddRange(productModelDescription1,productModelDescription2);
                await _db.SaveChangesAsync();

                var newProductDescriptionCount = await _db.ProductModelProductDescriptions.CountAsync();

                if (newProductDescriptionCount != currentProductDescriptionCount + 2)
                    await transaction.RollbackToSavepointAsync("Saving Product Models");

                await transaction.CommitAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }


        }
    }
}
