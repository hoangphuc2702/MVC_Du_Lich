using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Du_Lich.Controllers
{
    public class CategoryController : Controller
    {
        private QLDULICHEntities database = new QLDULICHEntities();

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        // Action PartialViewResult
        public ActionResult LoaiTour() 
        {
            var LocTour = database.LOAITOURs.ToList();
            return PartialView(LocTour);
        }
        public ActionResult DiemDen(int? id)
        {
            if (Session["LoaiTour"] != null)
            {
                int loaiTour = (int)Session["LoaiTour"];
                var LocTour = database.DIEMDENs.Where(tour => tour.MaLoaiTour == loaiTour).ToList();
                return PartialView(LocTour);
            }
            else
            {
                int loaiTour = id == 1 ? 1 : 2;
                Session["LoaiTour"] = loaiTour; // Đảm bảo rằng Session luôn chứa một giá trị int.
                var LocTour = database.DIEMDENs.Where(tour => tour.MaLoaiTour == loaiTour).ToList();
                return PartialView(LocTour);
            }
        }

        public ActionResult DiemDi()
        {
            var LocTour = database.DIEMDIs.ToList();
            TempData["LoaiTour"] = null;
            return PartialView(LocTour);
        }
    }
}