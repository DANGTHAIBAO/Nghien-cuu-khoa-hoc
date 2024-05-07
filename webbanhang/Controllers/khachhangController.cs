using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbanhang.Models;

namespace webbanhang.Controllers
{
    public class khachhangController : Controller
    {
        // GET: khachhang
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult Index()
        {
            /*var lstkKH = from KH in db.KHACHHANGs select KH;*/
            var lstkKH = db.KHACHHANGs;
            return View(lstkKH);
        }
        public ActionResult Index1()
        {
            var lstkKH = db.KHACHHANGs;
            /*var lstkKH = from KH in db.KHACHHANGs select KH;*/
            return View(lstkKH);
        }
        public ActionResult truyvan1doituong()
        {
            //truy vấn 1 đối tượng
            var lstkKH = from KH in db.KHACHHANGs where KH.MAKH == 4 select KH;
            KHACHHANG khang = lstkKH.FirstOrDefault();
            return View(khang);
        }
        public ActionResult sortdulieu()
        {
           /* List<KHACHHANG> lstKH = db.KHACHHANGs.OrderBy(n => n.TENKH).ToList();*/
            List<KHACHHANG> lstKH = db.KHACHHANGs.OrderByDescending(n => n.TENKH).ToList();
            return View(lstKH);
        }
        public ActionResult groupdulieu()
        {
            List<THANHVIEN> lstKH = db.THANHVIENs.OrderByDescending(n => n.TAIKHOAN).ToList();
            return View(lstKH);
        }
    }
}