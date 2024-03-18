using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Du_Lich.Models
{
    public class ThanhVien : Controller
    {
        QLDULICHEntities db = new QLDULICHEntities();
        public int MaHK { get; set; }
        public string TenHK { get; set; }
        public double PTGiam { get; set; }
        public string MaTour { get; set; }
        public double DonGia { get; set; }
        public string TenTV { get; set; }
        public bool GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }


        //tính thành tiền
        public double ThanhTien(int SoLuong)
        {
            return PTGiam * DonGia * SoLuong;
        }

        public ThanhVien(int MaHK, string MaTour)
        {
            this.MaHK = MaHK;
            this.MaTour = MaTour;

            var tour = db.TOURs.Single(s => s.MaTour == this.MaTour);
            this.DonGia = (double)tour.Gia;

            //tìm hành khách có mã id và gán cho mặt hàng được mua
            var tv = db.HANHKHACHes.Single(s => s.MaHK == this.MaHK);
            this.TenHK = tv.TenHK;
            this.PTGiam = tv.PTGiam;
        }
    }
}