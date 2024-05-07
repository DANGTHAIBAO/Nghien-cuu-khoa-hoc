using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbanhang.Models;
namespace webbanhang.Controllers
{
    public class giohangController : Controller
    {

        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // lấy giỏ hàng
        public List<itemgiohang> laygiohang()
        {
            //giỏ hàng đã tồn tại
            List <itemgiohang> lstgiohang= Session["giohang"] as List<itemgiohang>;
            if (lstgiohang == null)
            {
                // nếu session giỏ hàng chưa tồn tại thì khởi tạo giỏ hàng
                lstgiohang = new List<itemgiohang>();
                Session["giohang"] = lstgiohang;
               
            }
            return lstgiohang;
        }
        // thêm giỏ hàng 
        public ActionResult themgiohang(int MASP ,string strURL)
        {
            // kiểm tra sản phẩm có tồn tại trong csdl hay ko
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP==MASP);
            if (sp == null)
            {
                // trang đường dẫn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy giỏ hàng1
            List<itemgiohang> lstgiohang = laygiohang();
            // trường hợp nếu sản phẩm đã tồn tại trong giỏ hàng
            itemgiohang spcheck = lstgiohang.SingleOrDefault(n => n.MASP == MASP);
            if (spcheck != null)
            {
                // kiểm tra số lượng tồn trước khi đặt hàng
                if (sp.SOLUONGTON < spcheck.SOLUONG)
                {
                    return View("thongbao");
                }
                spcheck.SOLUONG++;
                spcheck.THANHTIEN = spcheck.SOLUONG * spcheck.DONGIA;
                return Redirect(strURL);
            }
           
            itemgiohang itemGH = new itemgiohang(MASP);
            if (sp.SOLUONGTON < itemGH.SOLUONG)
            {
                return View("thongbao");
            }
            lstgiohang.Add(itemGH);
            return Redirect(strURL);
        }
        // tỉnh tổng tiền
        public double tinhtongsoluong()
        {
            // lấy giỏ hàng
            List<itemgiohang> lstgiohang = Session["giohang"] as List<itemgiohang>;
            if (lstgiohang == null)
            {
                return 0;
            }
            return lstgiohang.Sum(n => n.SOLUONG);
        }
        public decimal tinhtongtien()
        {
            // lấy giỏ hàng
            List<itemgiohang> lstgiohang = Session["giohang"] as List<itemgiohang>;
            if (lstgiohang == null)
            {
                return 0;
            }
            return lstgiohang.Sum(n => n.THANHTIEN);
        }

       
        public ActionResult giohangpartial()
        {
            if (tinhtongsoluong() == 0)
            {
                ViewBag.tongsoluong = 0;
                ViewBag.tongtien = 0;
                return PartialView();
            }
            ViewBag.tongsoluong = tinhtongsoluong();
            ViewBag.tongtien = tinhtongtien();
            return PartialView();
        }
        // GET: giohang
        public ActionResult xemgiohang()
        {

            List<itemgiohang> lstgiohang = laygiohang();
            return View(lstgiohang);
        }
        [HttpGet]
        public ActionResult suagiohang(int MASP)
        {
            // kiểm tra session giỏ hàng đã tồn tại chưa
            if (Session["giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // kiểm tra sản phẩm có tồn tại trong cơ sở dữ liệu hay ko
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == MASP);
            if (sp == null)
            {
                // trang đường dẫn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy ra list giỏ hàng từ session
            List<itemgiohang> lstgiohang = laygiohang();
            itemgiohang spCheck = lstgiohang.SingleOrDefault(n => n.MASP == MASP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.giohang = lstgiohang;
            // nếu tồn tại rồi
            return View(spCheck);
        }
        //xử lý cập nhật giỏ hàng
        [HttpPost]
        public ActionResult capnhatgiohang(itemgiohang itemGH)
        {
            // kiểm tra số lượng tồn
            SANPHAM spcheck = db.SANPHAMs.Single(n => n.MASP == itemGH.MASP);
            if (spcheck.SOLUONGTON < itemGH.SOLUONG)
            {
                return View("thongbao");
            }
            // CẬP NHẬT SỐ LƯỢNG TRONG SESSION GIỎ HÀNG
            //BƯỚC 1 LẤY LIST<GIOHANG> TỪ SESSION["GIOHANG"]
            List<itemgiohang> lstGH = laygiohang();
            // BƯỚC 2 LẤY SẢN PHẨM CẦN CẬP NHẬP TỪ TRÔNG LIST<ITEMGIOHANG> RA
            itemgiohang itemGHUpdate = lstGH.Find(n => n.MASP == itemGH.MASP);
            // BƯỚC 3 LẤY TIỀN HÀNH CẬP NHẬT LẠI SỐ LƯỢNG CŨNG THÀNH TIỀN
            itemGHUpdate.SOLUONG = itemGH.SOLUONG;
            itemGHUpdate.THANHTIEN = itemGHUpdate.SOLUONG * itemGHUpdate.DONGIA;
            return RedirectToAction("xemgiohang");

        }
        public ActionResult xoagiohang(int MASP)
        {
            // kiểm tra session giỏ hàng đã tồn tại chưa
            if (Session["giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // kiểm tra sản phẩm có tồn tại trong cơ sở dữ liệu hay ko
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == MASP);
            if (sp == null)
            {
                // trang đường dẫn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy ra list giỏ hàng từ session
            List<itemgiohang> lstgiohang = laygiohang();
            itemgiohang spCheck = lstgiohang.SingleOrDefault(n => n.MASP == MASP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // XÓA item trong gio hang
            lstgiohang.Remove(spCheck);
            return RedirectToAction("xemgiohang");
        }
        public ActionResult dathang(KHACHHANG kh)
        {
            // kiểm tra session giỏ hàng tồn tại chưa
            if (Session["giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KHACHHANG khang = new KHACHHANG();
            if (Session["TAIKHOAN"] == null)
            {
                //THÊM 1 KHÁCH HÀNG VÀO BẢNG KHÁCH HÀNG dối với khách hàng ch có tài khoản
                
                khang = kh;
                db.KHACHHANGs.Add(khang);
                db.SaveChanges();
            }
            else
            {
                // đối với khách hàng là thành viên
                THANHVIEN tv = Session["TAIKHOAN"] as THANHVIEN;
                khang.MATHANHVIEN = tv.MATHANHVIEN;
                khang.TENKH = tv.HOTEN;
                khang.DIACHI = tv.DIACHI;
                khang.EMAI = tv.EMAIL;
                khang.SODIENTHOAI = tv.SODIENTHOAI;
                /*khang.MATHANHVIEN = tv.MALOAITV;*/
                db.KHACHHANGs.Add(khang);
                db.SaveChanges();
            }
            // thêm đơn hàng
            DONDATHANG ddh = new DONDATHANG();
            ddh.MAKH = khang.MAKH;
            ddh.NGAYDAT = DateTime.Now;
            ddh.TINHTRANGGIAOHANG = 0;
            ddh.DATHANHTOAN = false;
            ddh.UUDAI = 0;
            ddh.DAXOA = false;
            ddh.DAHUY = false;
            db.DONDATHANGs.Add(ddh);
            db.SaveChanges();
            // thêm chi tiết đơn đặt hàng
            List<itemgiohang> lstGH = laygiohang();
            foreach(var item in lstGH)
            {
                CHITIETDONDATHANG ctdh = new CHITIETDONDATHANG();
                ctdh.MADDH = ddh.MADDH;
                ctdh.MASP = item.MASP;
                ctdh.TENSP = item.TENSP;
                ctdh.SOLUONG = item.SOLUONG;
                ctdh.DONGIA = item.DONGIA;
                db.CHITIETDONDATHANGs.Add(ctdh);
                
            }
            db.SaveChanges();
            Session["giohang"] = null;
            return RedirectToAction("xemgiohang");
        }
        public ActionResult dangxuat()
        {
            Session["TAIKHOAN"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
  
}