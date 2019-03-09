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
    public class teacherController1 : Controller
    {
        studentContext db = new studentContext();
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            string[] v = new string[] { "value1", "value2" };

            return v;

        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Teacher>> Post([FromBody]Teacher value)
        {
            var result =  await db.Teacher.FirstOrDefaultAsync(s => s.TeacherId == value.TeacherId&&s.WxId==value.WxId);
            if (result == null)
                return BadRequest("账号或密码错误");
            return result;
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody]Teacher value)
        {
            try
            {
                var result = await db.Teacher.FirstOrDefaultAsync(s => s.TeacherId == value.TeacherId);
                result.WxId = value.WxId;
                result.Picture = value.Picture;
                result.Occupation = value.Occupation;
                await db.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
