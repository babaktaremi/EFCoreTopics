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
        private readonly ILogger<MainController> _logger;
        public MainController(AdventureWorksLContext db, ILogger<MainController> logger)
        {
            _db = db;
            _logger = logger;
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

        [HttpPost("AddPriceHistory")]
        public async Task<IActionResult> AddSomePriceHistory()
        {
            var priceHistories = new List<PriceHistory>();
            
            int iteration=0;
            var priceDate = DateTime.Now;

            var random = new Random();

            while (iteration<20000)
            {
                priceHistories.Add(new PriceHistory(){ProductName = "PS 5",Date = priceDate,RecordedPrice =random.Next(20_000_000,23_000_000) });
                priceDate = priceDate.AddDays(1);
                iteration++;
            }

            _db.PriceHistories.AddRange(priceHistories);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("WithStreaming")]
        public async Task<IActionResult> TestWithStreaming(CancellationToken cancellationToken)
        {

            await foreach (var item in _db.GetPricesWithStreaming(cancellationToken))
                _logger.LogWarning($"Product With Id {item.Id} is Yield");

            return Ok();
        }

        [HttpGet("WithoutStreaming")]
        public async Task<IActionResult> TestWithoutStreaming(CancellationToken cancellationToken)
        {
            var priceHistory = await _db.PriceHistories.AsNoTracking().ToListAsync(cancellationToken);

            foreach (var history in priceHistory)
            {
                _logger.LogWarning($"Some Id {history.Id}");
            }

            return Ok();
        }
    }
}
