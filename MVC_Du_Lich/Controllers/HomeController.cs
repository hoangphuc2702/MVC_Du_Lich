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
    public class HomeController : Controller
    {
        QLDULICHEntities database = new QLDULICHEntities();

        private List<TOUR> LayTourMoi(int soluong)
        {
            // Sắp xếp sách theo ngày cập nhật giảm dần, lấy đúng số lượng sách cần
            // Chuyển qua dạng danh sách kết quả đạt được
            return database.TOURs.OrderByDescending(tour => tour.NgayDiTour).Take(soluong).ToList();
        }

        public ActionResult Index()
        {
            var dsSachMoi = LayTourMoi(4);
            return View(dsSachMoi);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}