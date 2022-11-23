using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DataContracts
{
    public interface IElHeatingDataDataContract
    {
        public int Index { get; set; }
        public int StartHour { get; set; }
        public int StartMin { get; set; }
        public int EndHour { get; set; }
        public int EndMin { get; set; }
    }
}
