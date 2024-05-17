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
using MVC_Du_Lich.Design_Pattern.Creational_Pattern.Factory;

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

        [HttpGet]
        public ActionResult OTP(int chon)
        {
            Session["chon"] = chon;
            return View();
        }

        [HttpPost]
        public ActionResult OTP(FormCollection formCollection)
        {
            string email = formCollection["email"];

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(email))
                {
                    ViewBag.Error = "Email không được để trống";
                }

                //kiểm tra xem có người nào đã đăng ký với điện thoại này hay chưa
                var khachhang = database.KHACHHANGs.FirstOrDefault(k => k.EmailKH == email);
                if (khachhang == null)
                {
                    ViewBag.Error = "Email chưa được đăng ký";
                }
                else
                {
                    Session["KH"] = khachhang;
                    OTPSender sender = OTPSenderFactory.GetSender("email");

                    string otp = GenerateOTP();
                    Session["otp"] = otp;

                    sender.SendOTP(email, otp);
                    ViewBag.ThongBao = "Đã gửi otp, hãy kiểm tra email";
                }
            }
            return View();
        }

        static string GenerateOTP()
        {
            // Generate a random 4-digit OTP
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }

        [HttpPost]
        public ActionResult Verify(FormCollection formCollection)
        {
            string otp = formCollection["otp"];

            if (string.IsNullOrEmpty(otp))
            {
                ViewBag.Error = "Email không được để trống";
            }

            //kiểm tra xem có người nào đã đăng ký với điện thoại này hay chưa
            if (otp != Session["otp"].ToString())
            {
                ViewBag.Error = "Sai otp";
            }
            else
            {
                if (Convert.ToInt32(Session["chon"]) == 1)
                {
                    Session["chon"] = null;
                    Session["otp"] = null;
                    return RedirectToAction("QuenMatKhau");
                }
                else
                {
                    Session["chon"] = null;
                    Session["otp"] = null;
                    return RedirectToAction("DoiMatKhau");
                }
            }
            return View("OTP");
        }

        [HttpGet]
        public ActionResult QuenMatKhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuenMatKhau(FormCollection formCollection)
        {
            string pass = formCollection["pass"];
            string passConfirm = formCollection["passConfirm"];

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(pass))
                {
                    ViewBag.Error = "Mật khẩu mới không được để trống";
                }
                if (string.IsNullOrEmpty(passConfirm))
                {
                    ViewBag.Error = "Mật khẩu nhập lại không được để trống";
                }
                if (pass != passConfirm)
                {
                    ViewBag.Error = "Mật khẩu không trùng khớp";
                }

                KHACHHANG khachhang = Session["KH"] as KHACHHANG;
                if (khachhang.MatKhau == pass)
                {
                    ViewBag.Error = "Mật khẩu phải khác mật khẩu cũ";
                }

                if (ViewBag.Error == null)
                {
                    var kh1 = database.KHACHHANGs.FirstOrDefault(k => k.MaKH == khachhang.MaKH);

                    kh1.MatKhau = pass;
                    kh1.XacNhanMatKhau = passConfirm;
                    database.Entry(kh1).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            Session["KH"] = null;
            return RedirectToAction("DangNhap");
        }

        [HttpGet]
        public ActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection formCollection)
        {
            string oldPass = formCollection["oldPass"];
            string newPass = formCollection["newPass"];
            string passConfirm = formCollection["passConfirm"];

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(oldPass))
                {
                    ViewBag.Error = "Mật khẩu cũ không được để trống";
                }
                if (string.IsNullOrEmpty(newPass))
                {
                    ViewBag.Error = "Mật khẩu mới không được để trống";
                }
                if (string.IsNullOrEmpty(passConfirm))
                {
                    ViewBag.Error = "Mật khẩu nhập lại không được để trống";
                }
                if (oldPass != passConfirm)
                {
                    ViewBag.Error = "Mật khẩu không trùng khớp";
                }

                var khachhang = Session["KhachHang"] as KHACHHANG;
                if (khachhang.MatKhau == newPass)
                {
                    ViewBag.Error = "Mật khẩu mới phải khác mật khẩu cũ";
                }

                if (ViewBag.Error == null)
                {
                    var kh1 = database.KHACHHANGs.FirstOrDefault(k => k.MaKH == khachhang.MaKH);

                    kh1.MatKhau = newPass;
                    kh1.XacNhanMatKhau = passConfirm;
                    database.Entry(kh1).State = System.Data.Entity.EntityState.Modified;
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