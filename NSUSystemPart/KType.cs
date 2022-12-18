using System;
using System.Xml.Linq;
using System.Linq;
using NSU.Shared.DataContracts;

namespace NSU.Shared.NSUSystemPart
{
    public class KType : NSUPartBase, IKTypeDataContract
    {
        #region Constants
        public const string LogTag = "KType";
        public const string TargetName = "ktype";

        private const string XMLAttrName = "name";
        private const string XMLAttrEnabled = "enabled";
        private const string XMLAttrInterval = "interval";
        private const string XMLAttrTemperature = "temperature";
        private const string XMLAttrConfigPos = "cfgpos";
        #endregion

        #region Properties
        public byte ConfigPos { get => _cfgPos; set => SetConfigPos(value); }
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public string Name { get => _name; set => SetName(value); }
        public int Interval { get => _interval; set => SetInterval(value); }
        public int Temperature { get => _temp; set => SetTemperature(value); }
        #endregion

        #region Private fields
        private byte _cfgPos;
        private bool _enabled;
        private string _name;
        private int _interval;
        private int _temp;
        private XElement _xElement = null;
        #endregion

        public KType()
        {
            _cfgPos = INVALID_VALUE;
            _enabled = false;
            _name = string.Empty;
            _interval = 0;
            _temp = 0;
        }

        public KType(IKTypeDataContract dataContract)
        {
            _cfgPos = dataContract.ConfigPos;
            _enabled = dataContract.Enabled;
            _name = dataContract.Name;
            _interval = dataContract.Interval;
            _temp = dataContract.Temperature;
        }

        #region Private methods
        private void SetConfigPos(byte value)
        {
            _cfgPos = value;
            _xElement?.SetAttributeValue(XMLAttrConfigPos, _cfgPos);
        }

        private void SetEnabled(bool value)
        {
            _enabled = value;
            _xElement?.SetAttributeValue(XMLAttrEnabled, _enabled);
        }

        private void SetName(string value)
        {
            _name = value;
            _xElement?.SetAttributeValue(XMLAttrName, _name);
        }

        private void SetInterval(int value)
        {
            _interval = value;
            _xElement?.SetAttributeValue(XMLAttrInterval, _interval);
        }

        private void SetTemperature(int value)
        {
            _temp = value;
            _xElement?.SetAttributeValue(XMLAttrTemperature, _temp);
            OnPropertyChanged(nameof(Temperature));
        }

        #endregion

        #region Public methods
        public override string ToString()
        {
            return _temp.ToString();
        }

        override public void AttachXMLNode(XElement xml)
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml), "XElement cannot be null.");

            if (_xElement == null && !string.IsNullOrWhiteSpace(Name))
                _xElement = xml.Elements().FirstOrDefault(item => item.Attribute(XMLAttrName)?.Value == _name);

            if (_xElement != null)
                ReadXMLNode(_xElement);
            else
                CreateNodeDefaults(xml);
        }

        private void CreateNodeDefaults(XElement xml)
        {
            _xElement = new XElement("KType");
            _xElement.Add(new XAttribute(XMLAttrName, Name));
            _xElement.Add(new XAttribute(XMLAttrEnabled, Enabled));
            _xElement.Add(new XAttribute(XMLAttrInterval, Interval));
            _xElement.Add(new XAttribute(XMLAttrTemperature, Temperature));
            _xElement.Add(new XAttribute(XMLAttrConfigPos, ConfigPos));
            xml.Add(_xElement);
        }

        override public void ReadXMLNode(XElement xml)
        {
            _xElement = xml;
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            _name = (string)_xElement.Attribute(XMLAttrName);
            _interval = ((int?)_xElement.Attribute(XMLAttrInterval)).GetValueOrDefault(0);
            _temp = ((int?)_xElement.Attribute(XMLAttrTemperature)).GetValueOrDefault(0);
            _cfgPos = ((byte?)(int?)_xElement.Attribute(XMLAttrConfigPos)).GetValueOrDefault(INVALID_VALUE);
        }
        #endregion
    }
}

