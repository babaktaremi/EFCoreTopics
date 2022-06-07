﻿using EFCoreTopics.Database.Data;
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


        [HttpPost("InsertManyAddress")]
        public async Task<IActionResult> InsertManyAddress()
        {
            for (int i = 0; i < 1000; i++)
            {
                _db.Addresses.Add(new Address()
                {
                    AddressLine2 = "Test2", City = $"Test{i}", StateProvince = $"Test_{i}", CountryRegion = "Iran",
                    PostalCode = "1234",AddressLine1 = "Test1"
                });
            }

            await _db.SaveChangesAsync();

            return Ok();
        }

        #endregion
    }
}
