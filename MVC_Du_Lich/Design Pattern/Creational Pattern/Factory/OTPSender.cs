using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Creational_Pattern.Factory
{
    public abstract class OTPSender
    {
        public abstract void SendOTP(string email, string otp);
    }
}