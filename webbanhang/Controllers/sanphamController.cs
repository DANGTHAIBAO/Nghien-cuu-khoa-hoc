using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbanhang.Models;
using System.Net;
using System.IO;
using PagedList;

namespace webbanhang.Controllers
{
    public class sanphamController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: sanpham
        public ActionResult tatcasanpham(int? page)
        {
           /* if (Session["TAIKHOAN"] == null || Session["TAIKHOAN"] == null)
            {
                return RedirectToAction("Index", "Home");
            }*/


            var lstsanpham = db.SANPHAMs;
            ViewBag.lstSP = lstsanpham;
          
          
            return View();
        }
        public ActionResult sanphammsi()
        {
            /* var lstsanpham = db.SANPHAMs.Where(n => n.MALOAISP==1);*/
            var lstsanpham = db.SANPHAMs.Where(n => n.MALOAISP==1);
            ViewBag.lstSP = lstsanpham;
            return View();
        }
        public ActionResult giatucaodenthap()
        {
            var lstsanpham = db.SANPHAMs.OrderByDescending(n => n.DONGIA);
            lstsanpham.Reverse();
            ViewBag.lstSP = lstsanpham;
            return View();
        }
        public ActionResult sanphamamoi()
        {
            var lstsanpham = db.SANPHAMs.Where(n => n.MOI==1);
            ViewBag.lstSP = lstsanpham;
            return View();
        }
        public ActionResult sanpham(int? MALOAISP,int?page)
        {
            if (MALOAISP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstsanpham = db.SANPHAMs.Where(n => n.MALOAISP == MALOAISP);
            if (lstsanpham.Count() == 0)
            {
                return HttpNotFound();
            }
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            // tạo biến số sản phẩm trên trang
            int PageSize = 6;
            // tạo biến thứ 2 số phân trang hiện tại
            int PageNumber = (page ?? 1);
            ViewBag.MALOAISP = MALOAISP;
            
            return View(lstsanpham.OrderBy(n => n.MASP).ToPagedList(PageNumber, PageSize));
        }

        [ChildActionOnly]
        public ActionResult sanphampartial()
        {
           /* var lstSP = db.SANPHAMs;*/
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult sanphampartialIndex()
        {
            /* var lstSP = db.SANPHAMs;*/
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult sanphampartialtop()
        {
            /* var lstSP = db.SANPHAMs;*/
            return PartialView();
        }
        public ActionResult chitietsanpham(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            SANPHAM sanpham = db.SANPHAMs.SingleOrDefault(n => n.MASP == id );

            if (sanpham == null) return HttpNotFound();

            return View(sanpham);

           

        }
        [HttpGet]
        public ActionResult DANGKI()
        {
            ViewBag.CAUHOI = new SelectList(loadcauhoi());
            return View();
        }
        [HttpPost]
        public ActionResult DANGKI(THANHVIEN tv,FormCollection f)
        {
            ViewBag.CAUHOI = new SelectList(loadcauhoi());
            if (ModelState.IsValid)
            {
                tv.MALOAITV = 1;
                db.THANHVIENs.Add(tv);
                db.SaveChanges();
                ViewBag.thongbao = "thành công";
                return View();
          
            }
            ViewBag.thongbao = "thất bại";
            return View();

        }
        public List<string> loadcauhoi()
        {
            List<string> lstcauhoi = new List<string>();
            lstcauhoi.Add("con vật mà bạn yêu thích");
            lstcauhoi.Add("ca sĩ mà bạn yêu thích");
            lstcauhoi.Add("hiện tại bạn đang làm công việc gì");
            return lstcauhoi;
        }
        [HttpPost]
        public ActionResult dangnhap(FormCollection f)
        {
            string sTAIKHOAN = f["txtTenDangNhap"].ToString();
            string sMATKHAU = f["txtMatKhau"].ToString();
            THANHVIEN tv = db.THANHVIENs.SingleOrDefault(n => n.TAIKHOAN == sTAIKHOAN && n.MATKHAU == sMATKHAU);
            if (tv != null)
            {
                if (tv.MALOAITV == 1002)
                {
                    Session["TAIKHOAN"] = tv;

                    /* return Content("<script>location.reload();</script> ");*/
                    return RedirectToAction("Index","admin");
                }
                Session["TAIKHOAN"] = tv;

                /* return Content("<script>location.reload();</script> ");*/
                return RedirectToAction("Index", "Home");
            }
            ViewBag.thongbao = "Tài Khoản Mật Khẩu không hợp lệ!";
            return RedirectToAction("Index", "Home");
        }
      
        public ActionResult dangxuat()
        {
            Session["TAIKHOAN"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
    
}