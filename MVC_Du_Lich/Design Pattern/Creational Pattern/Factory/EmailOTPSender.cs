using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Threading.Tasks;

namespace MVC_Du_Lich.Design_Pattern.Creational_Pattern.Factory
{
    public class EmailOTPSender : OTPSender
    {
        public override void SendOTP(string email, string otp)
        {
            // SMTP server configuration
            var smtpServer = "smtp.gmail.com";
            var port = 587;
            var senderEmail = "vietnamtravel53627@gmail.com";
            var password = "lksmdhljavuwellv";

            // Create email message
            var message = new MailMessage(senderEmail, email, "Your OTP", $"Your OTP is: {otp}");

            // Configure SMTP client
            var client = new SmtpClient(smtpServer, port)
            {
                Credentials = new NetworkCredential(senderEmail, password),
                EnableSsl = true
            };

            // Send email
            client.Send(message);
        }
    }
}