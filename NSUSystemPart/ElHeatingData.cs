using NSU.Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public int Index { get; set; }
        public int StartHour { get => _startHour; set => SetStartHour(value); }
        public int StartMin { get => _startMin; set => SetStartMin(value); }
        public int EndHour { get => _endHour; set => SetEndHour(value); }
        public int EndMin { get => _endMin; set => SetEndMin(value); }

        private int _startHour = WaterBoiler.INVALID_VALUE;
        private int _startMin = WaterBoiler.INVALID_VALUE;
        private int _endHour = WaterBoiler.INVALID_VALUE;
        private int _endMin = WaterBoiler.INVALID_VALUE;
        private XElement? _xElement = null;


        public ElHeatingData(int index)
        {
            Index = index;
        }

        /* **************************************************************************
         * PRIVATE
         * **************************************************************************/
        private void SetStartHour(int value)
        {
            _startHour = value;
            _xElement?.SetAttributeValue(XMLAttrStartHour, _startHour);
        }

        private void SetStartMin(int value)
        {
            _startMin = value;
            _xElement?.SetAttributeValue(XMLAttrStartMin, _startMin);
        }

        private void SetEndHour(int value)
        {
            _endHour = value;
            _xElement?.SetAttributeValue(XMLAttrEndHour, _endHour);
        }

        private void SetEndMin(int value)
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

            _xElement ??= xml.Elements().FirstOrDefault(item => item.Attribute(XMLAttrIndex)?.Value == Index.ToString());

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
            Index = ((int?)_xElement.Attribute(XMLAttrIndex)).GetValueOrDefault(0xFF);
            _startHour = ((int?)_xElement.Attribute(XMLAttrStartHour)).GetValueOrDefault(0xFF);
            _startMin = ((int?)_xElement.Attribute(XMLAttrStartMin)).GetValueOrDefault(0xFF);
            _endHour = ((int?)_xElement.Attribute(XMLAttrEndHour)).GetValueOrDefault(0xFF);
            _endMin = ((int?)_xElement.Attribute(XMLAttrEndMin)).GetValueOrDefault(0xFF);
        }
    }
}
