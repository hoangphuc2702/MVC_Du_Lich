using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Pattern.Strategy
{
    public class ThanhToanOnline : IThanhToan
    {
        public string ThanhToan()
        {
            return "ĐẶT TOUR THÀNH CÔNG, VUI LÒNG THANH TOÁN QUA TÀI KHOẢN NÀY \n Momo: 0938492749";
        }
    }
}