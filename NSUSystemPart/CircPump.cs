using System;
using System.Xml.Linq;
using System.Linq;
using NSU.Shared.DataContracts;

namespace NSU.Shared.NSUSystemPart
{
    public class CircPump : NSUPartBase, ICircPumpDataContract
    {
        #region Constants
        private const string XMLAttrConfigPos = "cfgpos";
        private const string XMLAttrEnabled = "enabled";
        private const string XMLAttrName = "name";
        private const string XMLAttrTriggerName = "trgname";
        private const string XMLAttrCurrSpeed = "currspd";
        private const string XMLAttrMaxSpeed = "maxSpeed";
        private const string XMLAttrSpd1Channel = "spd1Channel";
        private const string XMLAttrSpd2Channel = "spd2Channel";
        private const string XMLAttrSpd3Channel = "spd3Channel";
        private const string XMLAttrStatus = "status";
        private const string XMLAttrOpenedValvesCount = "openedvalves";
        #endregion

        #region Events
        public event EventHandler<EventArgs> Clicked;
        //public event EventHandler<StatusChangedEventArgs>? StatusChanged;
        #endregion

        #region Properties
        public byte ConfigPos { get => _cfgpos; set => SetConfigPos(value); }
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public string Name { get => _name; set => SetName(value); }
        public string TempTriggerName { get => _tempTriggerName; set => SetTriggerName(value); }
        public byte CurrentSpeed { get => _currSpeed; set => SetCurrentSpeed(value); }
        public byte MaxSpeed { get => _maxSpeed; set => SetMaxSpeed(value); }
        public byte Spd1Channel { get => _spd1Channel; set => SetSpd1Channel(value); }
        public byte Spd2Channel { get => _spd2Channel; set => SetSpd2Channel(value); }
        public byte Spd3Channel { get => _spd3Channel; set => SetSpd3Channel(value); }
        public Status Status { get => _status; set => SetStatus(value); }
        public int OpenedValvesCount { get => _openedValvesCount; set => SetOpenedValvesCount(value); }
        #endregion

        #region Private fields
        private byte _cfgpos;
        private bool _enabled;
        private string _name;
        private string _tempTriggerName;
        private byte _currSpeed;
        private byte _maxSpeed;
        private byte _spd1Channel;
        private byte _spd2Channel;
        private byte _spd3Channel;
        private Status _status;
        private int _openedValvesCount;
        private bool _pendingChange = false;

        private XElement _xElement = null;
        #endregion



        public CircPump()
        {
            _cfgpos = INVALID_VALUE;
            _enabled = false;
            _name = string.Empty;
            _tempTriggerName = string.Empty;
            _currSpeed = 0;
            _maxSpeed = 1;
            _spd1Channel = 0xFF;
            _spd2Channel = 0xFF;
            _spd3Channel = 0xFF;
            _status = Status.UNKNOWN;
            _openedValvesCount = 0;
        }

        public CircPump(ICircPumpDataContract circPumpData)
        {
            _cfgpos = circPumpData.ConfigPos;
            _enabled = circPumpData.Enabled;
            _name = circPumpData.Name;
            _tempTriggerName = circPumpData.TempTriggerName;
            _currSpeed = circPumpData.CurrentSpeed;
            _maxSpeed = circPumpData.MaxSpeed;
            _spd1Channel = circPumpData.Spd1Channel;
            _spd2Channel = circPumpData.Spd2Channel;
            _spd3Channel = circPumpData.Spd3Channel;
            _status = circPumpData.Status;
            _openedValvesCount = circPumpData.OpenedValvesCount;
        }

        #region Private Methods
        private void SetConfigPos(byte value)
        {
            _cfgpos = value;
            _xElement?.SetAttributeValue(XMLAttrConfigPos, _cfgpos);
        }

        private void SetEnabled(bool value)
        {
            _enabled = value;
            _xElement?.SetAttributeValue(XMLAttrEnabled, _enabled);
        }

        private void SetName(string value)
        {
            if (!_name.Equals(value))
            {
                _name = value;
                _xElement?.SetAttributeValue(XMLAttrName, _name);
            }
        }

        private void SetTriggerName(string value)
        {
            _tempTriggerName = value;
            _xElement?.SetAttributeValue(XMLAttrTriggerName, _tempTriggerName);
        }

        private void SetCurrentSpeed(byte value)
        {
            _currSpeed = value;
            _pendingChange = true;
            _xElement?.SetAttributeValue(XMLAttrCurrSpeed, _currSpeed);
        }

        private void SetMaxSpeed(byte value)
        {
            _maxSpeed = value;
            _xElement?.SetAttributeValue(XMLAttrMaxSpeed, _maxSpeed);
        }

        private void SetSpd1Channel(byte value)
        {
            _spd1Channel = value;
            _xElement?.SetAttributeValue(XMLAttrSpd1Channel, _spd1Channel);
        }

        private void SetSpd2Channel(byte value)
        {
            _spd2Channel = value;
            _xElement?.SetAttributeValue(XMLAttrSpd2Channel, _spd2Channel);
        }

        private void SetSpd3Channel(byte value)
        {
            _spd3Channel = value;
            _xElement?.SetAttributeValue(XMLAttrSpd3Channel, _spd3Channel);
        }

        private void SetStatus(Status value)
        {
            if (_status != value)
            {
                _status = value;
                _pendingChange = true;
                _xElement?.SetAttributeValue(XMLAttrStatus, _status);    
            }
            if (_pendingChange)
            {
                _pendingChange = false;
                OnStatusChanged();
            }
        }

        private void SetOpenedValvesCount(int value)
        {
            _openedValvesCount = value;
            _pendingChange = true;
            _xElement?.SetAttributeValue(XMLAttrOpenedValvesCount, _openedValvesCount);
        }

        private void CreateNodeDefaults(XElement xml)
        {
            _xElement = new XElement("CircPump");
            _xElement.Add(new XAttribute(XMLAttrConfigPos, Convert.ToString(_cfgpos)));
            _xElement.Add(new XAttribute(XMLAttrEnabled, Convert.ToString(_enabled)));
            _xElement.Add(new XAttribute(XMLAttrName, _name));
            _xElement.Add(new XAttribute(XMLAttrTriggerName, _tempTriggerName));
            _xElement.Add(new XAttribute(XMLAttrCurrSpeed, Convert.ToString(_currSpeed)));
            _xElement.Add(new XAttribute(XMLAttrMaxSpeed, Convert.ToString(_maxSpeed)));
            _xElement.Add(new XAttribute(XMLAttrSpd1Channel, Convert.ToString(_spd1Channel)));
            _xElement.Add(new XAttribute(XMLAttrSpd2Channel, Convert.ToString(_spd2Channel)));
            _xElement.Add(new XAttribute(XMLAttrSpd3Channel, Convert.ToString(_spd3Channel)));
            _xElement.Add(new XAttribute(XMLAttrStatus, _status.ToString()));
            _xElement.Add(new XAttribute(XMLAttrOpenedValvesCount, Convert.ToString(_openedValvesCount)));
            xml.Add(_xElement);
        }
        #endregion Private Methods

        #region Public Methods
        public void OnClicked()
        {
            var evt = Clicked;
            evt?.Invoke(this, new EventArgs());
        }

        private void OnStatusChanged()
        {
            OnPropertyChanged(nameof(Status));
        }

        override public void AttachXMLNode(XElement xml)
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml));

            if (_xElement == null && !string.IsNullOrWhiteSpace(_name))
                _xElement = xml.Elements().FirstOrDefault(item => item.Attribute(XMLAttrName)?.Value == _name);

            if (_xElement != null)
                ReadXMLNode(_xElement);
            else
                CreateNodeDefaults(xml);
        }

        override public void ReadXMLNode(XElement xml)
        {
            _xElement = xml;
            _cfgpos = ((byte?)(int?)_xElement.Attribute(XMLAttrConfigPos)).GetValueOrDefault(INVALID_VALUE);
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            _name = (string)_xElement.Attribute(XMLAttrName);
            _tempTriggerName = (string)_xElement.Attribute(XMLAttrTriggerName);
            _currSpeed = ((byte?)(int?)_xElement.Attribute(XMLAttrCurrSpeed)).GetValueOrDefault(0);
            _maxSpeed = ((byte?)(int?)_xElement.Attribute(XMLAttrMaxSpeed)).GetValueOrDefault(1);
            _spd1Channel = ((byte?)(int?)_xElement.Attribute(XMLAttrSpd1Channel)).GetValueOrDefault(INVALID_VALUE);
            _spd2Channel = ((byte?)(int?)_xElement.Attribute(XMLAttrSpd2Channel)).GetValueOrDefault(INVALID_VALUE);
            _spd3Channel = ((byte?)(int?)_xElement.Attribute(XMLAttrSpd3Channel)).GetValueOrDefault(INVALID_VALUE);
            _status = ((Status?)(int?)_xElement.Attribute(XMLAttrStatus)).GetValueOrDefault(Status.OFF);
            _openedValvesCount = ((int?)(int?)_xElement.Attribute(XMLAttrOpenedValvesCount)).GetValueOrDefault(0);
        }
        #endregion
    }
}
