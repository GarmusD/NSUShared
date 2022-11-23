using System;
using System.Linq;
using System.Xml.Linq;
using NSU.Shared.DataContracts;
using NSU.Shared.NSUUtils;

namespace NSU.Shared.NSUSystemPart
{
    public class ComfortZone : NSUPartBase, IComfortZoneDataContract
    {
        #region Constants
        public const string LogTag = "ComfortZone";

        private const string XMLAttrConfigPos = "cfgpos";
        private const string XMLAttrEnabled = "enabled";
        private const string XMLAttrName = "name";
        private const string XMLAttrTitle = "title";
        private const string XMLAttrRoomSensorName = "rsname";
        private const string XMLAttrFloorSensorName = "fsname";
        private const string XMLAttrCollectorName = "collname";
        private const string XMLAttrRoomTempHi = "roomTempHi";
        private const string XMLAttrRoomTempLo = "roomTempLow";
        private const string XMLAttrFloorTempHi = "floorTempHi";
        private const string XMLAttrFloorTempLo = "floorTempLow";
        private const string XMLAttrHisteresis = "hist";
        private const string XMLAttrActuator = "actuator";
        private const string XMLAttrLowTempMode = "lowtempmode";
        private const string XMLAttrCurrRoomTemp = "crTemp";
        private const string XMLAttrCurrFloorTemp = "cfTemp";
        private const string XMLAttrActuatorOpened = "actopened";
        #endregion

        #region Properties
        public int ConfigPos { get => _cfgPos; set => SetConfigPos(value); }
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public string Name { get => _name; set => SetName(value); }
        public string Title { get => GetTitle(); set => SetTitle(value); }
        public string RoomSensorName { get => _roomSensorName; set => SetRoomSensorName(value); }
        public string FloorSensorName { get => _floorSensorName; set => SetFloorSensorName(value); }
        public string CollectorName { get => _collectorName; set => SetCollectorName(value); }
        public double RoomTempHi { get => _roomTempHi; set => SetRoomTempHi(value); }
        public double RoomTempLow { get => _roomTempLow; set => SetRoomTempLow(value); }
        public double FloorTempHi { get => _floorTempHi; set => SetFloorTempHi(value); }
        public double FloorTempLow { get => _floorTempLow; set => SetFloorTempLow(value); }
        public double Histeresis { get => _histeresis; set => SetHisteresis(value); }
        public int Actuator { get => _actuator; set => SetActuator(value); }
        public bool LowTempMode { get => _lowTempMode; set => SetLowTempMode(value); }
        public double CurrentRoomTemperature { get => _currentRoomTemperature; set => SetCurrentRoomTemperature(value); }
        public double CurrentFloorTemperature { get => _currentFloorTemperature; set => SetCurrentFloorTemperature(value); }
        public bool ActuatorOpened { get => _actuatorOpened; set => SetActuatorOpened(value); }
        #endregion

        #region Private fields
        private int _cfgPos;
        private bool _enabled;
        private string _name;
        private string _title;
        private string _roomSensorName;
        private string _floorSensorName;
        private string _collectorName;
        private double _roomTempHi;
        private double _roomTempLow;
        private double _floorTempHi;
        private double _floorTempLow;
        private double _histeresis;
        private int _actuator;
        private bool _lowTempMode;
        private double _currentRoomTemperature;
        private double _currentFloorTemperature;
        private bool _actuatorOpened;
        private XElement? _xElement;
        #endregion

        public ComfortZone()
        {
            _enabled = false;
            _cfgPos = INVALID_VALUE;
            _name = string.Empty;
            _title = string.Empty;
            _roomSensorName = string.Empty;
            _floorSensorName = string.Empty;
            _collectorName = string.Empty;
            _roomTempHi = -127.0f;
            _roomTempLow = -127.0f;
            _floorTempHi = -127.0f;
            _floorTempLow = -127.0f;
            _histeresis = 0;
            _actuator = INVALID_VALUE;
            _lowTempMode = false;
            _currentRoomTemperature = -127.0f;
            _currentFloorTemperature = -127.0f;
            _xElement = null;
        }

        public ComfortZone(IComfortZoneDataContract dataContract)
        {
            _enabled = dataContract.Enabled;
            _cfgPos = dataContract.ConfigPos;
            _name = dataContract.Name;
            _title = dataContract.Title;
            _roomSensorName = dataContract.RoomSensorName;
            _floorSensorName = dataContract.FloorSensorName;
            _collectorName = dataContract.CollectorName;
            _roomTempHi = dataContract.RoomTempHi;
            _roomTempLow = dataContract.RoomTempLow;
            _floorTempHi = dataContract.FloorTempHi;
            _floorTempLow = dataContract.FloorTempLow;
            _histeresis = dataContract.Histeresis;
            _actuator = dataContract.Actuator;
            _lowTempMode = dataContract.LowTempMode;
            _currentRoomTemperature = dataContract.CurrentFloorTemperature;
            _currentFloorTemperature = dataContract.CurrentFloorTemperature;
            _xElement = null;
        }

        #region Private methods
        private void SetConfigPos(int value)
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

        private void SetTitle(string value)
        {
            _title = value;
            _xElement?.SetAttributeValue(XMLAttrTitle, _title);
        }

        private string GetTitle()
        {
            if (string.IsNullOrEmpty(_title))
                return Utils.FirstLetterToUpper(_name.Replace('_', ' '));
            return _title;
        }

        private void SetRoomSensorName(string value)
        {
            _roomSensorName = value;
            _xElement?.SetAttributeValue(XMLAttrRoomSensorName, _roomSensorName);
        }

        private void SetFloorSensorName(string value)
        {
            _floorSensorName = value;
            _xElement?.SetAttributeValue(XMLAttrFloorSensorName, _floorSensorName);
        }

        private void SetCollectorName(string value)
        {
            _collectorName = value;
            _xElement?.SetAttributeValue(XMLAttrCollectorName, _collectorName);
        }

        private void SetRoomTempHi(double value)
        {
            _roomTempHi = value;
            _xElement?.SetAttributeValue(XMLAttrRoomTempHi, _roomTempHi);
        }

        private void SetRoomTempLow(double value)
        {
            _roomTempLow = value;
            _xElement?.SetAttributeValue(XMLAttrRoomTempLo, _roomTempLow);
        }

        private void SetFloorTempHi(double value)
        {
            _floorTempHi = value;
            _xElement?.SetAttributeValue(XMLAttrFloorTempHi, _floorTempHi);
        }

        private void SetFloorTempLow(double value)
        {
            _floorTempLow = value;
            _xElement?.SetAttributeValue(XMLAttrFloorTempLo, _floorTempLow);
        }

        private void SetHisteresis(double value)
        {
            _histeresis = value;
            _xElement?.SetAttributeValue(XMLAttrHisteresis, _histeresis);
        }

        private void SetActuator(int value)
        {
            _actuator = value;
            _xElement?.SetAttributeValue(XMLAttrActuator, _actuator);
        }

        private void SetLowTempMode(bool value)
        {
            if (_lowTempMode != value)
            {
                _lowTempMode = value;
                _xElement?.SetAttributeValue(XMLAttrLowTempMode, _lowTempMode);
                OnPropertyChanged(nameof(LowTempMode));
            }
        }

        private void SetCurrentRoomTemperature(double value)
        {
            if (_currentRoomTemperature != value)
            {
                _currentRoomTemperature = value;
                _xElement?.SetAttributeValue(XMLAttrCurrRoomTemp, _currentRoomTemperature);
                OnPropertyChanged(nameof(CurrentRoomTemperature));
            }
        }

        private void SetCurrentFloorTemperature(double value)
        {
            if (_currentFloorTemperature != value)
            {
                _currentFloorTemperature = value;
                _xElement?.SetAttributeValue(XMLAttrCurrFloorTemp, _currentFloorTemperature);
                OnPropertyChanged(nameof(CurrentFloorTemperature));
            }
        }

        private void SetActuatorOpened(bool value)
        {
            if (_actuatorOpened != value)
            {
                _actuatorOpened = value;
                _xElement?.SetAttributeValue(XMLAttrActuatorOpened, _actuatorOpened);
                OnPropertyChanged(nameof(ActuatorOpened));
            }
        }
        #endregion

        #region Public methods
        override public void AttachXMLNode(XElement xml)
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml), "XElement cannot be null.");

            if (_xElement == null && !string.IsNullOrWhiteSpace(_name))
                _xElement = xml.Elements().FirstOrDefault(item => item.Attribute(XMLAttrName)?.Value == _name);

            if (_xElement != null)
                ReadXMLNode(_xElement);
            else
                CreateNodeDefaults(xml);
        }

        private void CreateNodeDefaults(XElement xml)
        {
            _xElement = new XElement("ComfortZone");
            _xElement.Add(new XAttribute(XMLAttrConfigPos, _cfgPos));
            _xElement.Add(new XAttribute(XMLAttrEnabled, _enabled));
            _xElement.Add(new XAttribute(XMLAttrName, _name));
            _xElement.Add(new XAttribute(XMLAttrTitle, _title));
            _xElement.Add(new XAttribute(XMLAttrRoomSensorName, _roomSensorName));
            _xElement.Add(new XAttribute(XMLAttrFloorSensorName, _floorSensorName));
            _xElement.Add(new XAttribute(XMLAttrCollectorName, _collectorName));
            _xElement.Add(new XAttribute(XMLAttrRoomTempHi, _roomTempHi));
            _xElement.Add(new XAttribute(XMLAttrRoomTempLo, _roomTempLow));
            _xElement.Add(new XAttribute(XMLAttrFloorTempHi, _floorTempHi));
            _xElement.Add(new XAttribute(XMLAttrFloorTempLo, _floorTempLow));
            _xElement.Add(new XAttribute(XMLAttrHisteresis, _histeresis));
            _xElement.Add(new XAttribute(XMLAttrActuator, _actuator));
            _xElement.Add(new XAttribute(XMLAttrLowTempMode, _lowTempMode));
            _xElement.Add(new XAttribute(XMLAttrCurrRoomTemp, _currentRoomTemperature));
            _xElement.Add(new XAttribute(XMLAttrCurrFloorTemp, _currentFloorTemperature));
            _xElement.Add(new XAttribute(XMLAttrActuatorOpened, _actuatorOpened));
            xml.Add(_xElement);
        }

        override public void ReadXMLNode(XElement xml)
        {
            _xElement = xml;

            _cfgPos = ((int?)_xElement.Attribute(XMLAttrConfigPos)).GetValueOrDefault(0xFF);
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            _name = (string)_xElement.Attribute(XMLAttrName);
            _title = (string)_xElement.Attribute(XMLAttrTitle);
            _roomSensorName = (string)_xElement.Attribute(XMLAttrRoomSensorName);
            _floorSensorName = (string)_xElement.Attribute(XMLAttrFloorSensorName);
            _collectorName = (string)_xElement.Attribute(XMLAttrCollectorName);
            _roomTempHi = ((double?)_xElement.Attribute(XMLAttrRoomTempHi)).GetValueOrDefault(0);
            RoomTempLow = ((double?)_xElement.Attribute(XMLAttrRoomTempLo)).GetValueOrDefault(0);
            _floorTempHi = ((double?)_xElement.Attribute(XMLAttrFloorTempHi)).GetValueOrDefault(0);
            _floorTempLow = ((double?)_xElement.Attribute(XMLAttrFloorTempLo)).GetValueOrDefault(0);
            _histeresis = ((double?)_xElement.Attribute(XMLAttrHisteresis)).GetValueOrDefault(0);
            _actuator = ((int?)_xElement.Attribute(XMLAttrActuator)).GetValueOrDefault(0xFF);
            _lowTempMode = ((bool?)_xElement.Attribute(XMLAttrLowTempMode)).GetValueOrDefault(false);
            _currentRoomTemperature = ((double?)_xElement.Attribute(XMLAttrCurrRoomTemp)).GetValueOrDefault(0);
            _currentFloorTemperature = ((double?)_xElement.Attribute(XMLAttrCurrFloorTemp)).GetValueOrDefault(0);
            _actuatorOpened = ((bool?)_xElement.Attribute(XMLAttrActuatorOpened)).GetValueOrDefault(false);
        }
        #endregion
    }
}

