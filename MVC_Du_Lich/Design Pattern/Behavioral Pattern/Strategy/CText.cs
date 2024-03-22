using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Pattern.Strategy
{
    public class CText
    {
        IThanhToan thanhToan;
        public void setChoice(IThanhToan thanhToan)
        {
            this.thanhToan = thanhToan;
        }

        public string showChoice()
        {
            return thanhToan.ThanhToan();
        }
    }
}