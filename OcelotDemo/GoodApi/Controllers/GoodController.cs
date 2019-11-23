using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GoodApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var item = new Goods
            {
                Id = id,
                Content = $"{id}的关联的商品明细"
            };
            return JsonSerializer.Serialize(item);
        }
    }
}