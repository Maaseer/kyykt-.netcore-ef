using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kyykt.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        studentContext db = new studentContext();
        ILogger<MessagesController> logger;

        public MessagesController( ILogger<MessagesController> logger)
        {
       
            this.logger = logger;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassMessage>>> Get()
        {
            try
            {           
                var result = await db.ClassMessage.ToListAsync();
                return result;         
            }
            catch
            {
                return BadRequest("请求出错");
            }        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ClassMessage>>> Get(string id)
        {
            try
            {
                var result = await db.ClassMessage.Where(s => s.ClassId == id).ToListAsync();
                return result;
            }
            catch
            {
                return BadRequest("请求出错");
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]ClassMessage value)
        {
            try
            {
                await db.ClassMessage.AddAsync(value);
                await db.SaveChangesAsync();
                return Ok("添加成功");
            }
            catch(Exception e) { 
                return BadRequest("添加失败,请求出错:" + e.InnerException.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody]ClassMessage value)
        {
            try
            {
               var result = await db.ClassMessage.Where(s => s.ClassId == value.ClassId && s.MessageId == value.MessageId).FirstOrDefaultAsync();
                result.Message = value.Message;
                result.MessageHead = value.MessageHead;
                await db.SaveChangesAsync();
                return Ok("修改成功");
            }
            catch (Exception e)
            {
                return BadRequest("修改失败，请求出错:" + e.Message);
            }
        }
        [HttpPut("reply/")]
        public async Task<ActionResult> PutReply([FromBody]ClassMessage value)
        {
            try
            {
                var result = await db.ClassMessage.Where(s => s.ClassId == value.ClassId && s.MessageId == value.MessageId).FirstOrDefaultAsync();
                result.ReplyMessage = value.ReplyMessage;
                result.HasReply = 1;
                await db.SaveChangesAsync();
                return Ok("回复成功");
            }
            catch (Exception e)
            {
                return BadRequest("修改失败，请求出错:" + e.Message);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody]ClassMessage value)
        {
            try
            {
                db.ClassMessage.Remove(value);
                await db.SaveChangesAsync();
                return Ok("删除成功");
            }
            catch (Exception e)
            {
                return BadRequest("删除失败，请求出错:" + e.Message);
            }
        }
    }
}
