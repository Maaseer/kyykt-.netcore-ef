using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using kyykt.Model.QrCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kyykt.Controllers
{
    [Route("api/[controller]")]
    public class QrCodeController : Controller
    {

        studentContext db = new studentContext();

        ILogger<QrCodeController> logger;
        

        public QrCodeController(ILogger<QrCodeController> logger)
        {
            this.logger = logger;
        }

        public Bitmap code(string msg, int version, int pixel, int icon_size, int icon_border, bool white_edge)
        {
            string icon_path = Environment.CurrentDirectory + @"\wwwroot\img\logo.jpg";
            logger.LogInformation(icon_path);

            QRCoder.QRCodeGenerator code_generator = new QRCoder.QRCodeGenerator();

            QRCoder.QRCodeData code_data = code_generator.CreateQrCode(msg, QRCoder.QRCodeGenerator.ECCLevel.M/* 这里设置容错率的一个级别 */, true, true, QRCoder.QRCodeGenerator.EciMode.Utf8, version);

            QRCoder.QRCode code = new QRCoder.QRCode(code_data);


            Bitmap icon = new Bitmap(icon_path);
            Bitmap bmp = code.GetGraphic(pixel, Color.Black, Color.White, icon, icon_size, icon_border, white_edge);

            return bmp;

        }

        public static string GetRandomString(int length, bool useNum = true, bool useLow = true, bool useUpp = true, bool useSpe = false, string custom = "")
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }


        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<QrToTeacher>> Get(string ClassId)
        {
            int SignNum;
            //计算签到次数
            try
            {
                var result = db.OpeningClass.Where(s => s.ClassId == ClassId).FirstOrDefault();
                SignNum = result.Times - 1;
            }
            catch
            {
                return BadRequest();
            }
            //若上一次签到未关闭，则将其设置为过期
            try
            {
                var result = db.ClassSignIn.Where(s => s.ClassId == ClassId).OrderBy(s => s.SignInDate).LastOrDefault();

                if (result!=null && result.IsOverDue == 0)
                    result.IsOverDue = 1;

            }
            catch
            {
                return BadRequest();
            }
            
            string FilePathstr = Environment.CurrentDirectory + @"\wwwroot\img\code" + ClassId + @".jpg";
            string CodeStr = GetRandomString(40);
            try
            {
                code(CodeStr, 10, 40, 30, 1, false).Save(FilePathstr, ImageFormat.Jpeg);
                var si = new ClassSignIn(CodeStr, ClassId, DateTime.Now, SignNum);
                await db.ClassSignIn.AddAsync(si);
                await db.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("出问题啦~");
            }

            return new QrToTeacher(@"img\code" + ClassId + @".jpg", CodeStr);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]StudentSignIn value)
        {
            try
            {
                var result =await  db.ClassSignIn.Where(s => s.SignInCode == value.SignInCode).FirstOrDefaultAsync();
                if (result.IsOverDue == 1)
                    return BadRequest("二维码已过期");
                var temp = await db.ClassSignIn.Where(s => s.SignNum == result.SignNum).ToListAsync();
                foreach(var i in temp)
                {
                    if (await db.StudentSignIn.Where(s => s.SignInCode == i.SignInCode && s.StudentId == value.StudentId).FirstOrDefaultAsync() != null)
                        return BadRequest("本节课已签到");
                }

                await db.StudentSignIn.AddAsync(value);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest("签到错误");
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody]TeacherToQr value)
        {
            try {
                var result = db.OpeningClass.Where(s => s.ClassId == value.ClassId).FirstOrDefault();
                result.Times++;
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
        public async Task<ActionResult> Delete(string id)
        {
            try{
                var result = await db.ClassSignIn.Where(s => s.SignInCode == id).FirstOrDefaultAsync();
                result.IsOverDue = 1;
                db.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
