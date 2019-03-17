using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kyykt.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kyykt.Controllers
{
    [Route("api/[controller]")]
    public class ClassController : Controller
    {
        studentContext db = new studentContext();
        ILogger<TodoController> logger;

        public ClassController(ILogger<TodoController> logger)
        {
            this.logger = logger;
        }

        // GET: api/Class?studentId=XXXXX
        [HttpGet]
        public ActionResult<IEnumerable<ClassTable>> StudentGetClass(string studentId)
        {
            List<ClassTable> data = new List<ClassTable>();
            //根据学号查询出该学生的所有课程
            var result = db.Selection.Where(s => s.StudentId == studentId);
            //遍历结果，将需要的信息封装入类中
            foreach (var i in result)
            {
                var temp = new ClassTable
                {
                    CourseId = db.OpeningClass.FirstOrDefault(s => s.ClassId == i.ClassId).CourseId,
                    ClassTime = new List<ClassTime>()
                };

                temp.CourseName = db.TeaCourse.FirstOrDefault(s => s.CourseId == temp.CourseId).Name;
                var temp_result = db.ClassTime.Where(s => s.ClassId == i.ClassId);

                foreach (var j in temp_result)
                    temp.ClassTime.Add(new ClassTime(j.Time, j.Place, j.ClassId));
                data.Add(temp);
            }
            return Json(data);
        }

        //GET: api/Class/xxxxxxx
        [HttpGet("{teacherId}")]
        public ActionResult<IEnumerable<ClassTable>> TeacherGetClass(string teacherId)
        {
            List<ClassTable> data = new List<ClassTable>();
            //根据学号查询出该学生的所有课程
            var result = db.TeaCourse.Where(s => s.TeacherId == teacherId).Include(a=>a.OpeningClass).ToList();
            //遍历结果，将需要的信息封装入类中
            foreach (var i in result)
            {
                var temp_openingClass = i.OpeningClass.ToList();
                foreach (var item in temp_openingClass)
                {
                    var temp = new ClassTable
                    {
                        CourseId = i.CourseId,
                        CourseName = i.Name,
                        ClassTime = new List<ClassTime>()
                    };
                    var temp_ClassTime = db.ClassTime.Where(s => s.ClassId == item.ClassId).ToList();
                    foreach (var j in temp_ClassTime)
                        temp.ClassTime.Add(new ClassTime(j.Time, j.Place, j.ClassId));

                    data.Add(temp);
                }
            }
            return Json(data);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
