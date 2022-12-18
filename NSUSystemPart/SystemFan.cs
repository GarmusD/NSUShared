using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using NSU.Shared.DataContracts;

namespace NSU.Shared.NSUSystemPart
{
    public class SystemFan : NSUPartBase, ISystemFanDataContract
    {
        #region Constants
        private const string XMLAttrConfigPos = "cfgpos";
        private const string XMLAttrEnabled = "enabled";
        private const string XMLAttrName = "name";
        private const string XMLAttrTempSensorName = "tSensorName";
        private const string XMLAttrMinTemp = "minTemp";
        private const string XMLAttrMaxTemp = "maxTemp";
        private const string XMLAttrCurrentPWM = "currpwm";
        #endregion

        #region Properties
        public byte ConfigPos { get => _cfgPos; set => SetConfigPos(value); }
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public string Name { get => _name; set => SetName(value); }
        public string TempSensorName { get => _tsName; set => SetTempSensorName(value); }
        public double MinTemp { get => _minTemp; set => SetMinTemp(value); }
        public double MaxTemp { get => _maxTemp; set => SetMaxTemp(value); }
        public int CurrentPWM { get => _currentPWM; set => SetCurrentPWM(value); }
        #endregion

        #region Private fields
        private byte _cfgPos;
        private bool _enabled;
        private string _name;
        private string _tsName;
        private double _minTemp;
        private double _maxTemp;
        private int _currentPWM;
        XElement _xElement;
        #endregion
        

        public SystemFan()
        {
            _cfgPos = 255;
            _enabled = false;
            _name = string.Empty;
            _tsName = string.Empty;
            _minTemp = 0;
            _maxTemp = 0;
            _currentPWM = 0;
            _xElement = null;
        }

        public SystemFan(ISystemFanDataContract dataContract)
        {
            _cfgPos = dataContract.ConfigPos;
            _enabled = dataContract.Enabled;
            _name = dataContract.Name;
            _tsName = dataContract.TempSensorName;
            _minTemp = dataContract.MinTemp;
            _maxTemp = dataContract.MaxTemp;
            _currentPWM = dataContract.CurrentPWM;
            _xElement = null;
        }

        #region Private methods.
        /// <summary>
        /// ConfigPos
        /// </summary>
        /// <param name="value">ConfigPos in MCU settings file</param>
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

        private void SetMinTemp(double value)
        {
            _minTemp = value;
            _xElement?.SetAttributeValue(XMLAttrMinTemp, _minTemp);
        }

        private void SetMaxTemp(double value)
        {
            _maxTemp = value;
            _xElement?.SetAttributeValue(XMLAttrMaxTemp, _maxTemp);
        }

        private void SetCurrentPWM(int value)
        {
            if (_currentPWM != value)
            {
                _currentPWM = value;
                _xElement?.SetAttributeValue(XMLAttrCurrentPWM, _currentPWM);
                OnPropertyChanged(nameof(CurrentPWM));
            }
        }

        private void CreateNodeDefaults(XElement xml)
        {
            _xElement = new XElement("SystemFan");
            _xElement.Add(new XAttribute(XMLAttrConfigPos, _cfgPos.ToString()));
            _xElement.Add(new XAttribute(XMLAttrEnabled, _enabled.ToString()));
            _xElement.Add(new XAttribute(XMLAttrName, _name));
            _xElement.Add(new XAttribute(XMLAttrTempSensorName, _tsName));
            _xElement.Add(new XAttribute(XMLAttrMinTemp, _minTemp.ToString()));
            _xElement.Add(new XAttribute(XMLAttrMaxTemp, _maxTemp.ToString()));
            _xElement.Add(new XAttribute(XMLAttrCurrentPWM, _currentPWM.ToString()));
            xml.Add(_xElement);
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

        override public void ReadXMLNode(XElement xml)
        {
            _xElement = xml;
            _cfgPos = ((byte?)(int?)_xElement.Attribute(XMLAttrConfigPos)).GetValueOrDefault(0xFF);
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            _name = (string)_xElement.Attribute(XMLAttrName);
            _tsName = (string)_xElement.Attribute(XMLAttrTempSensorName);
            _minTemp = ((double?)_xElement.Attribute(XMLAttrMinTemp)).GetValueOrDefault(0);
            _maxTemp = ((double?)_xElement.Attribute(XMLAttrMaxTemp)).GetValueOrDefault(0);
            _currentPWM = ((int?)_xElement.Attribute(XMLAttrCurrentPWM)).GetValueOrDefault(0);
        }
        #endregion
    }
}
