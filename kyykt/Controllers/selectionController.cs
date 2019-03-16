using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kyykt.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kyykt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class selectionController : ControllerBase
    {
  

        // GET: api/selection/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/selection
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/selection/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
