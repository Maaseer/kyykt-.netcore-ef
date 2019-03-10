using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kyykt.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        studentContext db = new studentContext();
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{TeacherId}")]
        public async Task<ActionResult<IEnumerable<TeaCourse>>> Gets
            (string TeacherId)
        {
            try
            {
                var result = await db.TeaCourse.Where(s => s.TeacherId == TeacherId).ToListAsync();
                if (result == null)
                    return NotFound();
                return result;

            }
            catch {
                return BadRequest();
            }
            
        }
        [HttpGet]
        public async Task<ActionResult<TeaCourse>> Get(string CourseId)
        {
            try
            {
                var result = await db.TeaCourse.Where(s => s.CourseId == CourseId).FirstOrDefaultAsync();
                if (result == null)
                    return NotFound();
                return result;

            }
            catch
            {
                return BadRequest();
            }

        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]TeaCourse value)
        {
            try
            {
                var result = await db.TeaCourse.Where(s=>s.CourseId == value.CourseId).FirstOrDefaultAsync();
                result.Name = value.Name;
                result.Hours = value.Hours;
                result.TeacherId = value.TeacherId;
                result.Credit = value.Credit;
                result.Way = value.Way;
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody]TeaCourse value)
        {
            try
            {
                await db.TeaCourse.AddAsync(value);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
