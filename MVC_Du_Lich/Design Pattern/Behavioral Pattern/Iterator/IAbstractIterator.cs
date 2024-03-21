using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Iterator
{
    interface IAbstractIterator
    {
        ThanhVien First();
        ThanhVien Next();
        bool hasNext { get; }
        ThanhVien currentTV { get; }
    }
}