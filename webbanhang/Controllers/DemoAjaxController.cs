using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbanhang.Models;

namespace webbanhang.Controllers
{
    public class DemoAjaxController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: DemoAjax
        public ActionResult demoAjax()
        {
            return View();
        }
        public ActionResult sanphampartial()
        {
            var lstSP = db.SANPHAMs;
            return PartialView("sanphampartial",lstSP);
        }
        public ActionResult tatcasanpham()
        {

            var lstsanpham = db.SANPHAMs.Where(n => n.MOI == 1);
            ViewBag.lstSP = lstsanpham;
            return View();
        }
    }
}