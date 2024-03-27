using MVC_Du_Lich.Models;
using MVC_Du_Lich.Pattern.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Du_Lich.Design_Pattern.Behavioral_Pattern.Command
{
    public class CommandStack
    {
        private static readonly CommandStack instance = new CommandStack();

        private Stack<ICommand> _stack = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _stack.Push(command);
        }

        public void Undo(int id)
        {
            if (_stack.Count > 0)
            {
                var command = _stack.Pop();
                command.Undo(id);
            }
        }

        public bool HasUndoCommand()
        {
            return _stack.Count > 0;
        }

        public void LazyInit()
        {
            if (_stack.Count == 0)
            {
                _stack = new Stack<ICommand>();
            }
        }

        public static CommandStack GetInstance()
        {
            return instance;
        }
    }
}