using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using webbanhang.Models;
using System.Net;
using System.Net.Mail;

namespace webbanhang.Controllers
{
    public class quanlydonhangController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: quanlydonhang
        public ActionResult chuathanhtoan()
        {
            // lấy danh sách các đơn hàng chưa duyệt
            var lstchuathanhtoan = db.DONDATHANGs.Where(n => n.DATHANHTOAN == false).OrderByDescending(n => n.NGAYDAT);

            return View(lstchuathanhtoan);
        }
        public ActionResult chuagiao()
        {
            // lấy danh sách các đơn hàng chưa duyệt
            var lstchuagiao = db.DONDATHANGs.Where(n => n.TINHTRANGGIAOHANG == 0 && n.DATHANHTOAN==true).OrderByDescending(n => n.NGAYDAT);

            return View(lstchuagiao);
        }
        public ActionResult dagiaodathanhtoan()
        {
            // lấy danh sách các đơn hàng chưa duyệt
            var lstxong = db.DONDATHANGs.Where(n => n.TINHTRANGGIAOHANG == 1 && n.DATHANHTOAN == true).OrderByDescending(n => n.NGAYDAT);

            return View(lstxong);
        }
        [HttpGet]
        public ActionResult duyetdonhang(int ? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            DONDATHANG model = db.DONDATHANGs.SingleOrDefault(n => n.MADDH == id);

            if (model == null) return HttpNotFound();
            // lấy danh sách các đơn hàng chưa duyệt
            var lstchitietDH = db.CHITIETDONDATHANGs.Where(n => n.MADDH == id);
            ViewBag.lstchitietdh = lstchitietDH;
            return View(model);
        }
        [HttpPost]
        public ActionResult duyetdonhang(DONDATHANG ddh)
        {
            DONDATHANG ddupdate = db.DONDATHANGs.Single(n => n.MADDH == ddh.MADDH);
            ddupdate.DATHANHTOAN = ddh.DATHANHTOAN;
            ddupdate.TINHTRANGGIAOHANG = ddh.TINHTRANGGIAOHANG;
            db.SaveChanges();
            var lstchitietDH = db.CHITIETDONDATHANGs.Where(n => n.MADDH == ddh.MADDH);
            ViewBag.lstchitietdh = lstchitietDH;
            // gửi khách hàng 1 mail để xác nhân việc thanh toán
            
            /*string sTAIKHOAN = db.KHACHHANGs.Emai*/
/*            GuiEmail("xác nhận đơn hàng", "nguyentuananh02091992@gmail.com", "nguyentuananh02091992@gmail.com", "1234", "Đơn hàng của bạn đã được đặt thành công");
*/            return View(ddupdate);
        }
        public void GuiEmail(string title,string toemail,string fromemail,string password,string content)
        {
           
            MailMessage mail = new MailMessage();
            mail.To.Add(toemail);// địa chỉ nhận
            mail.From = new MailAddress(toemail);// địa chỉ gửi
            mail.Subject = title;
            mail.Body = content;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(fromemail, password);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}