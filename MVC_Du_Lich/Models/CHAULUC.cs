//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Du_Lich.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHAULUC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHAULUC()
        {
            this.DIEMDENs = new HashSet<DIEMDEN>();
        }
    
        public int MaChauLuc { get; set; }
        public string TenChauLuc { get; set; }
        public Nullable<int> MaLoaiTour { get; set; }
    
        public virtual LOAITOUR LOAITOUR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIEMDEN> DIEMDENs { get; set; }
    }
}
