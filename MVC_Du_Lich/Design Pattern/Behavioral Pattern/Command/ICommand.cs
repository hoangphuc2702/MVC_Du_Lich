using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Behavioral_Pattern.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo(int id);
    }
}   