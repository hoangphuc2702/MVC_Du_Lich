using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Pattern.Strategy
{
    public class ThanhToanOffline : IThanhToan
    {
        public string ThanhToan()
        {
            return "ĐẶT TOUR THÀNH CÔNG, VUI LÒNG ĐỢI NHÂN VIÊN LIÊN HỆ";
        }
    }
}