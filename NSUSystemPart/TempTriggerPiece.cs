using NSU.Shared.DataContracts;
using System;
using System.Linq;
using System.Xml.Linq;

namespace NSU.Shared.NSUSystemPart
{
    public class TempTriggerPiece : ITempTriggerPieceDataContract
    {
        #region Constants
        private const string XMLAttrIndex = "idx";
        private const string XMLAttrEnabled = "enabled";
        private const string XMLAttrTempSensorName = "tsName";
        private const string XMLAttrTriggerCondition = "condition";
        private const string XMLAttrTemp = "temp";
        private const string XMLAttrHisteresis = "histeresis";
        #endregion

        #region Properties
        public byte Index { get => _idx; set => SetIndex(value); }
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public string TSensorName { get => _tsName; set => SetTSName(value); }
        public TriggerCondition Condition { get => _condition; set => SetCondition(value); }
        public double Temperature { get => _temp; set => SetTemperature(value); }
        public double Histeresis { get => _histeresis; set => SetHisteresis(value); }
        #endregion

        #region Private fields
        private byte _idx;
        private bool _enabled;
        private string _tsName;
        private TriggerCondition _condition;
        private double _temp;
        private double _histeresis;
        private XElement _xElement = null;
        #endregion


        public TempTriggerPiece(byte index)
        {
            _idx = index;
            _enabled = false;
            _tsName = string.Empty;
            _condition = TriggerCondition.TrueIfHigher;
            _temp = -127.0f;
            _histeresis = 0.0f;
        }

        #region Private methods
        /*******************************************************************
         * PRIVATE
         * *****************************************************************/
        private void SetIndex(byte value)
        {
            _idx = value;
            _xElement?.SetAttributeValue(XMLAttrIndex, _idx);
        }

        private void SetEnabled(bool value)
        {
            _enabled = value;
            _xElement?.SetAttributeValue(XMLAttrEnabled, _enabled);
        }

        private void SetTSName(string value)
        {
            _tsName = value;
            _xElement?.SetAttributeValue(XMLAttrTempSensorName, _tsName);
        }

        private void SetCondition(TriggerCondition value)
        {
            _condition = value;
            _xElement?.SetAttributeValue(XMLAttrTriggerCondition, _condition.ToString());
        }

        private void SetTemperature(double value)
        {
            _temp = value;
            _xElement?.SetAttributeValue(XMLAttrTemp, _temp);
        }

        private void SetHisteresis(double value)
        {
            _histeresis = value;
            _xElement?.SetAttributeValue(XMLAttrHisteresis, _histeresis);
        }

        #endregion

        #region Public methods
        /*******************************************************************
         * PUBLIC
         * *****************************************************************/
        public void AttachXMLNode(XElement xml)
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml), "XElement cannot be null.");

            if (_xElement == null)
                _xElement = xml.Elements().FirstOrDefault(item => item.Attribute(XMLAttrIndex)?.Value == _idx.ToString());

            if (_xElement != null)
                ReadXMLNode(_xElement);
            else
                CreateNodeDefaults(xml);
        }

        private void CreateNodeDefaults(XElement xml)
        {
            _xElement = new XElement("TempTriggerPiece");
            _xElement.Add(new XAttribute(XMLAttrIndex, _idx));
            _xElement.Add(new XAttribute(XMLAttrEnabled, _enabled));
            _xElement.Add(new XAttribute(XMLAttrTempSensorName, _tsName));
            _xElement.Add(new XAttribute(XMLAttrTriggerCondition, _condition.ToString()));
            _xElement.Add(new XAttribute(XMLAttrTemp, _temp));
            _xElement.Add(new XAttribute(XMLAttrHisteresis, _histeresis));
            xml.Add(_xElement);
        }

        public void ReadXMLNode(XElement xml)
        {
            _xElement = xml;
            _idx = ((byte?)(int?)_xElement.Attribute(XMLAttrIndex)).GetValueOrDefault(TempTrigger.INVALID_VALUE);
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            _tsName = (string)_xElement.Attribute(XMLAttrTempSensorName) ?? string.Empty;
            if (_xElement.Attribute(XMLAttrTriggerCondition) != null)
            {
                if (!Enum.TryParse(_xElement.Attribute(XMLAttrTriggerCondition).Value, out _condition))
                {
                    //value in numeric form?
                    if (int.TryParse(_xElement.Attribute(XMLAttrTriggerCondition).Value, out int i))
                        _condition = (TriggerCondition)i;
                    else
                        throw new NotSupportedException($"Invalid TriggerCondition value: '{_xElement.Attribute(XMLAttrTriggerCondition).Value}'");
                }
            }
            else
                _condition = TriggerCondition.TrueIfHigher;

            _temp = ((float?)_xElement.Attribute(XMLAttrTemp)).GetValueOrDefault(0f);
            _histeresis = ((float?)_xElement.Attribute(XMLAttrHisteresis)).GetValueOrDefault(0f);
        }
        #endregion
    }
}
