using EFCoreTopics.Database.Data;
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

        #region Get Customer Using DB Functions

        [HttpGet("GetCustomer")]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            var customer =await _db.GetCustomer(customerId).FirstOrDefaultAsync();

            return Ok(customer);
        }

        #endregion
    }
}
