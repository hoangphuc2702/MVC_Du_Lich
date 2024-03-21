using MVC_Du_Lich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Iterator
{
    public class Iterator : IAbstractIterator
    {
        List<ThanhVien> listTV = new List<ThanhVien>();
        int current = 0;

        public Iterator(List<ThanhVien> listTV) 
        { 
            this.listTV = listTV;
        }

        public ThanhVien currentTV
        {
            get { return listTV[current]; }
        }

        public ThanhVien First()
        {
            current = 0;
            if(listTV.Count > 0)
            {
                return listTV[current];
            }
            return null;
        }

        public bool hasNext
        {
            get { return (current >= listTV.Count); }
        }

        public ThanhVien Next()
        {
            current++;
            if (!hasNext)
                return listTV[current];
            return null;

        }
    }
}