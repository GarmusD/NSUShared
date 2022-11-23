using System;
using System.Xml.Linq;
using System.Linq;
using NSU.Shared.DataContracts;

namespace NSU.Shared.NSUSystemPart
{
    public class Collector : NSUPartBase, ICollectorDataContract
    {
        #region Constants
        const string XMLAttrEnabled = "enabled";
        const string XMLAttrName = "name";
        const string XMLAttrConfigPos = "cfgpos";
        const string XMLAttrCircPumpName = "cpname";
        const string XMLAttrValvesCount = "actcount";
        #endregion

        #region Properties
        public int ConfigPos { get => _cfgPos; set => SetConfigPos(value); }
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public string Name { get => _name; set => SetName(value); }
        public string CircPumpName { get => _circPumpName; set => SetCircPumpName(value); }
        public int ActuatorsCount => ICollectorDataContract.MAX_COLLECTOR_ACTUATORS;
        public IThermoActuatorDataContract[] Actuators { get => _actuators; }
        #endregion

        #region Private fields
        private int _cfgPos = INVALID_VALUE;
        private bool _enabled = false;
        private string _name = string.Empty;
        private string _circPumpName = string.Empty;
        private ThermoActuator[] _actuators = new ThermoActuator[ICollectorDataContract.MAX_COLLECTOR_ACTUATORS];
        private XElement? _xElement = null;
        #endregion

        public Collector(ICollectorDataContract dataContract)
        {
            _cfgPos = dataContract.ConfigPos;
            _enabled = dataContract.Enabled;
            _name = dataContract.Name;
            _circPumpName = dataContract.CircPumpName;
            CreateDefaultActuators();
            foreach (var actuator in dataContract.Actuators)
            {
                _actuators[actuator.Index].Type = actuator.Type;
                _actuators[actuator.Index].RelayChannel = actuator.RelayChannel;
                _actuators[actuator.Index].Opened = actuator.Opened.GetValueOrDefault();
            }
        }

        private void CreateDefaultActuators()
        {
            _actuators = Enumerable
                .Range(0, ICollectorDataContract.MAX_COLLECTOR_ACTUATORS)
                .Select((n) => { return new ThermoActuator(n); })
                .ToArray();
        }

        #region Private methods

        private void SetConfigPos(int value)
        {
            if (_cfgPos != value)
            {
                _cfgPos = value;
                _xElement?.SetAttributeValue(XMLAttrConfigPos, _cfgPos);
                OnPropertyChanged(nameof(ConfigPos));
            }
        }

        private void SetEnabled(bool value)
        {
            if (_enabled != value)
            {
                _enabled = value;
                _xElement?.SetAttributeValue(XMLAttrEnabled, _enabled);
                OnPropertyChanged(nameof(Enabled));
            }
        }

        private void SetName(string value)
        {
            if (_name != value)
            {
                _name = value;
                _xElement?.SetAttributeValue(XMLAttrName, _name);
                OnPropertyChanged(nameof(Name));
            }
        }

        private void SetCircPumpName(string value)
        {
            if (_circPumpName != value)
            {
                _circPumpName = value;
                _xElement?.SetAttributeValue(XMLAttrCircPumpName, _circPumpName);
                OnPropertyChanged(nameof(CircPumpName));
            }
        }
        #endregion

        #region Public methods
        public void UpdateActuatorStatus(bool[] status)
        {
            bool changed = false;
            for(int i = 0; i < _actuators.Length; i++)
            {
                if (_actuators[i].Opened != status[i])
                {
                    changed = true;
                    _actuators[i].Opened = status[i];
                }
            }
            if (changed) OnPropertyChanged(nameof(Actuators));
        }

        override public void AttachXMLNode(XElement xml)
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml), "XElement cannot be null.");

            if (_xElement == null && !string.IsNullOrWhiteSpace(_name))
                _xElement = xml.Elements().FirstOrDefault(item => item.Attribute(XMLAttrName)?.Value == _name);

            if (_xElement != null)
                ReadXMLNode(_xElement);
            else
                SetNodeDefaults(xml);
        }

        private void SetNodeDefaults(XElement xml)
        {
            _xElement = new XElement("Collector");
            _xElement.Add(new XAttribute(XMLAttrConfigPos, Convert.ToString(_cfgPos)));
            _xElement.Add(new XAttribute(XMLAttrEnabled, Convert.ToString(_enabled)));
            _xElement.Add(new XAttribute(XMLAttrName, _name));
            _xElement.Add(new XAttribute(XMLAttrCircPumpName, _circPumpName));
            _xElement.Add(new XAttribute(XMLAttrValvesCount, Convert.ToString(ActuatorsCount)));
            foreach (var actuator in _actuators)
                ((ThermoActuator)actuator).AttachXMLNode(_xElement);
            xml.Add(_xElement);
        }

        override public void ReadXMLNode(XElement xml)
        {
            _xElement = xml;
            _cfgPos = ((int?)(int?)_xElement.Attribute(XMLAttrConfigPos)).GetValueOrDefault(INVALID_VALUE);
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            _name = (string)_xElement.Attribute(XMLAttrName);
            _circPumpName = (string)_xElement.Attribute(XMLAttrCircPumpName);
        }
        #endregion
    }
}

