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

        [HttpPost("CreateSharedWallet")]
        public async Task<IActionResult> CreateSharedWallet()
        {
            var wallet = new SharedWallet() { WalletAmount = 30000M, WalletName = "First Shared Wallet" };

            _db.SharedWallets.Add(wallet);

            await _db.SaveChangesAsync();

            return Ok(wallet);
        }

        [HttpPost("WithDrawFromWallet")]
        public async Task<IActionResult> WithDrawMoney(Guid walletId)
        {
            try
            {
                var wallet = await _db.SharedWallets.FirstOrDefaultAsync(c => c.Id.Equals(walletId));

                if (wallet == null)
                    return NotFound();

                wallet.WalletAmount -= 1000M;

                await _db.SaveChangesAsync();

                return Ok();

            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
