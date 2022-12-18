using NSU.Shared.DataContracts;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace NSU.Shared.NSUSystemPart
{
    public class WaterBoiler : NSUPartBase, IWaterBoilerDataContract
    {
        #region Constants
        public const int MAX_WATERBOILER_EL_HEATING_COUNT = 7;

        private const string XMLAttrName = "name";
        private const string XMLAttrConfigPos = "cfgpos";
        private const string XMLAttrEnabled = "enabled";
        private const string XMLAttrTempSensorName = "tsname";
        private const string XMLAttrTempTriggerName = "ttrgname";
        private const string XMLAttrCircPumpName = "cpname";
        private const string XMLAttrElHeatingEnabled = "ehenabled";
        private const string XMLAttrElHeatingChannel = "elHeatingCh";
        #endregion

        #region Properties
        public byte ConfigPos { get => _cfgPos; set => SetConfigPos(value); }
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public string Name { get => _name; set => SetName(value); }
        public string TempSensorName { get => _tsName; set => SetTempSensorName(value); }
        public string TempTriggerName { get => _ttrgName; set => SetTempTriggerName(value); }
        public string CircPumpName { get => _cpName; set => SetCircPumpName(value); }
        public bool ElHeatingEnabled { get => _elHeatingEnabled; set => SetElHeatingEnabled(value); }
        public int ElHeatingChannel { get => _elHeatingChannel; set => SetElHeatingChannel(value); }
        public IElHeatingDataDataContract[] ElHeatingData { get => _heatingData; set => throw new InvalidOperationException("ElHeatingData cannot be set directly."); }

        [IndexerName("PowerData")]
        public IElHeatingDataDataContract this[int index] => _heatingData[index];
        #endregion

        #region Private fields
        private byte _cfgPos = INVALID_VALUE;
        private bool _enabled = false;
        private string _name = string.Empty;
        private string _tsName = string.Empty;
        private string _ttrgName = string.Empty;
        private string _cpName = string.Empty;
        private bool _elHeatingEnabled = false;
        private int _elHeatingChannel = INVALID_VALUE;
        private readonly ElHeatingData[] _heatingData;
        private XElement _xElement = null;
        #endregion


        public WaterBoiler()
        {
            _heatingData = Enumerable.Range(0, MAX_WATERBOILER_EL_HEATING_COUNT).Select((i) => new ElHeatingData((byte)i)).ToArray();
        }

        public WaterBoiler(IWaterBoilerDataContract dataContract)
        {
            _cfgPos = dataContract.ConfigPos;
            _enabled = dataContract.Enabled;
            _name = dataContract.Name;
            _tsName = dataContract.TempSensorName;
            _ttrgName = dataContract.TempTriggerName;
            _cpName = dataContract.CircPumpName;
            _elHeatingEnabled = dataContract.ElHeatingEnabled;
            _elHeatingChannel = dataContract.ElHeatingChannel;
            _heatingData = Enumerable.Range(0, MAX_WATERBOILER_EL_HEATING_COUNT).Select((i) => new ElHeatingData((byte)i) 
            {
                StartHour = dataContract.ElHeatingData[i].StartHour,
                StartMin = dataContract.ElHeatingData[i].StartMin,
                EndHour = dataContract.ElHeatingData[i].EndHour,
                EndMin = dataContract.ElHeatingData[i].EndMin
            }).ToArray();
        }

        #region Private methods
        /* **************************************************************************
         * PRIVATE
         * **************************************************************************/
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

        private void SetTempSensorName(string value)
        {
            _tsName = value;
            _xElement?.SetAttributeValue(XMLAttrTempSensorName, _tsName);
        }

        private void SetTempTriggerName(string value)
        {
            _ttrgName = value;
            _xElement?.SetAttributeValue(XMLAttrTempTriggerName, _ttrgName);
        }

        private void SetCircPumpName(string value)
        {
            _cpName = value;
            _xElement?.SetAttributeValue(XMLAttrCircPumpName, _cpName);
        }

        private void SetElHeatingEnabled(bool value)
        {
            _elHeatingEnabled = value;
            _xElement?.SetAttributeValue(XMLAttrElHeatingEnabled, _elHeatingEnabled);
        }

        private void SetElHeatingChannel(int value)
        {
            _elHeatingChannel = value;
            _xElement?.SetAttributeValue(XMLAttrElHeatingChannel, _elHeatingChannel);
        }
        #endregion

        #region Public methods
        /* **************************************************************************
         * PUBLIC
         * **************************************************************************/

        public ElHeatingData GetHeatingDataByIndex(int index)
        {
            return _heatingData.FirstOrDefault(x => x.Index == index);
        }

        public override void AttachXMLNode(XElement xml)
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
            _xElement = new XElement("WaterBoiler");
            _xElement.Add(new XAttribute(XMLAttrConfigPos, _cfgPos));
            _xElement.Add(new XAttribute(XMLAttrEnabled, _enabled));
            _xElement.Add(new XAttribute(XMLAttrName, _name));
            _xElement.Add(new XAttribute(XMLAttrTempSensorName, _tsName));
            _xElement.Add(new XAttribute(XMLAttrTempTriggerName, _ttrgName));
            _xElement.Add(new XAttribute(XMLAttrCircPumpName, _cpName));
            _xElement.Add(new XAttribute(XMLAttrElHeatingEnabled, _elHeatingEnabled));
            _xElement.Add(new XAttribute(XMLAttrElHeatingChannel, _elHeatingChannel));
            foreach (var data in _heatingData) data.AttachXMLNode(_xElement);
            xml.Add(_xElement);
        }

        public override void ReadXMLNode(XElement xml)
        {
            _xElement = xml;
            _cfgPos = ((byte?)(int?)_xElement.Attribute(XMLAttrConfigPos)).GetValueOrDefault(INVALID_VALUE);
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            _name = (string)_xElement.Attribute(XMLAttrName) ?? string.Empty;
            _tsName = (string)_xElement.Attribute(XMLAttrTempSensorName) ?? string.Empty;
            _ttrgName = (string)_xElement.Attribute(XMLAttrTempTriggerName) ?? string.Empty;
            _cpName = (string)_xElement.Attribute(XMLAttrCircPumpName) ?? string.Empty;
            _elHeatingEnabled = ((bool?)_xElement.Attribute(XMLAttrElHeatingEnabled)).GetValueOrDefault(false);
            _elHeatingChannel = ((int?)_xElement.Attribute(XMLAttrElHeatingChannel)).GetValueOrDefault(INVALID_VALUE);
            foreach (var data in _heatingData) data.AttachXMLNode(_xElement);
        }
        #endregion
    }
}
