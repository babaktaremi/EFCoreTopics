using EFCoreTopics.Database.Data;
using EFCoreTopics.Database.Models.Tpc;
using EFCoreTopics.Database.Models.Tph;
using EFCoreTopics.Database.Models.Tpt;
using EFCoreTopics.ViewModels;
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


        #region TPH

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(AddOrderViewModel model)
        {
            if (model.IsInternational)
            {
                var internationalOrder = new InternationalOrderTph()
                { CityName = model.CityName, CountryName = model.CountryName, UserName = model.UserName, OrderName = model.OrderName };

                _db.Orders.Add(internationalOrder);

                await _db.SaveChangesAsync();

                return Ok(internationalOrder.Id);
            }

            var order = new OrderTph() { UserName = model.UserName, OrderName = model.OrderName };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return Ok(order.Id);
        }

        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var order = await _db.Orders.AsNoTracking().FirstOrDefaultAsync(c => c.Id == orderId);

            if (order == null)
                return NotFound();

            if (order is InternationalOrderTph internationalOrder)
            {
                return Ok(new
                {
                    OrderId = internationalOrder.Id,
                    ShippingCode = internationalOrder.ShippingCode,
                    ArrivalDate = internationalOrder.CreatedDate.AddDays(30),
                    OrderName = internationalOrder.OrderName
                });
            }

            return Ok(new
            {
                OrderId = order.Id,
                OrderName = order.OrderName,
                ArrivalDate = order.CreatedDate.AddDays(3),
            });
        }

        #endregion

        #region TPT

        [HttpPost("AddOrderTpt")]
        public async Task<IActionResult> AddOrderTpt(AddOrderViewModel model)
        {
            if (model.IsInternational)
            {
                var internationalOrder = new InternationalOrderTpt()
                { CityName = model.CityName, CountryName = model.CountryName, UserName = model.UserName, OrderName = model.OrderName };

                _db.OrdersTpt.Add(internationalOrder);

                await _db.SaveChangesAsync();

                return Ok(internationalOrder.Id);
            }

            var order = new OrderTpt() { UserName = model.UserName, OrderName = model.OrderName };

            _db.OrdersTpt.Add(order);
            await _db.SaveChangesAsync();
            return Ok(order.Id);
        }

        [HttpGet("GetOrderTpt")]
        public async Task<IActionResult> GetOrderTpt(int orderId)
        {
            var order = await _db.OrdersTpt.AsNoTracking().FirstOrDefaultAsync(c => c.Id == orderId);
            if (order == null)
                return NotFound();

            if (order is InternationalOrderTpt internationalOrder)
            {
                return Ok(new
                {
                    OrderId = internationalOrder.Id,
                    ShippingCode = internationalOrder.ShippingCode,
                    ArrivalDate = internationalOrder.CreatedDate.AddDays(30),
                    OrderName = internationalOrder.OrderName
                });
            }

            return Ok(new
            {
                OrderId = order.Id,
                OrderName = order.OrderName,
                ArrivalDate = order.CreatedDate.AddDays(3),
            });
        }

        #endregion

        #region TPC

        [HttpPost("AddOrderTpc")]
        public async Task<IActionResult> AddOrderTpc(AddOrderViewModel model)
        {
            if (model.IsInternational)
            {
                var internationalOrder = new InternationalOrderTpc()
                { CityName = model.CityName, CountryName = model.CountryName, UserName = model.UserName, OrderName = model.OrderName };

                _db.BaseOrdersTpc.Add(internationalOrder);

                await _db.SaveChangesAsync();

                return Ok(internationalOrder.Id);
            }

            var order = new OrderTpc() { UserName = model.UserName, OrderName = model.OrderName };

            _db.BaseOrdersTpc.Add(order);
            await _db.SaveChangesAsync();
            return Ok(order.Id);
        }

        [HttpGet("GetOrderTpc")]
        public async Task<IActionResult> GetOrderTpc(int orderId)
        {
            var order = await _db.BaseOrdersTpc.AsNoTracking().FirstOrDefaultAsync(c => c.Id == orderId);
            if (order == null)
                return NotFound();

            if (order is InternationalOrderTpc internationalOrder)
            {
                return Ok(new
                {
                    OrderId = internationalOrder.Id,
                    ShippingCode = internationalOrder.ShippingCode,
                    ArrivalDate = internationalOrder.CreatedDate.AddDays(30),
                    OrderName = internationalOrder.OrderName
                });
            }

            else if (order is OrderTpc orderTpc)
            {
                return Ok(new
                {
                    OrderId = orderTpc.Id,
                    OrderName = orderTpc.OrderName,
                    ArrivalDate = orderTpc.CreatedDate.AddDays(3),
                });
            }

            return NotFound();
        }

        #endregion
    }
}
