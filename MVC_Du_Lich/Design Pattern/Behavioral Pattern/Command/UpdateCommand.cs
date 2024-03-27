using MVC_Du_Lich.Models;
using MVC_Du_Lich.Pattern.Strategy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Behavioral_Pattern.Command
{
    public class UpdateCommand : ICommand
    {
        private readonly int id;
        private readonly QLDULICHEntities database;
        private bool htttoan;
        private bool initialHtttoan;

        public UpdateCommand(int id, QLDULICHEntities database, bool htttoan)
        {
            this.id = id;
            this.database = database;
            this.htttoan = htttoan;
            this.initialHtttoan = htttoan;
        }

        public void Execute()
        {
            var dondattour = database.DONDATTOURs.FirstOrDefault(d => d.SoHD == id);
            bool thanhtoan = !htttoan;
            if (dondattour != null)
            {
                dondattour.HTThanhToan = Convert.ToBoolean(thanhtoan);
                database.SaveChanges();
            }
        }

        public void Undo(int id)
        {
            var dondattour = database.DONDATTOURs.FirstOrDefault(d => d.SoHD == id);
            if (dondattour != null)
            {
                dondattour.HTThanhToan = initialHtttoan; // Hoàn tác lại trạng thái ban đầu của HTThanhToan
                database.SaveChanges();
            }
        }
    }
}