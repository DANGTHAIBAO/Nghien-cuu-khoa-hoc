using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbanhang.Models;
using System.IO;

namespace webbanhang.Controllers
{
    public class adminController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: admin
        public ActionResult Index()
        {
            return View(db.SANPHAMs);
        }
        [HttpGet]
        public ActionResult themsanpham()
        {
            // load droplist
            ViewBag.MANCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TENNCC), "MANCC", "TENNCC");
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MALOAISP), "MALOAISP", "TENLOAI");
            ViewBag.MANSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MANSX), "MANSX", "TENNSX");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult themsanpham(SANPHAM sp, HttpPostedFileBase HINHANH)
        {
            ViewBag.MANCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TENNCC), "MANCC", "TENNCC");
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MALOAISP), "MALOAISP", "TENLOAI");
            ViewBag.MANSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MANSX), "MANSX", "TENNSX");
            // kiếm tra hình ảnh đã tồn tại trong cơ sở dữ liệu chưa
            if (HINHANH.ContentLength > 0)
            {
                // lấy tên hình ảnh
                var fileName = Path.GetFileName(HINHANH.FileName);
                //lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    HINHANH.SaveAs(path);
                    /*Session["TENHINH"] = HINHANH.FileName;*/
                    sp.HINHANH = fileName;

                }


            }
            db.SANPHAMs.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Index", "admin");
        }
        [HttpGet]
        public ActionResult chinhsua(int? id)
        {
            // láy sản phẩm cần chỉnh sửa
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TENNCC), "MANCC", "TENNCC", sp.MANCC);
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MALOAISP), "MALOAISP", "TENLOAI", sp.MALOAISP);
            ViewBag.MANSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MANSX), "MANSX", "TENNSX", sp.MANSX);
            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult chinhsua(SANPHAM model)
        {
            ViewBag.MANCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TENNCC), "MANCC", "TENNCC", model.MANCC);
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MALOAISP), "MALOAISP", "TENLOAI", model.MALOAISP);
            ViewBag.MANSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MANSX), "MANSX", "TENNSX", model.MANSX);
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "admin");

        }
        [HttpGet]
        public ActionResult xoa(int? id)
        {
            // láy sản phẩm cần chỉnh sửa
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TENNCC), "MANCC", "TENNCC", sp.MANCC);
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MALOAISP), "MALOAISP", "TENLOAI", sp.MALOAISP);
            ViewBag.MANSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MANSX), "MANSX", "TENNSX", sp.MANSX);
            return View(sp);
        }
        [HttpPost]
        public ActionResult xoa(int id)
        {
            // láy sản phẩm cần chỉnh sửa
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            db.SANPHAMs.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Index", "admin");
        }
    }
}