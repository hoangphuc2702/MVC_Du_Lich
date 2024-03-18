﻿using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace MVC_Du_Lich.Controllers
{
    public class DatTourController : Controller
    {
        QLDULICHEntities database = new QLDULICHEntities();

        //// GET: GioHang
        //public List<ThanhVien> LayGioHang()
        //{
        //    List<ThanhVien> gioHang = Session["GioHang"] as List<ThanhVien>;

        //    //nếu giỏ hàng chưa tồn tại thì tạo mới và đưa vào session
        //    if (gioHang == null)
        //    {
        //        gioHang = new List<ThanhVien>();
        //        Session["GioHang"] = gioHang;
        //    }
        //    return gioHang;
        //}

        //public ActionResult ThemHanhKhachVaoDon(int MaHK, string MaTour, int choice)
        //{
        //    //Lây giỏ hàng hiện tại
        //    List<ThanhVien> gioHang = LayGioHang();
        //    //Kiểm tra xem có tồn tại có mặt hàng trong giỏ hay chưa
        //    //Nếu có tăng số lượng lên 1 và ngược lại thêm vào giỏ
        //    ThanhVien sanPham = gioHang.FirstOrDefault(s => s.MaHK == MaHK);
        //    var tour = database.TOURs.FirstOrDefault(k => k.MaTour == MaTour);
        //    Session["MaTour"] = tour;

        //    if (choice == 1) //1 là tăng, 2 là giảm
        //    {
        //        if (sanPham == null)//sản phẩm chưa có trong giỏ
        //        {
        //            sanPham = new ThanhVien(MaHK, MaTour);
        //            gioHang.Add(sanPham);
        //        }
        //        else
        //        {
        //            sanPham.SoLuong++;
        //        }
        //    }
        //    else
        //    {
        //        if (sanPham.SoLuong == 0)//sản phẩm có trong giỏ
        //        {
        //            gioHang.RemoveAll(s => s.MaHK == MaHK);
        //        }
        //        else
        //        {
        //            sanPham.SoLuong--;
        //        }
        //    }
        //    return RedirectToAction("HienThiCTTour");
        //}

        //private int TinhTongSL(int maHK)
        //{
        //    int tongSL = 0;
        //    List<ThanhVien> gioHang = LayGioHang();

        //    if (gioHang != null)
        //    {
        //        // Tính tổng số lượng của các thành viên có mã HK bằng mã HK được truyền vào
        //        tongSL = gioHang.Where(tv => tv.MaHK == maHK).Sum(tv => tv.SoLuong);
        //    }

        //    return tongSL;
        //}

        //private double TinhTongTienHK(int maHK)
        //{
        //    double thanhTien = 0;
        //    List<ThanhVien> gioHang = LayGioHang();

        //    if (gioHang != null)
        //    {
        //        // Lấy đối tượng ThanhVien có mã HK tương ứng
        //        ThanhVien thanhVien = gioHang.FirstOrDefault(tv => tv.MaHK == maHK);

        //        if (thanhVien != null)
        //        {
        //            // Tính thành tiền dựa trên giá và số lượng
        //            thanhTien = thanhVien.ThanhTien();
        //        }
        //    }

        //    return thanhTien;
        //}


        //private double TinhTongTien()
        //{
        //    double TongTien = 0;
        //    List<ThanhVien> gioHang = LayGioHang();
        //    if (gioHang != null)
        //    {
        //        TongTien = gioHang.Sum(sp => sp.ThanhTien());
        //    }
        //    return TongTien;
        //}

        //public ActionResult HienThiCTTour()
        //{
        //    List<ThanhVien> gioHang = LayGioHang();
        //    ViewBag.TongSLNL = TinhTongSL(1);
        //    ViewBag.TongSLTE = TinhTongSL(2);
        //    ViewBag.TongSLTN = TinhTongSL(3);
        //    ViewBag.TongSLEB = TinhTongSL(4);

        //    ViewBag.DonGiaNL = TinhTongTienHK(1);
        //    ViewBag.DonGiaTE = TinhTongTienHK(2);
        //    ViewBag.DonGiaTN = TinhTongTienHK(3);
        //    ViewBag.DonGiaEB = TinhTongTienHK(4);

        //    ViewBag.TongTien = TinhTongTien();
        //    return View(gioHang);
        //}

        ////xác nhận đơn hàng
        //public ActionResult DongYDatHang()
        //{
        //    KHACHHANG khach = Session["TaiKhoan"] as KHACHHANG;
        //    List<ThanhVien> danhSachThanhVien = LayGioHang();

        //    DONDATTOUR DonHang = new DONDATTOUR();
        //    if (Session["TaiKhoan"] != null)
        //    {
        //        DonHang.MaKH = khach.MaKH;
        //        DonHang.HoTenKH = khach.HoTenKH;
        //        DonHang.DiaChiKH = khach.DiaChiKH;
        //        DonHang.SDT_KH = khach.DienThoaiKH;
        //        DonHang.EmailKH = khach.EmailKH;
        //    }
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (string.IsNullOrEmpty(DonHang.HoTenKH))
        //            {
        //                ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
        //            }
        //            if (string.IsNullOrEmpty(DonHang.EmailKH))
        //            {
        //                ModelState.AddModelError(string.Empty, "Email không được để trống");
        //            }
        //            if (string.IsNullOrEmpty(DonHang.DiaChiKH))
        //            {
        //                ModelState.AddModelError(string.Empty, "Địa chỉ không được để trống");
        //            }
        //            if (string.IsNullOrEmpty(DonHang.SDT_KH))
        //            {
        //                ModelState.AddModelError(string.Empty, "Số điện thoại không được để trống");
        //            }
        //            if (string.IsNullOrEmpty(DonHang.EmailKH))
        //            {
        //                ModelState.AddModelError(string.Empty, "Email không được để trống");
        //            }
        //        }
        //    }

        //    DonHang.TongTien = (decimal)TinhTongTien();
        //    DonHang.HTThanhToan = false;
        //    DonHang.TrangThai = false;

        //    database.DONDATTOURs.Add(DonHang);
        //    database.SaveChanges();

        //    //thêm từng chi tiết cho đơn hàng
        //    foreach (var thanhVien in danhSachThanhVien)
        //    {
        //        CTDATTOUR chiTietDonHang = new CTDATTOUR
        //        {
        //            SoHD = DonHang.SoHD,
        //            MaHK = thanhVien.MaHK,
        //            DonGia = (decimal)thanhVien.DonGia,
        //            PTGiam = thanhVien.PTGiam,
        //            TenTV = thanhVien.TenTV,
        //            GioiTinh = thanhVien.GioiTinh,
        //            NgaySinh = thanhVien.NgaySinh
        //        };

        //        database.CTDATTOURs.Add(chiTietDonHang);
        //    }

        //    //xoá sản phẩm trong giỏ hàng
        //    Session["GioHang"] = null;
        //    return RedirectToAction("HoanThanhDonHang");
        //}

        //public ActionResult HoanThanhDonHang()
        //{
        //    return View();
        //}

        public ActionResult NguoiLon(int loai)
        {
            var loai1 = database.HANHKHACHes.FirstOrDefault(s => s.MaHK == loai);
            return PartialView(loai1);
        }

        public ActionResult TreEm(int loai)
        {
            var loai1 = database.HANHKHACHes.FirstOrDefault(s => s.MaHK == loai);
            return PartialView(loai1);
        }

        public ActionResult TreNho(int loai)
        {
            var loai1 = database.HANHKHACHes.FirstOrDefault(s => s.MaHK == loai);
            return PartialView(loai1);
        }

        public ActionResult EmBe(int loai)
        {
            var loai1 = database.HANHKHACHes.FirstOrDefault(s => s.MaHK == loai);
            return PartialView(loai1);
        }





        // GET: GioHang

        public List<ThanhVien> LayGioHang()
        {
            List<ThanhVien> gioHang = Session["GioHang"] as List<ThanhVien>;

            //nếu giỏ hàng chưa tồn tại thì tạo mới và đưa vào session
            if (gioHang == null)
            {
                gioHang = new List<ThanhVien>();
                Session["GioHang"] = gioHang;
            }
            return gioHang;
        }

        public ActionResult ThemHanhKhachVaoDon(string MaTour, CTDATTOUR ctdattour)
        {

            //Lây giỏ hàng hiện tại
            List<ThanhVien> gioHang = LayGioHang();
            //Kiểm tra xem có tồn tại có mặt hàng trong giỏ hay chưa
            //Nếu có tăng số lượng lên 1 và ngược lại thêm vào giỏ
            ThanhVien sanPham;
            int MaHK;
            if (DateTime.Now.Year - ctdattour.NgaySinh.Value.Year >= 18)
            {
                sanPham = gioHang.FirstOrDefault(s => s.MaHK == 1);
                MaHK = 1;
            } 
            else if (DateTime.Now.Year - ctdattour.NgaySinh.Value.Year >= 5)
            {
                sanPham = gioHang.FirstOrDefault(s => s.MaHK == 2);
                MaHK = 2;
            } 
            else if (DateTime.Now.Year - ctdattour.NgaySinh.Value.Year >= 2)
            {
                sanPham = gioHang.FirstOrDefault(s => s.MaHK == 3);
                MaHK = 3;
            }
            else
            {
                sanPham = gioHang.FirstOrDefault(s => s.MaHK == 4);
                MaHK = 4;
            }

            sanPham = new ThanhVien(MaHK, MaTour);

            sanPham.TenTV = ctdattour.TenTV;
            sanPham.GioiTinh = (bool)ctdattour.GioiTinh;
            sanPham.NgaySinh = (DateTime)ctdattour.NgaySinh;
            gioHang.Add(sanPham);


            return RedirectToAction("HienThiCTTour");
        }

        [HttpGet]
        public ActionResult XoaMatHang(string TenTV)
        {
            List<ThanhVien> gioHang = LayGioHang();

            var tv = gioHang.FirstOrDefault(s => s.TenTV == TenTV);
            if (tv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tv);
        }

        [HttpPost, ActionName("XoaMatHang")]
        public ActionResult DeleteConfirmedNXB(string TenTV)
        {
            List<ThanhVien> gioHang = LayGioHang();

            //Lấy sản phẩm trong giỏ hàng
            var sanpham = gioHang.FirstOrDefault(s => s.TenTV == TenTV);
            if (sanpham != null)
            {
                gioHang.RemoveAll(s => s.TenTV == TenTV);
            }
            return RedirectToAction("HienThiCTTour");
        }

        private int TinhTongSL(int maHK)
        {
            int tongSL = 0;
            List<ThanhVien> gioHang = LayGioHang();

            if (gioHang != null)
            {
                // Tính tổng số lượng của các thành viên có mã HK bằng mã HK được truyền vào
                tongSL = gioHang.Count(tv => tv.MaHK == maHK);
            }

            return tongSL;
        }

        private double TinhTongTienHK(int maHK)
        {
            double thanhTien = 0;
            List<ThanhVien> gioHang = LayGioHang();

            if (gioHang != null)
            {
                // Lấy đối tượng ThanhVien có mã HK tương ứng
                ThanhVien thanhVien = gioHang.FirstOrDefault(tv => tv.MaHK == maHK);

                if (thanhVien != null)
                {
                    // Tính thành tiền dựa trên giá và số lượng
                    thanhTien = thanhVien.ThanhTien(TinhTongSL(maHK));
                }
            }

            return thanhTien;
        }


        private double TinhTongTien()
        {
            double TongTien = 0;
            List<ThanhVien> gioHang = LayGioHang();
            if (gioHang != null)
            {
                double TongTien1 = gioHang.Sum(sp => sp.ThanhTien(1));
                double TongTien2 = gioHang.Sum(sp => sp.ThanhTien(2));
                double TongTien3 = gioHang.Sum(sp => sp.ThanhTien(3));
                double TongTien4 = gioHang.Sum(sp => sp.ThanhTien(4));
                TongTien = TongTien1 + TongTien2 + TongTien3 + TongTien4;
            }
            return TongTien;
        }

        public ActionResult DatTour(string MaTour)
        {
            if (Session["KhachHang"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }

            List<ThanhVien> gioHang = LayGioHang();

            var tour = database.TOURs.FirstOrDefault(k => k.MaTour == MaTour);
            Session["MaTour"] = tour;
            return RedirectToAction("HienThiCTTour");
        }

        public ActionResult HienThiCTTour()
        {
            List<ThanhVien> gioHang = LayGioHang();
            
            ViewBag.GioiTinh = new SelectList("Nam", "Nữ");

            ViewBag.TongSL1 = TinhTongSL(1);
            ViewBag.TongSL2 = TinhTongSL(2);
            ViewBag.TongSL3 = TinhTongSL(3);
            ViewBag.TongSL4 = TinhTongSL(4);

            ViewBag.DonGiaNL = TinhTongTienHK(1);
            ViewBag.DonGiaTE = TinhTongTienHK(2);
            ViewBag.DonGiaTN = TinhTongTienHK(3);
            ViewBag.DonGiaEB = TinhTongTienHK(4);

            ViewBag.TongTien = TinhTongTien();

            ViewBag.MaKH = new SelectList(database.KHACHHANGs, "MaKH", "HoTenKH");

            return View(gioHang);
        }

        [HttpGet]
        public ActionResult CapNhatThanhVien(string TenTV)
        {
            List<ThanhVien> gioHang = LayGioHang();

            var sanpham = gioHang.FirstOrDefault(s => s.TenTV == TenTV);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }

        [HttpPost]
        public ActionResult CapNhatThanhVien(string TenTV, CTDATTOUR ctdattour)
        {
            List<ThanhVien> gioHang = LayGioHang();

            var sanpham = gioHang.FirstOrDefault(s => s.TenTV == TenTV);
            if (sanpham != null)
            {
                sanpham.TenTV = ctdattour.TenTV;
                sanpham.GioiTinh = (bool)ctdattour.GioiTinh;
                sanpham.NgaySinh = (DateTime)ctdattour.NgaySinh;
            }
            return RedirectToAction("HienThiCTTour");
        }

        public ActionResult DatHang()
        {
            List<ThanhVien> gioHang = LayGioHang();
            ViewBag.TongSL1 = TinhTongSL(1);
            ViewBag.TongSL2 = TinhTongSL(2);
            ViewBag.TongSL3 = TinhTongSL(3);
            ViewBag.TongSL4 = TinhTongSL(4);

            ViewBag.DonGiaNL = TinhTongTienHK(1);
            ViewBag.DonGiaTE = TinhTongTienHK(2);
            ViewBag.DonGiaTN = TinhTongTienHK(3);
            ViewBag.DonGiaEB = TinhTongTienHK(4);

            ViewBag.TongTien = TinhTongTien();
            return View(gioHang);
        }

        //xác nhận đơn hàng
        public ActionResult DongYDatHang(string PTThanhToan)
        {
            KHACHHANG khach = Session["KhachHang"] as KHACHHANG;
            TOUR tour = Session["MaTour"] as TOUR;

            List<ThanhVien> gioHang = LayGioHang();

            DONDATTOUR DonHang = new DONDATTOUR();
            DonHang.MaKH = khach.MaKH;
            DonHang.HoTenKH = khach.HoTenKH;
            DonHang.DiaChiKH = khach.DiaChiKH;
            DonHang.SDT_KH = khach.DienThoaiKH;
            DonHang.EmailKH = khach.EmailKH;
            DonHang.MaTour = tour.MaTour;

            DonHang.TongTien = (decimal)TinhTongTien();
            DonHang.HTThanhToan = false;
            DonHang.TrangThai = false;
            DonHang.PTThanhToan = Convert.ToBoolean(PTThanhToan);

            database.DONDATTOURs.Add(DonHang);
            database.SaveChanges();

            //thêm từng chi tiết cho đơn hàng
            foreach (var thanhVien in gioHang)
            {
                CTDATTOUR chiTietDonHang = new CTDATTOUR
                {
                    SoHD = DonHang.SoHD,
                    MaTour = tour.MaTour,
                    MaHK = thanhVien.MaHK,
                    DonGia = (decimal)thanhVien.DonGia,
                    PTGiam = thanhVien.PTGiam,
                    TenTV = thanhVien.TenTV,
                    GioiTinh = thanhVien.GioiTinh,
                    NgaySinh = thanhVien.NgaySinh
                };
                database.CTDATTOURs.Add(chiTietDonHang);
            }
            database.SaveChanges();

            //xoá sản phẩm trong giỏ hàng
            Session["GioHang"] = null;
            return RedirectToAction("HoanThanhDonHang");
        }

        public ActionResult HoanThanhDonHang()
        {
            return View();
        }
    }
}