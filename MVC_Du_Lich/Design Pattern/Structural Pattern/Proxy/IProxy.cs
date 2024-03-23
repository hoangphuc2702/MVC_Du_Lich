using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Structural_Pattern.Proxy
{
    public interface IProxy
    {
        int AuthenticateUser(string username, string password);
    }
}