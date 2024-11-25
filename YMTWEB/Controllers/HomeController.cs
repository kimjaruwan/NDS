using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YMTWEB.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using YMTWEB.Data; // ใส่ namespace ที่ใช้ในโปรเจกต์ของคุณ
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using QuoNDS.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;


namespace YMTWEB.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }



        public IActionResult Index()
        {
            //เริ่มต้นเป็นหน้าแรก
            return View();
        }


        [HttpPost]
        public ActionResult CheckUser()
        {
            //var user = "thanet";
            //var pass = "1234";

            var user = "jaruwan.s";
            var pass = "j1234";

            var items = _context.YMTG_USER.Where(z => z.YPTUser == user && z.YPTPass == pass).FirstOrDefault();
            return Ok(items);
        }

        public ActionResult LogOff()
        {

            return RedirectToAction("Login", "Home");
        }


        [HttpPost]
        public IActionResult AddItem([FromBody] Item itemNews)
        {
            if (itemNews.Name != "")
            {
                _context.Items.Add(itemNews);
                _context.SaveChanges();

                return Ok(itemNews);
            }
            return BadRequest("Invalid data");
        }

        [HttpPost]
        public ActionResult GetItemss()
        {
            var items = _context.Items.ToList();
            return Ok(items);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult CreateQuo()
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetSku()
        {

            //จัดกลุ่มข้อมูลตาม Description ของตาราง MasterStyles แล้วแปลงเป็นลิสต์(List)


            var MasterStylesDataGroup = _context.MasterStyles.GroupBy(p => p.Description).ToList();
            var MasterStylesData = MasterStylesDataGroup.Select(g => g.Key).ToList();
            return Ok(MasterStylesData);
            // สรุป
            //ดึงข้อมูล MasterStyles จากฐานข้อมูล
            //จัดกลุ่มข้อมูลตาม Description(ค่าเดียวกันอยู่กลุ่มเดียวกัน)
            //ดึงค่าที่ไม่ซ้ำ(Key) จากแต่ละกลุ่ม
            //ส่งผลลัพธ์ในรูปแบบ JSON กลับไป


        }

        [HttpGet]
        public ActionResult GetColors()
        {
            var colors = new List<object>();

            colors.Add("Black");
            colors.Add("Green");
            colors.Add("Gray");
            colors.Add("Navy");
            colors.Add("White");

            return Ok(colors);

        }
        [HttpGet]
        public ActionResult GetSizes()
        {
            var sizes = new List<object>();
            sizes.Add("XS");
            sizes.Add("S");
            sizes.Add("M");
            sizes.Add("L");
            sizes.Add("XL");
            sizes.Add("2XL");
            sizes.Add("3XL");
            sizes.Add("4XL");

            return Ok(sizes);
        }
        //public ActionResult genQuotation()
        //{
        //    var generatedQuotationNumber = _context.QuotationRuns
        //        .FromSqlRaw("EXEC gnerateQuotationNumber")
        //        .Select(q => q.QuotationNumber)
        //        .FirstOrDefault();

        //    return Ok(generatedQuotationNumber);
        //    //return Ok(new { QuotationNumber = generatedQuotationNumber });
        //}

        public ActionResult GenerateQuotationNumber()
        {
            // เรียกคำสั่ง Raw SQL ผ่าน EF Core



            var GenerateQuotation = _context.QuotationRuns.GroupBy(p => p.QuotationNumber).ToList();
            var GenerateQuotations = GenerateQuotation.Select(g => g.Key).ToList();
            return Ok(GenerateQuotations);
            //// ตรวจสอบผลลัพธ์
            //if (generatedQuotationNumber == null)
            //{
            //    return BadRequest("ไม่พบผลลัพธ์จาก Stored Procedure");
            //}

            // ส่งผลลัพธ์กลับในรูปแบบ JSON;
            ////return Ok(new { QuotationNumber = generatedQuotationNumber });
            //return Ok(generatedQuotationNumber);

        }



        [HttpGet]
        public ActionResult GetProvinces()
         {


            var GetProvinceList = _context.MasterProvinces.GroupBy(p => p.Provinces).ToList();
            var GetProvinceLists = GetProvinceList.Select(g => g.Key).ToList();
            return Ok(GetProvinceLists);


        }
        [HttpPost]
        public ActionResult GetDistricts([FromBody] MasterProvince Provinces)
        {

            var GetProvinceList = _context.MasterProvinces.Where(z=> z.Provinces == Provinces.Provinces).GroupBy(p => p.Districts).ToList();
            var GetProvinceLists = GetProvinceList.Select(g => g.Key).ToList();
            return Ok(GetProvinceLists);
      
        }

        [HttpPost]
        public ActionResult GetListSubs([FromBody] MasterProvince Districts)
        {

            var GetDistricts = _context.MasterProvinces.Where(z => z.Districts == Districts.Districts).GroupBy(p => p.SubDistricts).ToList();
            var GetDistrictsList = GetDistricts.Select(g => g.Key).ToList();
            return Ok(GetDistrictsList);

        }

        [HttpPost]
        public ActionResult GetListZipcode([FromBody] MasterProvince SubDistricts)
        {
            var GetDistricts = _context.MasterProvinces.Where(z => z.SubDistricts == SubDistricts.SubDistricts).GroupBy(p => p.ZipCode).ToList();
            var GetDistrictsList = GetDistricts.Select(g => g.Key).ToList();
            return Ok(GetDistrictsList);
        }



        //GetOrderType
        [HttpGet]
        public ActionResult GetOrderType()
        {


            //var GetOrderTypeList = _context.TypeOrder.GroupBy(p => p.TypeRecapFrom).ToList();
            //var GetOrderTypeLists = GetOrderTypeList.Select(g => g.Key).ToList();

            var GetOrderTypeLists = _context.TypeOrder
          .Select(p => new
          {
              TypeRecapFrom = p.TypeRecapFrom,
              CodeChar = p.CodeChar
          })
          .Distinct()
          .ToList();
            return Ok(GetOrderTypeLists);




        }

        [HttpPost]
        public ActionResult SaveQuotation([FromBody] YmtgOrderNds dataQuotation)
        {
            if (dataQuotation == null)
            {
                return BadRequest("ข้อมูลไม่ถูกต้อง");
            }

            // บันทึกข้อมูลลงในฐานข้อมูลโดยตรง
            _context.YmtgOrders.Add(dataQuotation);

            _context.SaveChanges();

            // ส่งข้อมูลที่บันทึกกลับไป
            return Ok();
        }




    }


}
