using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DataContracts
{
    public interface IConsoleDataContract : INSUSysPartDataContract
    {
        public string Output { get; set; }
        public List<object> ContextList { get; }
    }
}
