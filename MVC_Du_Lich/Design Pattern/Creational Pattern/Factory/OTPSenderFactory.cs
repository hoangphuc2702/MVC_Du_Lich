using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Creational_Pattern.Factory
{
    public class OTPSenderFactory
    {
        public static OTPSender GetSender(string senderType)
        {
            if (senderType == "email")
            {
                return new EmailOTPSender();
            }
            else
            {
                throw new ArgumentException("Invalid sender type");
            }
        }
    }
}