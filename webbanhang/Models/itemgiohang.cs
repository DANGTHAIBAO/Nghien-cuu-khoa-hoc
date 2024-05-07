using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webbanhang.Models
{
    public class itemgiohang
    {
        public int MASP { get; set; }
        public string TENSP { get; set; }
        public int SOLUONG { get; set; }
        public decimal DONGIA { get; set; }
        public decimal THANHTIEN { get; set; }
        public string HINHANH { get; set; }
        public itemgiohang (int IMASP)//số lượng
        {
            using(QuanLyBanHangEntities db=new QuanLyBanHangEntities())
            {
                this.MASP = IMASP;
                SANPHAM SP = db.SANPHAMs.Single(n => n.MASP == IMASP);
                this.TENSP = SP.TENSP;
                this.HINHANH = SP.HINHANH;
                this.DONGIA = SP.DONGIA.Value;
                this.SOLUONG = 1;
                this.THANHTIEN = DONGIA * SOLUONG;

            }
        }
        public itemgiohang(int IMASP,int sl)//số lượng
        {
            using (QuanLyBanHangEntities db = new QuanLyBanHangEntities())
            {
                this.MASP = IMASP;
                SANPHAM SP = db.SANPHAMs.Single(n => n.MASP == IMASP);
                this.TENSP = SP.TENSP;
                this.HINHANH = SP.HINHANH;
                this.DONGIA = SP.DONGIA.Value;
                this.SOLUONG = sl;
                this.THANHTIEN = DONGIA * SOLUONG;

            }
        }
        public itemgiohang()
        {

        }
    }
}