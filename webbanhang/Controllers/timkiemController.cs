using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbanhang.Models;
using PagedList;

namespace webbanhang.Controllers
{
    public class timkiemController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: timkiem
        [HttpGet]
        public ActionResult kqtimkiem(string stukhoa,int?page)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            // tạo biến số sản phẩm trên trang
            int PageSize = 6;
            // tạo biến thứ 2 số phân trang hiện tại
            int PageNumber = (page ?? 1);
            //tìm kiếm theo tên sản phẩm 
        
            var lstsp = db.SANPHAMs.Where(n => n.TENSP.Contains(stukhoa));
            ViewBag.stukhoa = stukhoa;
            return View(lstsp.OrderBy(n=>n.TENSP).ToPagedList(PageNumber,PageSize));
        }

        [HttpPost]
        public ActionResult kqtimkiem(string stukhoa, int? page,FormCollection f)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            // tạo biến số sản phẩm trên trang
            int PageSize = 6;
            // tạo biến thứ 2 số phân trang hiện tại
            int PageNumber = (page ?? 1);
            //tìm kiếm theo tên sản phẩm 

            var lstsp = db.SANPHAMs.Where(n => n.TENSP.Contains(stukhoa));
            ViewBag.stukhoa = stukhoa;
            return View(lstsp.OrderBy(n => n.TENSP).ToPagedList(PageNumber, PageSize));
        }

    }
}