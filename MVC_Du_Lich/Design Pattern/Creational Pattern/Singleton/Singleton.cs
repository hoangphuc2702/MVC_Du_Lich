using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVC_Du_Lich.Pattern.Singleton
{
    public sealed class Singleton
    {
        private static readonly Singleton instance = new Singleton();
        private List<TOUR> tours = new List<TOUR>();

        //Private constructor is used to prevent
        //creation of instances with 'new' keyword outside this class
        private Singleton()
        {
        }

        public void LazyInit(QLDULICHEntities db)
        {
            if(tours.Count == 0)
            {
                tours = db.TOURs.ToList();
            }
        }

        public static Singleton GetInstance()
        {
            return instance;
        }

        public List<TOUR> getTours
        {
            get { return tours; }
        }

        public void UpdateTour(QLDULICHEntities db)
        {
            tours.Clear();
            LazyInit(db);
        }
    }
}