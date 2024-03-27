using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Behavioral_Pattern.Command
{
    public class DeleteCommand : ICommand
    {
        private readonly int id;
        private readonly QLDULICHEntities database;
        private DONDATTOUR deletedDONDATTOUR; // Đối tượng đã bị xóa
        private List<CTDATTOUR> ctdattours;
        private CTDATTOUR ctdattour; // Đối tượng đã bị xóa
        private int MaHK;
        private string MaTour;

        public DeleteCommand(int id, QLDULICHEntities database)
        {
            this.id = id;
            this.database = database;
        }

        public void Execute()
        {
            deletedDONDATTOUR = database.DONDATTOURs.Find(id); // Lưu trữ thông tin về đối tượng đã bị xóa trước khi xóa nó
            if (deletedDONDATTOUR != null)
            {
                MaTour = deletedDONDATTOUR.MaTour;
                ctdattour = database.CTDATTOURs.FirstOrDefault(ct => ct.SoHD == id); // Sử dụng FirstOrDefault thay vì Where
                if (ctdattour != null)
                {
                    ctdattours = new List<CTDATTOUR> { ctdattour }; // Tạo List chứa một phần tử ctdattour
                    MaHK = (int)ctdattour.MaHK;
                    database.CTDATTOURs.Remove(ctdattour);
                }
                database.DONDATTOURs.Remove(deletedDONDATTOUR);
                database.SaveChanges();
            }
        }

        public void Undo(int id)
        {
            if (deletedDONDATTOUR != null)
            {
                deletedDONDATTOUR.SoHD = id;
                deletedDONDATTOUR.MaTour = MaTour;

                var ddt = database.DONDATTOURs.Add(deletedDONDATTOUR); // Thêm lại đối tượng đã bị xóa vào database

                ddt.SoHD = id;
                database.SaveChanges();


                if (ctdattours != null && ctdattours.Any())
                {
                    foreach (var ctdattour in ctdattours)
                    {
                        ctdattour.SoHD = id;
                        ctdattour.MaTour = MaTour; // Gán lại Mã Tour cho mỗi chi tiết đặt tour
                        ctdattour.MaHK = MaHK; // Gán lại Mã Hành Khách cho mỗi chi tiết đặt tour

                        var ct = database.DONDATTOURs.Add(deletedDONDATTOUR); // Thêm lại đối tượng đã bị xóa vào database
                        ct.SoHD = id;

                        database.SaveChanges();
                        Restore();
                    }
                }
            }
        }

        public void Restore()
        {
            deletedDONDATTOUR = null; // Đặt lại giá trị của biến deletedDONDATTOUR để ngăn chặn việc lặp lại việc thêm lại nó nhiều lần
            ctdattours = null;
            ctdattour = null;
            MaTour = null;
        }
    }
}