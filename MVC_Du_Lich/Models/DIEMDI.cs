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
    
    public partial class DIEMDI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DIEMDI()
        {
            this.TOURs = new HashSet<TOUR>();
        }
    
        public int MaDDi { get; set; }
        public string TenDDi { get; set; }
        public Nullable<int> MaLoaiTour { get; set; }
    
        public virtual LOAITOUR LOAITOUR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOUR> TOURs { get; set; }
    }
}
