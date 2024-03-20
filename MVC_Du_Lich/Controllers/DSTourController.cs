using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;

namespace MVC_Du_Lich.Controllers
{
    public class DSTourController : Controller
    {
        private QLDULICHEntities database = new QLDULICHEntities();

        // GET: CustomerTour
        public ActionResult TrangChu(int? loaiTour, int? diemden, int? diemdi, int? page)
        {
            // Bắt đầu với toàn bộ danh sách tour
            var tours = database.TOURs.AsQueryable();


            // Áp dụng điều kiện loaiTour (nếu có)
            if (loaiTour != null)
            {
                tours = tours.OrderByDescending(x => x.MaLoaiTour).Where(x => x.MaLoaiTour == loaiTour);
                Session["LoaiTour"] = loaiTour;
                // Lưu tham số loaiTour trong ViewBag
                ViewBag.LoaiTour = loaiTour;
            }
            else
            {
                tours = tours.OrderByDescending(x => x.TenTour);
            }

            // Áp dụng điều kiện diemden (nếu có)
            if (diemden != null)
            {
                tours = tours.OrderByDescending(x => x.MaDDen).Where(x => x.MaDDen == diemden);
                ViewBag.DiemDen = diemden;
            }

            // Áp dụng điều kiện diemdi (nếu có)
            if (diemdi != null)
            {
                tours = tours.OrderByDescending(x => x.MaDDi).Where(x => x.MaDDi == diemdi);

                ViewBag.DiemDi = diemdi;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            return View(tours.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ChiTiet(string loai)
        {
            //Lấy sách có mã tương ứng
            var sach = database.TOURs.FirstOrDefault(s => s.MaTour == loai);
            return View(sach);
        }


    }
}