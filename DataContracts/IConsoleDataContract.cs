using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DataContracts
{
    public interface IConsoleDataContract : INSUSysPartDataContract
    {
        string Output { get; set; }
        List<object> ContextList { get; }
    }
}
