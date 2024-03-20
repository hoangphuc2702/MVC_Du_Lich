using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Creational_Pattern.Builder
{
    public interface IBuilder
    {
        IBuilder ThemMaTour(string maTour);
        IBuilder ThemTenTour(string tenTour);
        IBuilder ThemGia(decimal gia);
        IBuilder ThemSoLuong(int soLuong);
        IBuilder ThemHinh1(string hinh1);
        IBuilder ThemHinh2(string hinh2);
        IBuilder ThemHinh3(string hinh3);
        IBuilder ThemHinh4(string hinh4);
        IBuilder ThemMoTa(string moTa);
        IBuilder ThemSLConLai(int sLConLai);
        IBuilder ThemNgayDiTour(DateTime ngayDiTour);
        IBuilder ThemNgayKetThuc(DateTime ngayKetThuc);
        IBuilder ThemMaDDi(int maDDi);
        IBuilder ThemMaDDen(int maDDen);
        IBuilder ThemMaLoaiTour(int maLoaiTour);
        IBuilder ThemMaPT(int maPT);
        IBuilder ThemMaLKS(int maLKS);
        TOUR build();
    }
}