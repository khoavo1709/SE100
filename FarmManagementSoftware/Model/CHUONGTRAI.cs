//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FarmManagementSoftware.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHUONGTRAI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHUONGTRAI()
        {
            this.CT_PHIEUSUACHUA = new HashSet<CT_PHIEUSUACHUA>();
            this.HEOs = new HashSet<HEO>();
        }
    
        public string MaChuong { get; set; }
        public string MaLoaiChuong { get; set; }
        public string TinhTrang { get; set; }
        public Nullable<int> SuaChuaToiDa { get; set; }
        public Nullable<int> SoLuongHeo { get; set; }
    
        public virtual LOAICHUONG LOAICHUONG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUSUACHUA> CT_PHIEUSUACHUA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HEO> HEOs { get; set; }
    }
}
