using NSU.Shared.DataContracts;
using System;
using System.Linq;
using System.Xml.Linq;

namespace NSU.Shared.NSUSystemPart
{
    public class ElHeatingData : IElHeatingDataDataContract
    {
        private const string XMLAttrIndex = "idx";
        private const string XMLAttrStartHour = "startHour";
        private const string XMLAttrStartMin = "startMin";
        private const string XMLAttrEndHour = "endHour";
        private const string XMLAttrEndMin = "endMin";

        public byte Index { get; set; }
        public byte StartHour { get => _startHour; set => SetStartHour(value); }
        public byte StartMin { get => _startMin; set => SetStartMin(value); }
        public byte EndHour { get => _endHour; set => SetEndHour(value); }
        public byte EndMin { get => _endMin; set => SetEndMin(value); }

        private byte _startHour = WaterBoiler.INVALID_VALUE;
        private byte _startMin = WaterBoiler.INVALID_VALUE;
        private byte _endHour = WaterBoiler.INVALID_VALUE;
        private byte _endMin = WaterBoiler.INVALID_VALUE;
        private XElement _xElement = null;


        public ElHeatingData(byte index)
        {
            Index = index;
        }

        /* **************************************************************************
         * PRIVATE
         * **************************************************************************/
        private void SetStartHour(byte value)
        {
            _startHour = value;
            _xElement?.SetAttributeValue(XMLAttrStartHour, _startHour);
        }

        private void SetStartMin(byte value)
        {
            _startMin = value;
            _xElement?.SetAttributeValue(XMLAttrStartMin, _startMin);
        }

        private void SetEndHour(byte value)
        {
            _endHour = value;
            _xElement?.SetAttributeValue(XMLAttrEndHour, _endHour);
        }

        private void SetEndMin(byte value)
        {
            _endMin = value;
            _xElement?.SetAttributeValue(XMLAttrEndMin, _endMin);
        }

        /* **************************************************************************
        * PUBLIC
        * **************************************************************************/
        public void AttachXMLNode(XElement xml)
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml), "XElement cannot be null.");

            if (_xElement == null)
                _xElement = xml.Elements().FirstOrDefault(item => item.Attribute(XMLAttrIndex)?.Value == Index.ToString());

            if (_xElement != null)
                ReadXMLNode(_xElement);
            else
                CreateNodeDefaults(xml);
        }

        private void CreateNodeDefaults(XElement xml)
        {
            _xElement = new XElement("TempTriggerPiece");
            _xElement.Add(new XAttribute(XMLAttrIndex, Index));
            _xElement.Add(new XAttribute(XMLAttrStartHour, _startHour));
            _xElement.Add(new XAttribute(XMLAttrStartMin, _startMin));
            _xElement.Add(new XAttribute(XMLAttrEndHour, _endHour));
            _xElement.Add(new XAttribute(XMLAttrEndMin, _endMin));
            xml.Add(_xElement);
        }

        public void ReadXMLNode(XElement xml)
        {
            _xElement = xml;
            Index = ((byte?)(int?)_xElement.Attribute(XMLAttrIndex)).GetValueOrDefault(0xFF);
            _startHour = ((byte?)(int?)_xElement.Attribute(XMLAttrStartHour)).GetValueOrDefault(0xFF);
            _startMin = ((byte?)(int?)_xElement.Attribute(XMLAttrStartMin)).GetValueOrDefault(0xFF);
            _endHour = ((byte?)(int?)_xElement.Attribute(XMLAttrEndHour)).GetValueOrDefault(0xFF);
            _endMin = ((byte?)(int?)_xElement.Attribute(XMLAttrEndMin)).GetValueOrDefault(0xFF);
        }
    }
}
