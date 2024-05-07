using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webbanhang.Models;
using System.IO;

namespace webbanhang.Controllers
{
    public class SANPHAMsController : Controller
    {
        private QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        // GET: SANPHAMs
        public ActionResult Index()
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.LOAISANPHAM).Include(s => s.NHACUNGCAP).Include(s => s.NHASANXUAT);
            return View(sANPHAMs.ToList());
        }

        // GET: SANPHAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: SANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAI");
            ViewBag.MANCC = new SelectList(db.NHACUNGCAPs, "MANCC", "TENNHACC");
            ViewBag.MANSX = new SelectList(db.NHASANXUATs, "MANSX", "TENNSX");
            return View();
        }

        // POST: SANPHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MASP,TENSP,DONGIA,NGAYCAPNHAP,CAUHINH,MOTA,HINHANH,SOLUONGTON,LUOTXEM,LUOTBINHCHON,LUOTBINHLUAN,SOLANMUA,MOI,MANCC,MANSX,MALOAISP,DAXOA")] SANPHAM sANPHAM, HttpPostedFileBase HINHANH)
        {
            if (ModelState.IsValid)
            {
                if (HINHANH != null)
                {
                    //Lấy tên file của hình được up lên
                    var fileName = Path.GetFileName(HINHANH.FileName);
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                    //Lưu tên
                    sANPHAM.HINHANH = $"~/Images/{fileName}";
                    //Save vào Images Folder
                    HINHANH.SaveAs(path);
                }
            }

            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAI", sANPHAM.MALOAISP);
            ViewBag.MANCC = new SelectList(db.NHACUNGCAPs, "MANCC", "TENNHACC", sANPHAM.MANCC);
            ViewBag.MANSX = new SelectList(db.NHASANXUATs, "MANSX", "TENNSX", sANPHAM.MANSX);
            return View(sANPHAM);
        }

        // GET: SANPHAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAI", sANPHAM.MALOAISP);
            ViewBag.MANCC = new SelectList(db.NHACUNGCAPs, "MANCC", "TENNHACC", sANPHAM.MANCC);
            ViewBag.MANSX = new SelectList(db.NHASANXUATs, "MANSX", "TENNSX", sANPHAM.MANSX);
            return View(sANPHAM);
        }

        // POST: SANPHAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MASP,TENSP,DONGIA,NGAYCAPNHAP,CAUHINH,MOTA,HINHANH,SOLUONGTON,LUOTXEM,LUOTBINHCHON,LUOTBINHLUAN,SOLANMUA,MOI,MANCC,MANSX,MALOAISP,DAXOA")] SANPHAM sANPHAM, HttpPostedFileBase HINHANH)
        {
            if (ModelState.IsValid)
            {
                if (HINHANH != null)
                {
                    //Lấy tên file của hình được up lên
                    var fileName = Path.GetFileName(HINHANH.FileName);
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                    //Lưu tên
                    sANPHAM.HINHANH = $"~/Images/{fileName}";
                    //Save vào Images Folder
                    HINHANH.SaveAs(path);
                }

            }
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAI", sANPHAM.MALOAISP);
            ViewBag.MANCC = new SelectList(db.NHACUNGCAPs, "MANCC", "TENNHACC", sANPHAM.MANCC);
            ViewBag.MANSX = new SelectList(db.NHASANXUATs, "MANSX", "TENNSX", sANPHAM.MANSX);
            return View(sANPHAM);
        }

        // GET: SANPHAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
