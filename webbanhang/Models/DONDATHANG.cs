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
    
    public partial class DONDATHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONDATHANG()
        {
            this.CHITIETDONDATHANGs = new HashSet<CHITIETDONDATHANG>();
        }
    
        public int MADDH { get; set; }
        public Nullable<System.DateTime> NGAYDAT { get; set; }
        public Nullable<int> TINHTRANGGIAOHANG { get; set; }
        public Nullable<System.DateTime> NGAYGIAO { get; set; }
        public Nullable<bool> DATHANHTOAN { get; set; }
        public Nullable<int> MAKH { get; set; }
        public Nullable<int> UUDAI { get; set; }
        public Nullable<bool> DAHUY { get; set; }
        public Nullable<bool> DAXOA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONDATHANG> CHITIETDONDATHANGs { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
