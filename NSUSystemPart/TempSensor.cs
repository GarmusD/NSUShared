using System;
using NSU.Shared.NSUUtils;
using System.Xml.Linq;
using System.Linq;
using NSU.Shared.DataContracts;

namespace NSU.Shared.NSUSystemPart
{
    public class TempSensor : NSUPartBase, ITempSensorDataContract
    {
        #region Constants
        public const string NullAddress = "0:0:0:0:0:0:0:0";

        private const string XMLAttrAddr = "addr";
        private const string XMLAttrName = "name";
        private const string XMLAttrEnabled = "enabled";
        private const string XMLAttrInterval = "interval";
        private const string XMLAttrTemperature = "temperature";
        private const string XMLAttrConfigPos = "cfgpos";
        private const string XMLAttrNotFound = "notfound";
        private const string XMLAttrErrors = "errors";
        #endregion

        #region Static methods
        //Static methods
        public static string AddrToString(byte[] saddr)
        {
            if (saddr.Length != 8)
            {
                throw new Exception(string.Format("Invalid sensor address: {0}", BitConverter.ToString(saddr)));
            }
            return $"{saddr[0]:X2}:{saddr[1]:X2}:{saddr[2]:X2}:{saddr[3]:X2}:{saddr[4]:X2}:{saddr[5]:X2}:{saddr[6]:X2}:{saddr[7]:X2}";
        }

        public static byte[] StringToAddr(string saddr = "0:0:0:0:0:0:0:0")
        {
            if (saddr is null) saddr = "0:0:0:0:0:0:0:0";
            string[] parts = saddr.Split(':');
            if (parts.Length != 8)
            {
                throw new Exception(string.Format("Invalid sensor address format: '{0}'", saddr));
            }
            return parts.Select(s => Convert.ToByte(s, 16)).ToArray();
        }

        public static bool IsAddressNull(string value)
        {
            var addr = StringToAddr(value);
            return addr.All(b => b == 0);
        }

        public static bool IsAddressNull(TempSensor value)
        {
            return IsAddressNull(value.SensorID);
        }

        public static bool IsAddressNull(byte[] value)
        {
            return value.All(b => b == 0);
        }
        //
        #endregion

        #region Properties
        public byte ConfigPos { get => _cfgPos; set => SetConfigPos(value); }
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public string Name { get => _name; set => SetName(value); }
        public byte[] SensorID { get; set; }
        public int Interval { get => _interval; set => SetInterval(value); }
        public double Temperature { get => _temperature; set => SetTemperature(value); }
        public bool NotFound { get => _notFound; set => SetNotFound(value); }
        public int ReadErrorCount { get => _readErrorCount; set => SetReadErrors(value); }
        #endregion

        #region Private fields
        private byte _cfgPos = 0xFF;
        private bool _enabled = default;
        private string _name = default;
        private int _interval = default;
        private double _temperature = default;
        private bool _notFound = default;
        private int _readErrorCount = default;
        private XElement _xElement = null;
        #endregion


        public TempSensor()
        {
            _enabled = false;
            _name = string.Empty;
            SensorID = new byte[8];
            _interval = 0;
            _temperature = -127.00f;
            _cfgPos = INVALID_VALUE;
            _notFound = false;
        }

        public static TempSensor New()
        {
            return new TempSensor();
        }

        #region Private methods
        /************************************************************************
         * 
         * PRIVATE METHODS
         * 
         ************************************************************************/
        private void SetConfigPos(byte value)
        {
            _cfgPos = value;
            _xElement?.SetAttributeValue(XMLAttrConfigPos, _cfgPos);
        }

        private void SetName(string value)
        {
            string tmpName = Utils.ValidateName(value);
            if (tmpName != _name)
            {
                string old = _name;
                _name = tmpName;
                _xElement?.SetAttributeValue(XMLAttrName, _name);
                //OnNameChanged?.Invoke(this, new NameChangedEventArgs(_name, old));
            }
        }

        private void SetTemperature(double value)
        {
            if (_temperature != value)
            {
                _temperature = value;
                _xElement?.SetAttributeValue(XMLAttrTemperature, _temperature);
                OnPropertyChanged(nameof(Temperature));
            }
        }

        private void SetInterval(int value)
        {
            if (_interval != value)
            {
                _interval = value;
                _xElement?.SetAttributeValue(XMLAttrInterval, _interval);
                //OnIntervalChanged?.Invoke(this, new IntervalChangedEventArgs(_interval));
            }
        }

        private void SetEnabled(bool value)
        {
            if (_enabled != value)
            {
                _enabled = value;
                _xElement?.SetAttributeValue(XMLAttrEnabled, _enabled);
                //OnEnabledChanged?.Invoke(this, new EnabledChangedEventArgs(_enabled));
            }
        }

        private void SetNotFound(bool value)
        {
            _notFound = value;
            _xElement?.SetAttributeValue(XMLAttrNotFound, _notFound);
        }

        private void SetReadErrors(int value)
        {
            _readErrorCount = value;
            _xElement?.SetAttributeValue(XMLAttrErrors, _readErrorCount);
        }
        #endregion

        #region Public methods
        /************************************************************************
         * 
         * PUBLIC METHODS
         * 
         ************************************************************************/
        public bool CompareAddr(byte[] addr)
        {
            return SensorID.SequenceEqual(addr);
        }

        public string GetAddrTemp()
        {
            string temp_tmp = Convert.ToString(Convert.ToInt32(Temperature * 100));
            return string.Format("{0} {1}", AddrToString(SensorID), temp_tmp);
        }

        override public void AttachXMLNode(XElement xml)
        {
            // Try to find node for update
            if (!IsAddressNull(SensorID))
                _xElement = xml.Elements().FirstOrDefault(item => CompareAddr(StringToAddr((string)item.Attribute(XMLAttrAddr))));

            if (_xElement != null)
                ReadXMLNode(_xElement);
            else
                CreateNodeDefaults(xml);
        }

        private void CreateNodeDefaults(XElement xml)
        {
            _xElement = new XElement("TempSensor");
            _xElement.Add(new XAttribute(XMLAttrAddr, AddrToString(SensorID)));
            _xElement.Add(new XAttribute(XMLAttrName, Name));
            _xElement.Add(new XAttribute(XMLAttrEnabled, Convert.ToString(Enabled)));
            _xElement.Add(new XAttribute(XMLAttrInterval, Convert.ToString(Interval)));
            _xElement.Add(new XAttribute(XMLAttrTemperature, Convert.ToString(Temperature)));
            _xElement.Add(new XAttribute(XMLAttrConfigPos, Convert.ToString(ConfigPos)));
            _xElement.Add(new XAttribute(XMLAttrNotFound, Convert.ToString(NotFound)));
            _xElement.Add(new XAttribute(XMLAttrErrors, Convert.ToString(ReadErrorCount)));
            xml.Add(_xElement);
        }

        override public void ReadXMLNode(XElement xml)
        {
            _xElement = xml;
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            SensorID = StringToAddr((string)_xElement.Attribute(XMLAttrAddr));
            _name = (string)_xElement.Attribute(XMLAttrName);
            _interval = ((int?)_xElement.Attribute(XMLAttrInterval)).GetValueOrDefault(0);
            _temperature = ((double?)_xElement.Attribute(XMLAttrTemperature)).GetValueOrDefault(0);
            _cfgPos = ((byte?)(int?)_xElement.Attribute(XMLAttrConfigPos)).GetValueOrDefault(INVALID_VALUE);
            _notFound = ((bool?)_xElement.Attribute(XMLAttrNotFound)).GetValueOrDefault(false);
            _readErrorCount = ((int?)_xElement.Attribute(XMLAttrErrors)).GetValueOrDefault(0);
        }

        public void Clear()
        {
            _enabled = false;
            _name = string.Empty;
            _interval = 0;
            ConfigPos = INVALID_VALUE;
            NotFound = false;
            _readErrorCount = 0;
        }

        public void Update(TempSensor value)
        {
            ConfigPos = value.ConfigPos;
            Enabled = value.Enabled;
            Name = value.Name;
            Interval = value.Interval;
        }
        #endregion
    }
}

