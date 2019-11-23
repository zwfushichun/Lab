using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Models;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var item = new Orders
            {
                Id = id,
                Content = $"{id}的订单明细"
            };
            return JsonSerializer.Serialize(item);
        }
    }
}