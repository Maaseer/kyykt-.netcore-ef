using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using kyykt.Model.Teacher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kyykt.Controllers
{
    [Route("api/[controller]")]
    public class teacherController : Controller
    {
        studentContext db = new studentContext();




        /*[HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = @"img\aaa.jpg";

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }*/
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<Teacher>> Get(TeacherLogin value)
        {

            var result = await db.Teacher.FirstOrDefaultAsync(s => s.TeacherId == value.TeacherId && s.TeacherPasswd == value.TeacherPasswd);
            if (result == null)
                return BadRequest("账号或密码错误");
            return result;

        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Teacher>> Post([FromBody]Teacher value)
        {
            try
            {
                var result = await db.Teacher.FirstOrDefaultAsync(s => s.TeacherId == value.TeacherId);
                if (result == null)
                    return BadRequest("教师不存在");
                result.TeacherPasswd = value.TeacherPasswd;
                result.WxId = value.WxId;
                result.Picture = value.Picture;
                result.Occupation = value.Occupation;
                result.Name = value.Name;
                await db.SaveChangesAsync();
                return result;
            }
            catch
            {
                return BadRequest("修改失败");
            }
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
