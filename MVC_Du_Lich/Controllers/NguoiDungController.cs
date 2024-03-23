using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net.Mail;
using MVC_Du_Lich.Design_Pattern.Structural_Pattern.Proxy;
using System.Security.Policy;

namespace MVC_Du_Lich.Controllers
{
    public class NguoiDungController : Controller
    {
        IProxy userProxy;

        public NguoiDungController()
        {
            userProxy = new UserAuthenticationProxy();
        }
        QLDULICHEntities database = new QLDULICHEntities();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OTP()
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

        public ActionResult DangNhap(FormCollection formCollection)
        {
            string username = formCollection["username"];
            string password = formCollection["password"];

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(username))
                {
                    ViewBag.Error = "Điện thoại không được để trống";
                }
                if (string.IsNullOrEmpty(password))
                {
                    ViewBag.Error = "Mật khẩu không được để trống";
                }

                if (ModelState.IsValid)
                {
                    //kiểm tra xem người dùng đăng nhập đúng tên đăng nhập và mật khẩu hay không
                    int chon = userProxy.AuthenticateUser(username, password);

                    var khach = database.KHACHHANGs.FirstOrDefault(k => k.DienThoaiKH == username);
                    var admin = database.ADMINs.FirstOrDefault(a => a.UserAdmin == username);

                    switch (chon)
                    {
                        case 0:
                            Session["Admin"] = admin;
                            return RedirectToAction("Index", "QuanLy");
                        case 1:
                            ViewBag.Error = "Mật khẩu không đúng";
                            break;
                        case 2:
                            ViewBag.ThongBao = "Đăng nhập thành công";
                            //lưu vào session
                            Session["KhachHang"] = khach;
                            Session["NameUser"] = khach.HoTenKH;
                            Session["MaKH"] = khach.MaKH;
                            return RedirectToAction("trangChu", "TrangChu");
                        case 3:
                            ViewBag.Error = "Số điện thoại này chưa đăng nhập";
                            break;
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
            return RedirectToAction("trangChu", "TrangChu");
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
                return RedirectToAction("ThongBaoDH", "NguoiDung");
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