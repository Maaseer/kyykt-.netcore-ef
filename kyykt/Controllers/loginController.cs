using kyykt.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kyykt.Controllers
{
    [Route("api/[controller]")]
    public class loginController : Controller
    {
        studentContext db = new studentContext();
        ILogger<loginController> logger;

        public loginController(ILogger<loginController> logger)
        {
            this.logger = logger;
        }

        public string getOpenID(string wxId)
        {
            //请求的url
            string url = "https://api.weixin.qq.com/sns/jscode2session?appid=wx3360d69b35f91575&secret=9c6fbd4113474497b9e9eae84907d8dc&js_code=" + wxId + "&grant_type=authorization_code";
            //json类
            JObject ja = new JObject();
            //http请求初始化
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //发送请求并回收数据
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response == null) { NotFound(); }
            //将请求转化为输入流并且转化为json格式
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();
            ja = JObject.Parse(result);

            //获取请求中的openid
            try
            {
                wxId = ja["openid"].ToString();
            }
            catch
            {
                return null;
            }
            return wxId;
        }
        //----------------------------------------------------------------------------------------------       
        //根据微信ID返回学生信息
        // GET api/login/wxid
        [HttpGet("{wxId}")]
        public ActionResult<Student> Get(string wxId)
        {
            wxId = getOpenID(wxId);

            if (wxId == null)
                return NotFound();

            var st = db.Student.FirstOrDefault(s => s.WxId == wxId);
            if (st == null)
                return NotFound();

            return st;
        }

        //--------------------------------------------------------------------------------------------
        //得到所有学生信息
        //get api/login 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            var st = await db.Student.ToListAsync();
            if (st == null)
                return NotFound();
            return st;
        }
        //-------------------------------------------------------------------------------------------
        //绑定学号
        // POST api/login   JSON：WxId ，StudentId
        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody]regist value)
        {


            //获得openID
            string openID = getOpenID(value.WxId);
            //若openID已存在，则该微信已被绑定
            if (openID == null)
                return BadRequest("验证微信号失败");
            var result = await db.Student.FirstOrDefaultAsync(s => s.WxId.Equals(openID));
            if (result != null)
                return BadRequest("该微信已绑定其余学号");

            var student = await db.Student.FirstOrDefaultAsync(s => s.StudentId == value.StudentId);
            //如果能根据学号检索到学生，则表示该学号已被绑定
            if (student.WxId != "")
            {
                return BadRequest("该学号已被绑定");
            }

            if (student == null)
                return BadRequest("学号不存在");
            //绑定微信openID

            try
            {
                student.WxId = openID;
                student.Picture = value.Picture;
                await db.SaveChangesAsync();
            }
            catch { return BadRequest("登陆失败"); }
            //保存数据库

            //返回学生信息 
            return student;
        }

    }
}
