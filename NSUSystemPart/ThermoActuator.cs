using NSU.Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.NSUSystemPart
{
    public enum ActuatorType
    {
        NC,
        NO
    };

    public class ThermoActuator : IThermoActuatorDataContract
    {
        #region Constants
        private const string XMLAttrIndex = "idx";
        private const string XMLAttrType = "type";
        private const string XMLAttrRelayChannel = "ch";
        private const string XMLAttrOpened = "opened";
        #endregion        

        #region Properties
        public int Index { get; set; }
        public ActuatorType Type { get => _type; set => SetType(value); }
        public int RelayChannel { get => _relayChannel; set => SetRelayChannel(value); }
        public bool? Opened { get => _opened; set => SetOpened(value); }
        #endregion

        #region Private fields
        private bool? _opened = false;
        private ActuatorType _type = ActuatorType.NC;
        private int _relayChannel = NSUPartBase.INVALID_VALUE;
        private XElement? _xElement = null;
        #endregion


        public ThermoActuator(int idx)
        {
            Index = idx;
        }

        #region Private methods

        private void SetType(ActuatorType value)
        {
            _type = value;
            _xElement?.SetAttributeValue(XMLAttrType, _type);
        }

        private void SetRelayChannel(int value)
        {
            _relayChannel = value;
            _xElement?.SetAttributeValue(XMLAttrRelayChannel, _relayChannel);
        }

        private void SetOpened(bool? value)
        {
            if (_opened != value)
            {
                _opened = value;
                _xElement?.SetAttributeValue(XMLAttrOpened, _opened.GetValueOrDefault());
            }
        }
        #endregion

        #region Public methods

        public void AttachXMLNode(XElement node)
        {
            _xElement = node.Elements().FirstOrDefault(item => item.Attribute(XMLAttrIndex)?.Value == Index.ToString());
            
            if (_xElement != null)
                ReadXMLNode(_xElement);
            else
                SetNodeDefaults(node);
        }

        private void SetNodeDefaults(XElement node)
        {
            _xElement = new XElement("Actuator");
            _xElement.Add(new XAttribute(XMLAttrIndex, Convert.ToString(Index)));
            _xElement.Add(new XAttribute(XMLAttrType, Convert.ToString((int)Type)));
            _xElement.Add(new XAttribute(XMLAttrRelayChannel, Convert.ToString(RelayChannel)));
            _xElement.Add(new XAttribute(XMLAttrOpened, Convert.ToString(Opened)));
            node.Add(_xElement);
        }

        public void ReadXMLNode(XElement xml)
        {
            _xElement = xml;
            _type = ((ActuatorType?)(int?)_xElement.Attribute(XMLAttrType)).GetValueOrDefault(ActuatorType.NC);
            _relayChannel = ((int?)(int?)_xElement.Attribute(XMLAttrRelayChannel)).GetValueOrDefault(Collector.INVALID_VALUE);
            _opened = ((bool?)_xElement.Attribute(XMLAttrOpened)).GetValueOrDefault(false);
        }

        #endregion
    }
}
