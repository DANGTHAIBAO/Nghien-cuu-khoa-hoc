//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webbanhang.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHITIETDONDATHANG
    {
        public int MACHITIETDDH { get; set; }
        public Nullable<int> MADDH { get; set; }
        public Nullable<int> MASP { get; set; }
        public string TENSP { get; set; }
        public Nullable<int> SOLUONG { get; set; }
        public Nullable<decimal> DONGIA { get; set; }
    
        public virtual DONDATHANG DONDATHANG { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}
