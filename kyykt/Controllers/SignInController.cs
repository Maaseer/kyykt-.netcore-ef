using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kyykt.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kyykt.Controllers
{
    [Route("api/[controller]")]
    public class SignInController : Controller
    {
        // GET: api/<controller>
        studentContext db = new studentContext();
        // GET: api/selection
        [HttpGet]
        public ActionResult<IEnumerable<StudentInClass>> Get(TeacherGetStudentSign value)
        {
            List<StudentInClass> ret = new List<StudentInClass>();
            var sig = db.ClassSignIn.Where(s => s.SignNum == value.SignInNums && s.ClassId == value.ClassId).OrderByDescending(s => s.SignInDate).ToList();
            var result = db.Selection.Where(s => s.ClassId == value.ClassId).Include(s => s.Student).ToList();
            foreach (var item in result)
            {
                bool HasSignIn = false;
                DateTime tempDateTime = DateTime.MaxValue;
                var i = db.StudentSignIn.Where(s => s.StudentId == item.StudentId).ToList();
                foreach (var item1 in sig)
                {
                    foreach (var item2 in i)
                    {
                        if (item1.SignInCode == item2.SignInCode)
                        {
                            tempDateTime = (DateTime)item1.SignInDate;
                            HasSignIn = true;
                            goto addStu;
                        }
                    }
                }
                addStu:
                ret.Add(new StudentInClass(item.Student.Name, item.StudentId, item.Student.Tel,tempDateTime ,HasSignIn));

            }
            return ret;
        }

        // GET api/<controller>/5
        [HttpGet("{TeacherId}")]
        public async Task<ActionResult<IEnumerable<TeacherGetClass>>> Get(string TeacherId)
        {
            List<TeacherGetClass> data = new List<TeacherGetClass>();
            var result = await db.TeaCourse.Where(s => s.TeacherId == TeacherId ).Include(d => d.OpeningClass).ThenInclude(s=>s.ClassTime).ToListAsync();
            foreach (var item in result)
            {
                string className = item.Name;
                foreach (var item1 in item.OpeningClass)
                {
                    if(item1.CloseClass == "0")
                    {
                        int times = item1.Times;
                        string classId = item1.ClassId;
                        List<ClassTime> classTimes = new List<ClassTime>();
                        foreach (var item2 in item1.ClassTime)
                        {
                            classTimes.Add(new ClassTime(item2.Time, item2.Place));
                        }
                        bool isInSign = (await db.ClassSignIn.Where(s => s.ClassId == item1.ClassId && s.IsOverDue == 0).FirstOrDefaultAsync() == null);
                        data.Add(new TeacherGetClass(classId, isInSign, className, times, classTimes));
                    }
                }
            }
            return data;
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
