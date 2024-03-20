using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Creational_Pattern.Builder
{
    public class TourBuilder : IBuilder
    {
        public string MaTour { get; set; }
        public string TenTour { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public string Hinh1 { get; set; }
        public string Hinh2 { get; set; }
        public string Hinh3 { get; set; }
        public string Hinh4 { get; set; }
        public string MoTa { get; set; }
        public int SoLuongConLai { get; set; }
        public System.DateTime NgayDiTour { get; set; }
        public System.DateTime NgayKetThuc { get; set; }
        public int MaDDi { get; set; }
        public int MaDDen { get; set; }
        public int MaLoaiTour { get; set; }
        public int MaPT { get; set; }
        public int MaLKS { get; set; }

        public TOUR build()
        {
            return new TOUR(MaTour, TenTour, Gia, SoLuong, Hinh1, Hinh2, Hinh3, Hinh4, MoTa, 
                SoLuongConLai, NgayDiTour, NgayKetThuc, MaDDi, MaDDen, MaLoaiTour, MaPT, MaLKS);
        }

        public IBuilder ThemGia(decimal gia)
        {
            this.Gia = gia;
            return this;
        }

        public IBuilder ThemHinh1(string hinh1)
        {
            this.Hinh1 = hinh1;
            return this;
        }

        public IBuilder ThemHinh2(string hinh2)
        {
            this.Hinh2 = hinh2;
            return this;
        }

        public IBuilder ThemHinh3(string hinh3)
        {
            this.Hinh3 = hinh3;
            return this;
        }

        public IBuilder ThemHinh4(string hinh4)
        {
            this.Hinh4 = hinh4;
            return this;
        }

        public IBuilder ThemMaDDen(int maDDen)
        {
            this.MaDDen = maDDen;
            return this;
        }

        public IBuilder ThemMaDDi(int maDDi)
        {
            this.MaDDi = maDDi;
            return this;
        }

        public IBuilder ThemMaLKS(int maLKS)
        {
            this.MaLKS = maLKS;
            return this;
        }

        public IBuilder ThemMaLoaiTour(int maLoaiTour)
        {
            this.MaLoaiTour = maLoaiTour;
            return this;
        }

        public IBuilder ThemMaPT(int maPT)
        {
            this.MaPT = MaPT;
            return this;
        }

        public IBuilder ThemMaTour(string maTour)
        {
            this.MaTour = maTour;
            return this;
        }

        public IBuilder ThemMoTa(string moTa)
        {
            this.MoTa = moTa;
            return this;
        }

        public IBuilder ThemNgayDiTour(DateTime ngayDiTour)
        {
            this.NgayDiTour = ngayDiTour;
            return this;
        }

        public IBuilder ThemNgayKetThuc(DateTime ngayKetThuc)
        {
            this.NgayKetThuc = ngayKetThuc;
            return this;
        }

        public IBuilder ThemSLConLai(int sLConLai)
        {
            this.SoLuongConLai = sLConLai;
            return this;
        }

        public IBuilder ThemSoLuong(int soLuong)
        {
            this.SoLuong = soLuong;
            return this;
        }

        public IBuilder ThemTenTour(string tenTour)
        {
            this.TenTour = tenTour;
            return this;
        }
    }
}