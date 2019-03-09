using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kyykt.Model.Todo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kyykt.Controllers
{
    [Route("api/[controller]")]        
        public class TodoController : ControllerBase
        {
            studentContext db = new studentContext();
            ILogger<TodoController> logger;

            public TodoController(ILogger<TodoController> logger)
            {
                this.logger = logger;
            }
//--------------------------------------------------------------------------------------
        //输入StudengId，得到该学生所有的ToDo
        //api/ToDo/ + StudentId
        [HttpGet("{StudentID}")]
        public  ActionResult<IEnumerable<NeedToDo>> Get(string StudentID)
        {
            IEnumerable<NeedToDo> todo;
             todo = from i in db.NeedToDo
                       where i.StudentId == StudentID
                       select i;
            return todo.ToList();
        }
//--------------------------------------------------------------------------------------
        //输入：StudentId 和 hide ，获取ToDo条目
        //Get api/ToDo?StudentId=XXXX&hide=X    
        [HttpGet]
            public ActionResult<IEnumerable<NeedToDo>> Get(todo td)
            {
                IEnumerable<NeedToDo> todo = from i in db.NeedToDo
                           where (i.StudentId == td.StudentId) && (i.Hide == td.hide)
                           select i;

                return todo.ToList();
            }
//------------------------------------------------------------------------------------------
        //  输入一个ToDo的完整的信息，替换掉旧的ToDo
        //POST  api/ToDo Json：传入一个完整的ToDo信息    
        [HttpPost]
        public async Task<ActionResult<NeedToDo>> Post([FromBody]NeedToDo value)
        {

            var result = await db.NeedToDo.FirstOrDefaultAsync(s => s.NeedToDoId == value.NeedToDoId && s.StudentId == value.StudentId);
            try
            {
                result.Hide = value.Hide;
                result.Time = value.Time;
                result.Finish = value.Finish;
                result.Content = value.Content;
                result.Extra = value.Extra;
                await db.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            
            return Ok();
        }
//-------------------------------------------------------------------------------------------
        //输入一个ToDo的完整的信息，新增一个ToDo
        //PUT api/ToDo   Json：传入一个完整的ToDo信息 
        [HttpPut]
        public async Task<ActionResult> Put([FromBody]NeedToDo vaule)
        {
            var td = new NeedToDo();
            try{
                td.StudentId = vaule.StudentId;
                td.Content = vaule.Content;
                td.Hide = vaule.Hide;
                td.Time = vaule.Time;
                td.Finish = vaule.Finish;
                td.Extra = vaule.Extra;
                await db.NeedToDo.AddAsync(td);
                await db.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
//-------------------------------------------------------------------------------------------
        //输入一个ToDo的完整的信息，删除该ToDo
        //DELETE api/ToDo Json：传入一个完整的ToDo信息
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody]AlertTodo value)
        {
            var result = await db.NeedToDo.FirstOrDefaultAsync(s=> value.NeedToDoId == s.NeedToDoId && value.StudentId == s.StudentId);
             try
            {
                if (result.Hide.Equals("0"))
                    result.Hide = "1";
                else
                    result.Hide = "0";
                await db.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }
            return Ok();
        }
    }

 
    
}
