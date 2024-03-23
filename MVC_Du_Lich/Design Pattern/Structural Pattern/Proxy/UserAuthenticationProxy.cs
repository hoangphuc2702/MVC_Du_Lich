using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace MVC_Du_Lich.Design_Pattern.Structural_Pattern.Proxy
{
    public class UserAuthenticationProxy : IProxy
    {
        private readonly QLDULICHEntities database;

        public UserAuthenticationProxy()
        {
            database = new QLDULICHEntities();
        }

        public int AuthenticateUser(string username, string password)
        {
            var admin = database.ADMINs.FirstOrDefault(k => k.UserAdmin == username);
            if (admin != null)
            {
                if (password == admin.PassAdmin)
                    return 0; //thành công admin
                else 
                    return 1; //sai mật khẩu admin
            }
            else
            {
                // Logic kiểm tra xác thực người dùng
                var user = database.KHACHHANGs.FirstOrDefault(u => u.DienThoaiKH == username);

                if(user != null)
                {
                    if (password == user.MatKhau)
                    {
                        return 2; //thành công user
                    }
                    else
                    {
                        return 1; //sai mật khẩu user
                    }
                }
            }
            return 3; //tài khoản k có
        }
    }
}