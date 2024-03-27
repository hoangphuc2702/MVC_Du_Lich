using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Du_Lich.Design_Pattern.Behavioral_Pattern.Command;
using MVC_Du_Lich.Design_Pattern.Creational_Pattern.Builder;
using MVC_Du_Lich.Models;
using MVC_Du_Lich.Pattern.Singleton;
using PagedList;
using static System.Net.Mime.MediaTypeNames;

namespace MVC_Du_Lich.Controllers
{
    public class QuanLyController : Controller
    {
        QLDULICHEntities database = new QLDULICHEntities();
        private Singleton tourSingleton;
        private readonly CommandStack commandStack;

        public QuanLyController()
        {
            // Khởi tạo tourSingleton trong hàm khởi tạo của CustomerTourController
            tourSingleton = Singleton.GetInstance();
            tourSingleton.LazyInit(database);

            commandStack = CommandStack.GetInstance();
            commandStack.LazyInit();
        }

        // GET: Admin
        private List<TOUR> LayTourMoi(int soluong)
        {
            // Sắp xếp sách theo ngày cập nhật giảm dần, lấy đúng số lượng sách cần
            // Chuyển qua dạng danh sách kết quả đạt được
            return tourSingleton.getTours.OrderByDescending(tour => tour.NgayDiTour).Take(soluong).ToList();
        }

        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login");
            }
            var dsSachMoi = LayTourMoi(4);
            return View(dsSachMoi);
        }

        public ActionResult LogOut()
        {
            Session["Admin"] = null;
            return RedirectToAction("DangNhap", "NguoiDung");
        }

        public void ViewBags()
        {
            ViewBag.MaDDi = new SelectList(database.DIEMDIs, "MaDDi", "TenDDi");
            ViewBag.MaDDen = new SelectList(database.DIEMDENs, "MaDDen", "TenDDen");
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");
            ViewBag.MaPT = new SelectList(database.PHUONGTIENs, "MaPT", "TenPT");
            ViewBag.MaLKS = new SelectList(database.LOAIKS, "MaLKS", "TenLKS");
        }


        //TOUR

        public ActionResult Tour(int? page)
        {
            var dsTour = tourSingleton.getTours;
            //Tạo biến cho biết số sách mỗi trang
            int pageSize = 7;
            //Tạo biến số trang
            int pageNum = (page ?? 1);
            return View(dsTour.OrderBy(tour => tour.MaTour).ToPagedList(pageNum,
            pageSize));
        }

        [HttpGet]
        public ActionResult ThemTour()
        {
            ViewBags();
            return View();
        }

        [HttpPost]
        public ActionResult ThemTour(FormCollection formCollection, HttpPostedFileBase Hinh1, HttpPostedFileBase Hinh2, HttpPostedFileBase Hinh3, HttpPostedFileBase Hinh4)
        {
            ViewBags();

            if (Hinh1 == null || Hinh2 == null || Hinh3 == null || Hinh4 == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn đầy đủ ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //lấy tên file của hình được up lên
                    var fileName1 = Path.GetFileName(Hinh1.FileName);
                    var fileName2 = Path.GetFileName(Hinh2.FileName);
                    var fileName3 = Path.GetFileName(Hinh3.FileName);
                    var fileName4 = Path.GetFileName(Hinh4.FileName);

                    if (!fileName1.Contains(".png") && !fileName1.Contains(".jpg") && !fileName1.Contains(".jpeg"))
                        ModelState.AddModelError(string.Empty, "Hình 1 không đúng định dạng (phải là png/jpg/jpeg)");

                    if (!fileName2.Contains(".png") && !fileName2.Contains(".jpg") && !fileName2.Contains(".jpeg"))
                        ModelState.AddModelError(string.Empty, "Hình 2 không đúng định dạng (phải là png/jpg/jpeg)");

                    if (!fileName3.Contains(".png") && !fileName3.Contains(".jpg") && !fileName3.Contains(".jpeg"))
                        ModelState.AddModelError(string.Empty, "Hình 3 không đúng định dạng (phải là png/jpg/jpeg)");

                    if (!fileName4.Contains(".png") && !fileName4.Contains(".jpg") && !fileName4.Contains(".jpeg"))
                        ModelState.AddModelError(string.Empty, "Hình 4 không đúng định dạng (phải là png/jpg/jpeg)");

                    else
                    {
                        var builder = new TourBuilder();

                        //Tạo đường dẫn tới file
                        var path1 = Path.Combine(Server.MapPath("~/Images"), fileName1);
                        var path2 = Path.Combine(Server.MapPath("~/Images"), fileName2);
                        var path3 = Path.Combine(Server.MapPath("~/Images"), fileName3);
                        var path4 = Path.Combine(Server.MapPath("~/Images"), fileName4);

                        Hinh1.SaveAs(path1);
                        Hinh2.SaveAs(path2);
                        Hinh3.SaveAs(path3);
                        Hinh4.SaveAs(path4);

                        //lưu tên tour
                        builder.ThemHinh1(fileName1);
                        builder.ThemHinh2(fileName2);
                        builder.ThemHinh3(fileName3);
                        builder.ThemHinh4(fileName4);


                        builder.ThemMaTour("T" + (database.TOURs.Count() + 1).ToString());
                        if (database.TOURs.Count() < 10)
                        {
                            builder.ThemMaTour("T0" + (database.TOURs.Count() + 1).ToString());
                        }

                        builder.ThemTenTour(formCollection["TenTour"]);
                        builder.ThemGia(Convert.ToDecimal(formCollection["Gia"]));
                        builder.ThemSoLuong(Convert.ToInt32(formCollection["SoLuong"]));
                        builder.ThemMoTa(formCollection["MoTa"]);
                        builder.ThemSLConLai(Convert.ToInt32(formCollection["SoLuong"]));
                        builder.ThemNgayDiTour(Convert.ToDateTime(formCollection["NgayDiTour"]));
                        builder.ThemNgayKetThuc(Convert.ToDateTime(formCollection["NgayKetThuc"]));
                        builder.ThemMaDDi(Convert.ToInt32(formCollection["MaDDi"]));
                        builder.ThemMaDDen(Convert.ToInt32(formCollection["MaDDen"]));
                        builder.ThemMaLoaiTour(Convert.ToInt32(formCollection["MaLoaiTour"]));
                        builder.ThemMaPT(Convert.ToInt32(formCollection["MaPT"]));
                        builder.ThemMaLKS(Convert.ToInt32(formCollection["MaLKS"]));

                        TOUR tour = builder.build();

                        //lưu vào csdl
                        database.TOURs.Add(tour);
                        database.SaveChanges();
                        tourSingleton.UpdateTour(database);
                        return RedirectToAction("Tour");
                    }
                }
            }

            return View();
        }

        public ActionResult ChiTietTour(string loai)
        {
            var tour = database.TOURs.FirstOrDefault(s => s.MaTour == loai);
            if (tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tour);
        }

        [HttpGet]
        public ActionResult SuaTour(string loai)
        {
            var tour = database.TOURs.FirstOrDefault(s => s.MaTour == loai);
            if (tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBags();
            return View(tour);
        }

        [HttpPost]
        public ActionResult SuaTour(FormCollection formCollection, HttpPostedFileBase Hinh1, HttpPostedFileBase Hinh2, HttpPostedFileBase Hinh3, HttpPostedFileBase Hinh4)
        {
            ViewBags();

            if (ModelState.IsValid)
            {
                var builder = new TourBuilder();

                builder.ThemMaTour(formCollection["MaTour"]);
                builder.ThemTenTour(formCollection["TenTour"]);
                builder.ThemGia(Convert.ToDecimal(formCollection["Gia"]));
                builder.ThemSoLuong(Convert.ToInt32(formCollection["SoLuong"]));

                if (Hinh1 != null)
                {
                    //Lấy tên file của hình được up lên
                    var fileName = Path.GetFileName(Hinh1.FileName);
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                    //Lưu tên
                    builder.ThemHinh1(fileName);
                    //Save vào Images Folder
                    Hinh1.SaveAs(path);
                }

                if (Hinh2 != null)
                {
                    //Lấy tên file của hình được up lên
                    var fileName = Path.GetFileName(Hinh2.FileName);
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                    //Lưu tên
                    builder.ThemHinh2(fileName);
                    //Save vào Images Folder
                    Hinh2.SaveAs(path);
                }

                if (Hinh3 != null)
                {
                    //Lấy tên file của hình được up lên
                    var fileName = Path.GetFileName(Hinh3.FileName);
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                    //Lưu tên
                    builder.ThemHinh3(fileName);
                    //Save vào Images Folder
                    Hinh3.SaveAs(path);
                }

                if (Hinh4 != null)
                {
                    //Lấy tên file của hình được up lên
                    var fileName = Path.GetFileName(Hinh4.FileName);
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                    //Lưu tên
                    builder.ThemHinh4(fileName);
                    //Save vào Images Folder
                    Hinh4.SaveAs(path);
                }

                builder.ThemMoTa(formCollection["MoTa"]);
                builder.ThemSLConLai(Convert.ToInt32(formCollection["SoLuong"]));
                builder.ThemNgayDiTour(Convert.ToDateTime(formCollection["NgayDiTour"]));
                builder.ThemNgayKetThuc(Convert.ToDateTime(formCollection["NgayKetThuc"]));
                builder.ThemMaDDi(Convert.ToInt32(formCollection["MaDDi"]));
                builder.ThemMaDDen(Convert.ToInt32(formCollection["MaDDen"]));
                builder.ThemMaLoaiTour(Convert.ToInt32(formCollection["MaLoaiTour"]));
                builder.ThemMaPT(Convert.ToInt32(formCollection["MaPT"]));
                builder.ThemMaLKS(Convert.ToInt32(formCollection["MaLKS"]));

                TOUR tour = builder.build();

                var productDB = database.TOURs.FirstOrDefault(p => p.MaTour == tour.MaTour);
                if (productDB != null)
                {
                    // Update các thuộc tính của tour từ builder
                    productDB.TenTour = tour.TenTour;
                    productDB.Gia = tour.Gia;
                    productDB.SoLuong = tour.SoLuong;
                    productDB.Hinh1 = tour.Hinh1;
                    productDB.Hinh2 = tour.Hinh2;
                    productDB.Hinh3 = tour.Hinh3;
                    productDB.Hinh4 = tour.Hinh4;
                    productDB.MoTa = tour.MoTa;
                    productDB.SoLuongConLai = tour.SoLuongConLai;
                    productDB.NgayDiTour = tour.NgayDiTour;
                    productDB.NgayKetThuc = tour.NgayKetThuc;
                    productDB.MaDDi = tour.MaDDi;
                    productDB.MaDDen = tour.MaDDen;
                    productDB.MaLoaiTour = tour.MaLoaiTour;
                    productDB.MaPT = tour.MaPT;
                    productDB.MaLKS = tour.MaLKS;
                }
                database.SaveChanges();
                tourSingleton.UpdateTour(database);
                return RedirectToAction("Tour");
            }
            return View();
        }

        // GET: Products/Delete/5
        public ActionResult XoaTour(string loai)
        {
            var tour = database.TOURs.FirstOrDefault(s => s.MaTour == loai);
            if (tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tour);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("XoaTour")]
        public ActionResult DeleteConfirmedTour(string loai)
        {
            var tour = database.TOURs.Find(loai);
            database.TOURs.Remove(tour);
            database.SaveChanges();
            tourSingleton.UpdateTour(database);
            return RedirectToAction("Tour");
        }

        public ActionResult NhanBanTour(string loai)
        {
            var tour = database.TOURs.FirstOrDefault(s => s.MaTour == loai);
            if (tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tour);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("NhanBanTour")]
        public ActionResult NhanBanConfirmedTour(string loai)
        {
            var tour = database.TOURs.Find(loai);


            string maTour = "T" + (database.TOURs.Count() + 1).ToString();
            if (database.TOURs.Count() < 10)
            {
                maTour = "T0" + (database.TOURs.Count() + 1).ToString();
            }
            TOUR cloneTour = (TOUR)tour.Clone(tour, maTour);


            database.TOURs.Add(cloneTour);
            database.SaveChanges();
            tourSingleton.UpdateTour(database);
            return RedirectToAction("Tour");
        }



        //DiemDi

        public ActionResult DiemDi(int? page)
        {
            var dsDiemDi = database.DIEMDIs.ToList();
            //Tạo biến cho biết số sách mỗi trang
            int pageSize = 7;
            //Tạo biến số trang
            int pageNum = (page ?? 1);
            return View(dsDiemDi.OrderBy(diemdi => diemdi.MaDDi).ToPagedList(pageNum,
            pageSize));
        }

        [HttpGet]
        public ActionResult ThemDiemDi()
        {           
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");
            return View();
        }

        [HttpPost]
        public ActionResult ThemDiemDi(DIEMDI diemdi)
        {
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");
            try
            {
                database.DIEMDIs.Add(diemdi);
                database.SaveChanges();
                return RedirectToAction("DiemDi");
            }
            catch
            {
                return Content("LỖI TẠO MỚI ĐIỂM ĐI");
            }
        }

        public ActionResult ChiTietDiemDi(int id)
        {
            var diemdi = database.DIEMDIs.FirstOrDefault(s => s.MaDDi == id);
            if (diemdi == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(diemdi);
        }

        [HttpGet]
        public ActionResult SuaDiemDi(int id)
        {
            var diemdi = database.DIEMDIs.FirstOrDefault(s => s.MaDDi == id);
            if (diemdi == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");
            return View(diemdi);
        }

        [HttpPost]
        public ActionResult SuaDiemDi(DIEMDI diemdi, int id)
        {
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");

            database.Entry(diemdi).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("DiemDi");
        }

        // GET: Products/Delete/5
        public ActionResult XoaDiemDi(int id)
        {
            var diemdi = database.DIEMDIs.FirstOrDefault(s => s.MaDDi == id);
            if (diemdi == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(diemdi);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("XoaDiemDi")]
        public ActionResult DeleteConfirmedDiemDi(int id)
        {
            var diemdi = database.DIEMDIs.Find(id);
            database.DIEMDIs.Remove(diemdi);
            database.SaveChanges();
            return RedirectToAction("DiemDi");
        }


        //DiemDen

        public ActionResult DiemDen(int? page)
        {
            var dsDiemDen = database.DIEMDENs.ToList();
            //Tạo biến cho biết số sách mỗi trang
            int pageSize = 7;
            //Tạo biến số trang
            int pageNum = (page ?? 1);
            return View(dsDiemDen.OrderBy(diemden => diemden.MaDDen).ToPagedList(pageNum,
            pageSize));
        }

        [HttpGet]
        public ActionResult ThemDiemDen()
        {
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");
            ViewBag.MaCL = new SelectList(database.CHAULUCs, "MaChauLuc", "TenChauLuc");
            return View();
        }

        [HttpPost]
        public ActionResult ThemDiemDen(DIEMDEN diemden)
        {
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");
            ViewBag.MaCL = new SelectList(database.CHAULUCs, "MaChauLuc", "TenChauLuc");
            try
            {
                database.DIEMDENs.Add(diemden);
                database.SaveChanges();
                return RedirectToAction("DiemDen");
            }
            catch
            {
                return Content("LỖI TẠO MỚI ĐIỂM ĐẾN");
            }
        }

        public ActionResult ChiTietDiemDen(int id)
        {
            var diemden = database.DIEMDENs.FirstOrDefault(s => s.MaDDen == id);
            if (diemden == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(diemden);
        }

        [HttpGet]
        public ActionResult SuaDiemDen(int id)
        {
            var diemden = database.DIEMDENs.FirstOrDefault(s => s.MaDDen == id);
            if (diemden == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");
            ViewBag.MaCL = new SelectList(database.CHAULUCs, "MaChauLuc", "TenChauLuc");
            return View(diemden);
        }

        [HttpPost]
        public ActionResult SuaDiemDen(DIEMDEN diemden, int id)
        {
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");
            ViewBag.MaCL = new SelectList(database.CHAULUCs, "MaChauLuc", "TenChauLuc");

            database.Entry(diemden).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("DiemDen");
        }

        // GET: Products/Delete/5
        public ActionResult XoaDiemDen(int id)
        {
            var diemden = database.DIEMDENs.FirstOrDefault(s => s.MaDDen == id);
            if (diemden == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(diemden);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("XoaDiemDen")]
        public ActionResult DeleteConfirmedDiemDen(int id)
        {
            var diemden = database.DIEMDENs.Find(id);
            database.DIEMDENs.Remove(diemden);
            database.SaveChanges();
            return RedirectToAction("DiemDen");
        }


        //ChauLuc

        public ActionResult ChauLuc(int? page)
        {
            var dsChauLuc = database.CHAULUCs.ToList();
            //Tạo biến cho biết số sách mỗi trang
            int pageSize = 7;
            //Tạo biến số trang
            int pageNum = (page ?? 1);
            return View(dsChauLuc.OrderBy(chauluc => chauluc.MaChauLuc).ToPagedList(pageNum,
            pageSize));
        }

        [HttpGet]
        public ActionResult ThemChauLuc()
        {
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");          
            return View();
        }

        [HttpPost]
        public ActionResult ThemChauLuc(DIEMDEN chauluc)
        {
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");           
            return View("ChauLuc");
        }

        public ActionResult ChiTietChauLuc(int id)
        {
            var chauluc = database.CHAULUCs.FirstOrDefault(s => s.MaChauLuc == id);
            if (chauluc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chauluc);
        }

        [HttpGet]
        public ActionResult SuaChauLuc(int id)
        {
            var chauluc = database.CHAULUCs.FirstOrDefault(s => s.MaChauLuc == id);
            if (chauluc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");
            return View(chauluc);
        }

        [HttpPost]
        public ActionResult SuaChauLuc(CHAULUC chauluc, int id)
        {
            ViewBag.MaLoaiTour = new SelectList(database.LOAITOURs, "MaLoaiTour", "TenLoaiTour");

            database.Entry(chauluc).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("ChauLuc");
        }

        // GET: Products/Delete/5
        public ActionResult XoaChauLuc(int id)
        {
            var chauluc = database.CHAULUCs.FirstOrDefault(s => s.MaChauLuc == id);
            if (chauluc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chauluc);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("XoaChauLuc")]
        public ActionResult DeleteConfirmedChauLuc(int id)
        {
            var chauluc = database.CHAULUCs.Find(id);
            database.CHAULUCs.Remove(chauluc);
            database.SaveChanges();
            return RedirectToAction("ChauLuc");
        }

        //KHÁCH SẠN

        public ActionResult KhachSan(int? page)
        {
            var dsKhachSan = database.KHACHSANs.ToList();
            //Tạo biến cho biết số sách mỗi trang
            int pageSize = 7;
            //Tạo biến số trang
            int pageNum = (page ?? 1);
            return View(dsKhachSan.OrderBy(khachsan => khachsan.MaKS).ToPagedList(pageNum,
            pageSize));
        }

        [HttpGet]
        public ActionResult ThemKhachSan()
        {
            ViewBag.MaLKS = new SelectList(database.LOAIKS, "MaLKS", "TenLKS");
            return View();
        }

        [HttpPost]
        public ActionResult ThemKhachSan(KHACHSAN khachsan)
        {
            ViewBag.MaLKS = new SelectList(database.LOAIKS, "MaLKS", "TenLKS");
            return View("KhachSan");
        }

        public ActionResult ChiTietKhachSan(string loai)
        {
            var khachsan = database.KHACHSANs.FirstOrDefault(s => s.MaKS == loai);
            if (khachsan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachsan);
        }

        [HttpGet]
        public ActionResult SuaKhachSan(string loai)
        {
            var khachsan = database.KHACHSANs.FirstOrDefault(s => s.MaKS == loai);
            if (khachsan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLKS = new SelectList(database.LOAIKS, "MaLKS", "TenLKS");
            return View(khachsan);
        }

        [HttpPost]
        public ActionResult SuaKhachSan(KHACHSAN khachsan, string loai)
        {
            ViewBag.MaLKS = new SelectList(database.LOAIKS, "MaLKS", "TenLKS");

            database.Entry(khachsan).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("KhachSan");
        }

        // GET: Products/Delete/5
        public ActionResult XoaKhachSan(string id)
        {
            var khachsan = database.KHACHSANs.FirstOrDefault(s => s.MaKS == id);
            if (khachsan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachsan);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("XoaKhachSan")]
        public ActionResult DeleteConfirmedKhachSan(string id)
        {
            var khachsan = database.KHACHSANs.Find(id);
            database.KHACHSANs.Remove(khachsan);
            database.SaveChanges();
            return RedirectToAction("KhachSan");
        }

        //Loại Khách Sạn

        public ActionResult LoaiKS(int? page)
        {
            var dsLoaiKs = database.LOAIKS.ToList();
            //Tạo biến cho biết số sách mỗi trang
            int pageSize = 7;
            //Tạo biến số trang
            int pageNum = (page ?? 1);
            return View(dsLoaiKs.OrderBy(loaiks => loaiks.MaLKS).ToPagedList(pageNum,
            pageSize));
        }

        [HttpGet]
        public ActionResult ThemLoaiKS()
        {           
            return View();
        }

        [HttpPost]
        public ActionResult ThemLoaiKS(LOAIK loaiks)
        {
            try
            {
                database.LOAIKS.Add(loaiks);
                database.SaveChanges();
                return RedirectToAction("LoaiKS");
            }
            catch
            {
                return Content("LỖI TẠO MỚI Nhà xuất bản");
            }
        }

        public ActionResult ChiTietLoaiKS(int id)
        {
            var loaiks = database.LOAIKS.FirstOrDefault(s => s.MaLKS == id);
            if (loaiks == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(loaiks);
        }

        [HttpGet]
        public ActionResult SuaLoaiKS(int id)
        {
            var loaiks = database.LOAIKS.FirstOrDefault(s => s.MaLKS == id);
            if (loaiks == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(loaiks);
        }

        [HttpPost]
        public ActionResult SuaLoaiKS(LOAIK loaiks, int id)
        {

            database.Entry(loaiks).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("LoaiKS");
        }

        // GET: Products/Delete/5
        public ActionResult XoaLoaiKS(int id)
        {
            var loaiks = database.LOAIKS.FirstOrDefault(s => s.MaLKS == id);
            if (loaiks == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(loaiks);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("XoaLoaiKS")]
        public ActionResult DeleteConfirmedLoaiKS(int id)
        {
            var loaiks = database.LOAIKS.Find(id);
            database.LOAIKS.Remove(loaiks);
            database.SaveChanges();
            return RedirectToAction("LoaiKS");
        }

        //Đơn Đặt Tour

        public ActionResult DonDatTour(int? page)
        {
            var dsDonDatTour = database.DONDATTOURs.ToList();
            //Tạo biến cho biết số sách mỗi trang
            int pageSize = 7;
            //Tạo biến số trang
            int pageNum = (page ?? 1);
            return View(dsDonDatTour.OrderBy(dondattour => dondattour.SoHD).ToPagedList(pageNum,
            pageSize));
        }

        public ActionResult ChiTietDonDatTour(int id)
        {
            var dondattour = database.DONDATTOURs.FirstOrDefault(s => s.SoHD == id);
            if (dondattour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dondattour);
        }


        [HttpGet]
        public ActionResult SuaDonDatTour(int id)
        {
            var dondattour = database.DONDATTOURs.FirstOrDefault(s => s.SoHD == id);
            if (dondattour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaKH = new SelectList(database.KHACHHANGs, "MaKH", "HoTenKH");
            ViewBag.MaTour = new SelectList(database.TOURs, "MaTour", "TenTour");

            return View(dondattour);
        }

        [HttpPost]
        public ActionResult SuaDonDatTour(DONDATTOUR dondattour)
        {
            ViewBag.MaKH = new SelectList(database.KHACHHANGs, "MaKH", "HoTenKH");
            ViewBag.MaTour = new SelectList(database.TOURs, "MaTour", "TenTour");

            database.Entry(dondattour).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("DonDatTour");
        }

        public ActionResult SuaThanhToanTour(int id, bool htttoan)
        {
            ICommand command = new UpdateCommand(id, database, htttoan);

            // Kiểm tra xem Session["UpdatedIds"] đã tồn tại chưa
            if (Session["UpdatedIds"] == null)
            {
                // Nếu chưa tồn tại, tạo một danh sách mới để lưu trữ các ID đã được cập nhật
                Session["UpdatedIds"] = new List<int>();
            }

            // Kiểm tra xem danh sách ID đã tồn tại trong Session chưa
            List<int> updatedIds = Session["UpdatedIds"] as List<int>;

            // Thêm ID mới vào danh sách
            updatedIds.Add(id);

            // Lưu danh sách đã cập nhật vào Session
            Session["UpdatedIds"] = updatedIds;

            commandStack.ExecuteCommand(command);
            return RedirectToAction("DonDatTour");
        }

        public ActionResult XoaDonDatTour(int id)
        {
            var command = new DeleteCommand(id, database);

            Session["SoHD"] = id;

            // Kiểm tra xem Session["UpdatedIds"] đã tồn tại chưa
            if (Session["DeletedIds"] == null)
            {
                // Nếu chưa tồn tại, tạo một danh sách mới để lưu trữ các ID đã được cập nhật
                Session["DeletedIds"] = new List<int>();
            }

            // Kiểm tra xem danh sách ID đã tồn tại trong Session chưa
            List<int> deletedIds = Session["DeletedIds"] as List<int>;

            // Thêm ID mới vào danh sách
            deletedIds.Add(id);

            // Lưu danh sách đã cập nhật vào Session
            Session["DeletedIds"] = deletedIds;

            commandStack.ExecuteCommand(command);
            return RedirectToAction("DonDatTour");
        }

        public ActionResult Undo(int id)
        {
            commandStack.Undo(id);
            return RedirectToAction("DonDatTour");
        }


        [HttpGet]
        public ActionResult SuaCTDatTour(int hd)
        {
            var ctdattour = database.CTDATTOURs.Where(s => s.SoHD == hd).ToList();
            if (ctdattour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaTour = new SelectList(database.TOURs, "MaTour", "TenTour");
            ViewBag.MaHK = new SelectList(database.HANHKHACHes, "MaHK", "TenHK");
            return View(ctdattour);
        }

        [HttpPost]
        public ActionResult SuaCTDatTour(CTDATTOUR ctdattour)
        {
            ViewBag.MaTour = new SelectList(database.TOURs, "MaTour", "TenTour");
            ViewBag.MaHK = new SelectList(database.HANHKHACHes, "MaHK", "TenHK");

            database.Entry(ctdattour).State = System.Data.Entity.EntityState.Modified;

            database.SaveChanges();
            return RedirectToAction("ChiTietDonDatTour");
        }

        //public ActionResult XoaDonDatTour(int id)
        //{
        //    var dondattour = database.DONDATTOURs.FirstOrDefault(s => s.SoHD == id);

        //    if (dondattour == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    return View(dondattour);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("XoaDonDatTour")]
        //public ActionResult DeleteConfirmedDonDatTour(int id)
        //{
        //    // Tìm đơn đặt tour theo id
        //    var dondattour = database.DONDATTOURs.Find(id);

        //    if (dondattour != null)
        //    {
        //        // Lấy danh sách chi tiết đặt tour liên quan đến đơn đặt tour
        //        var ctdattours = database.CTDATTOURs.Where(ct => ct.SoHD == id).ToList();

        //        // Xóa tất cả các chi tiết đặt tour
        //        database.CTDATTOURs.RemoveRange(ctdattours);

        //        // Xóa đơn đặt tour
        //        database.DONDATTOURs.Remove(dondattour);

        //        // Lưu thay đổi vào cơ sở dữ liệu
        //        database.SaveChanges();
        //    }

        //    return RedirectToAction("DonDatTour");
        //}


        public ActionResult XoaCTDatTour(string TenTV)
        {
            var dondattour = database.CTDATTOURs.FirstOrDefault(s => s.TenTV == TenTV);

            if (dondattour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dondattour);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("XoaCTDatTour")]
        public ActionResult DeleteConfirmedDatTour(string TenTV)
        {
            var dondattour = database.CTDATTOURs.Find(TenTV);
            database.CTDATTOURs.Remove(dondattour);
            database.SaveChanges();
            return RedirectToAction("SuaCTDatTour");
        }
    }
}