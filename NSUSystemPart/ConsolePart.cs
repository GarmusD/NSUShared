using NSU.Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.NSUSystemPart
{
    public class ConsolePart : NSUPartBase, IConsoleDataContract
    {
        public string Output { get => _output; set => SetOutput(value); }
        public List<object> ContextList { get; } = new List<object>();

        private string _output = string.Empty;
        
        private void SetOutput(string value)
        {
            _output = value;
            if(ContextList.Any())
                OnPropertyChanged(nameof(Output));
        }

        public override void AttachXMLNode(XElement xml)
        {
            //
        }

        public override void ReadXMLNode(XElement xml)
        {
            //
        }
    }
}
