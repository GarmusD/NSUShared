using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.NSUSystemPart
{
    internal interface INSUPartBase
    {
        void AttachXMLNode(XElement xml);
        void ReadXMLNode(XElement xml);
    }
}
