using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbanhang.Models;

namespace webbanhang.Controllers
{
    
    public class HomeController : Controller
        
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: Home
        public ActionResult Index()
        {
            
            var spmsi = db.SANPHAMs.Where(n=>n.MALOAISP==1);
            var spasus = db.SANPHAMs.Where(n => n.MALOAISP == 3);
            var spacer = db.SANPHAMs.Where(n => n.MALOAISP == 2);
            var sptop = db.SANPHAMs.OrderBy(n => n.MALOAISP);
            ViewBag.lstmsi = spmsi;
            ViewBag.lstacer = spacer;
            ViewBag.lstasus = spasus;
            ViewBag.lsttop = sptop;
            return View();
        }
        public ActionResult blog()
        {
            return View();
        }
        public ActionResult contact()
        {
            return View();
        }

      
        public ActionResult dangxuat()
        {
            Session["TAIKHOAN"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}