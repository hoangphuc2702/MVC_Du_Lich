﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLDULICHEntities : DbContext
    {
        public QLDULICHEntities()
            : base("name=QLDULICHEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADMIN> ADMINs { get; set; }
        public virtual DbSet<CHAULUC> CHAULUCs { get; set; }
        public virtual DbSet<CTDATTOUR> CTDATTOURs { get; set; }
        public virtual DbSet<DIADIEM> DIADIEMs { get; set; }
        public virtual DbSet<DIEMDEN> DIEMDENs { get; set; }
        public virtual DbSet<DIEMDI> DIEMDIs { get; set; }
        public virtual DbSet<DONDATTOUR> DONDATTOURs { get; set; }
        public virtual DbSet<HANHKHACH> HANHKHACHes { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<LICHTRINH> LICHTRINHs { get; set; }
        public virtual DbSet<LOAIK> LOAIKS { get; set; }
        public virtual DbSet<LOAITOUR> LOAITOURs { get; set; }
        public virtual DbSet<PHUONGTIEN> PHUONGTIENs { get; set; }
        public virtual DbSet<TOUR> TOURs { get; set; }
        public virtual DbSet<KHACHSAN> KHACHSANs { get; set; }
    }
}
