
using NSU.Shared.DataContracts;
using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace NSU.Shared.NSUSystemPart
{
    public abstract class NSUPartBase : INSUPartBase, INotifyPropertyChanged
    {
        public const byte INVALID_VALUE = 0xFF;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var evt = PropertyChanged;
            evt?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void AttachXMLNode(XElement xml);
        public abstract void ReadXMLNode(XElement xml);
    }

    
}
