using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MVC_Du_Lich.Controllers
{
    public class UserController : Controller
    {
        QLDULICHEntities database = new QLDULICHEntities();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy(int? loai)
        {
            if(loai != null)
            {
                ViewBag.loai = loai;
            }
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.HoTenKH))
                {
                    ViewBag.Error = "Họ tên không được để trống";
                }
                if (string.IsNullOrEmpty(kh.EmailKH))
                {
                    ViewBag.Error = "Email không được để trống";
                }
                if (string.IsNullOrEmpty(kh.MatKhau))
                {
                    ViewBag.Error = "Mật khẩu không được để trống";
                }
                if (string.IsNullOrEmpty(kh.DiaChiKH))
                {
                    ViewBag.Error = "Địa chỉ không được để trống";
                }
                if (string.IsNullOrEmpty(kh.DienThoaiKH))
                {
                    ViewBag.Error = "Số điện thoại không được để trống";
                }
                if (kh.XacNhanMatKhau != kh.XacNhanMatKhau)
                {
                    ViewBag.Error = "Mật khẩu không trùng khớp";
                }

                //kiểm tra xem có người nào đã đăng ký với điện thoại này hay chưa
                var khachhang = database.KHACHHANGs.FirstOrDefault(k => k.DienThoaiKH == kh.DienThoaiKH);
                if (khachhang != null)
                {
                    ModelState.AddModelError(string.Empty, "Đã có người đăng ký tên này");
                }

                if (ModelState.IsValid)
                {
                    database.KHACHHANGs.Add(kh);
                    database.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("DangNhap");
        }

        [HttpGet]
        public ActionResult DangNhap(int? loai)
        {
            if (loai != null)
            {
                ViewBag.loai = loai;
            }
            return View();
        }

        public ActionResult DangNhap(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.DienThoaiKH))
                {
                    ViewBag.Error = "Điện thoại không được để trống";
                }
                if (string.IsNullOrEmpty(kh.MatKhau))
                {
                    ViewBag.Error = "Mật khẩu không được để trống";
                }

                if (ModelState.IsValid)
                {
                    //kiểm tra xem người dùng đăng nhập đúng tên đăng nhập và mật khẩu hay không
                    var khach = database.KHACHHANGs.FirstOrDefault(k => k.DienThoaiKH == kh.DienThoaiKH);
                    if (khach != null)
                    {
                        if (khach.MatKhau == kh.MatKhau)
                        {
                            ViewBag.ThongBao = "Đăng nhập thành công";
                            //lưu vào session
                            Session["KhachHang"] = khach;
                            Session["NameUser"] = khach.HoTenKH;
                            Session["MaKH"] = khach.MaKH;
                        }
                        else
                        {
                            ViewBag.Error = "Mật khẩu không đúng";
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Số điện thoại này chưa đăng nhập";
                    }

                }
                return View();
            }
            return RedirectToAction("DangNhap");
        }

        public ActionResult LogOut()
        {
            Session["NameUser"] = null;
            Session["KhachHang"] = null;
            Session["MaKH"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ThongTinCaNhan()
        {
            return View();
        }

        public ActionResult ThongBaoDH()
        {
            return View();
        }

        public ActionResult LSHoaDon(int? page)
        {
            int makh = (int)Session["MaKH"];
            // Tiếp tục xử lý với makh
            var dsHd1 = database.DONDATTOURs.Where(s => s.MaKH == makh).Select(s => s.SoHD).ToList();
            if (dsHd1.Count == 0)
            {
                return RedirectToAction("ThongBaoDH", "User");
            }
            else
            {
                var dsHd = database.CTDATTOURs.Where(s1 => dsHd1.Contains(s1.SoHD)).ToList(); // Sử dụng Contains
                //Tạo biến cho biết số sách mỗi trang
                int pageSize = 7;
                //Tạo biến số trang
                int pageNum = (page ?? 1);
                return View(dsHd.OrderBy(hd => hd.SoHD).ToPagedList(pageNum, pageSize));
            }
        }

        public ActionResult ChiTietHd(int id)
        {
            var hd = database.DONDATTOURs.FirstOrDefault(s => s.SoHD == id);
            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hd);
        }
    }
}