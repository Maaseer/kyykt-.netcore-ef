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
    public class NoticeController : Controller
    {
        studentContext db = new studentContext();
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<Notice>> Get(string ClassId, int NoticeId)
        {
            var result = await db.Notice.Where(s => s.ClassId == ClassId && s.NoticeId == NoticeId).FirstOrDefaultAsync();
            if (result == null)
                return NotFound("未找到通知");
            return result;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Notice>>> Get(string id)
        {
            var result = await db.Notice.Where(s => s.ClassId == id).OrderByDescending(s=>s.Time).ToListAsync();
            return Json(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Notice value)
        {
            try
            {
                var result = await db.Notice.FirstOrDefaultAsync(s => s.ClassId == value.ClassId && s.NoticeId == value.NoticeId);
                result.Time = value.Time;
                result.ClassId = value.ClassId;
                result.Content = value.Content;
                result.Head = value.Head;
                await db.SaveChangesAsync();
            }
            catch { return BadRequest("修改通知出错"); }
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody]Notice value)
        {
            try
            {
                value.Time = DateTime.Now;
                db.Notice.Add(value);
                await db.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("添加通知失败");
            }
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody]Notice vaule)
        {
            try
            {
                var result = await db.Notice.FirstOrDefaultAsync(s => s.NoticeId == vaule.NoticeId && s.ClassId == vaule.ClassId);
                db.Notice.Remove(result);
                await db.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("删除失败");
            }
            return Ok();
        }
    }
}
