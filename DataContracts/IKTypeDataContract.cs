using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface IKTypeDataContract : INSUSysPartDataContract
    {
        public int ConfigPos { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int Interval { get; set; }
        public int Temperature { get; set; }
    }
}
