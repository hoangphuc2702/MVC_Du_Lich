using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Creational_Pattern.Prototype
{
    public abstract class Prototype
    {
        public abstract Prototype Clone(TOUR tour, string maTour);
    }
}