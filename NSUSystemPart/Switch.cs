using System;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Linq;
using NSU.Shared.DataContracts;

namespace NSU.Shared.NSUSystemPart
{
    public class Switch : NSUPartBase, ISwitchDataContract
    {
        #region
        private const string XMLAttrEnabled = "enabled";
        private const string XMLAttrName = "name";
        private const string XMLAttrConfigPos = "cfgpos";
        private const string XMLAttrDependName = "depend";
        private const string XMLAttrStatus = "status";
        private const string XMLAttrOnDepStatus = "onDepStatus";
        private const string XMLAttrForceStatus = "forceStatus";
        private const string XMLAttrIsForced = "isForced";
        #endregion

        #region Events
        public event EventHandler<EventArgs>? Clicked;
        #endregion


        #region Properties
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public string Name { get => _name; set => SetName(value); }
        public int ConfigPos { get => _cfgpos; set => SetConfigPos(value); }
        public string Dependancy { get => _depend; set => SetDependName(value); }
        public Status Status { get => _status; set => SetStatus(value); }
        public Status OnDependancyStatus { get => _onDependancyStatus; set => SetOnDependancyStatus(value); }
        public Status ForceStatus { get => _forceStatus; set => SetForceStatus(value); }
        public bool IsForced { get => _isForced; set => SetIsForced(value); }
        #endregion

        #region Private field
        private bool _enabled;
        private string _name;
        private int _cfgpos;
        private string _depend;
        private Status _status;
        private Status _onDependancyStatus;
        private Status _forceStatus;
        private bool _isForced;
        private XElement? _xElement;
        #endregion

        
        public Switch()
        {
            _enabled = false;
            _name = string.Empty;
            _cfgpos = INVALID_VALUE;
            _depend = string.Empty;
            _status = Status.UNKNOWN;
            _onDependancyStatus = Status.UNKNOWN;
            _forceStatus = Status.UNKNOWN;
            _isForced = false;
            _xElement = null;
        }

        public Switch(ISwitchDataContract dataContract)
        {
            _enabled = dataContract.Enabled;
            _name = dataContract.Name;
            _cfgpos = dataContract.ConfigPos;
            _depend = dataContract.Dependancy;
            _status = dataContract.Status;
            _onDependancyStatus = dataContract.OnDependancyStatus;
            _forceStatus = dataContract.ForceStatus;
            _isForced = dataContract.IsForced;
            _xElement = null;
        }

        #region Private methods
        private void SetConfigPos(int value)
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
            _name = value;
            _xElement?.SetAttributeValue(XMLAttrName, _name);
        }

        private void SetDependName(string value)
        {
            _depend = value;
            _xElement?.SetAttributeValue(XMLAttrDependName, _depend);
        }

        private void SetStatus(Status value)
        {
            if (_status != value)
            {
                _status = value;
                _xElement?.SetAttributeValue(XMLAttrStatus, _status);
                OnPropertyChanged(nameof(Status));
            }
        }

        private void SetOnDependancyStatus(Status value)
        {
            _onDependancyStatus = value;
            _xElement?.SetAttributeValue(XMLAttrOnDepStatus, _onDependancyStatus);
        }

        private void SetForceStatus(Status value)
        {
            _forceStatus = value;
            _xElement?.SetAttributeValue(XMLAttrForceStatus, _forceStatus);
        }

        private void SetIsForced(bool value)
        {
            _isForced = value;
            _xElement?.SetAttributeValue(XMLAttrIsForced, _isForced);
        }

        private void CreateNodeDefaults(XElement xml)
        {
            _xElement = new XElement("Switch");
            _xElement.Add(new XAttribute(XMLAttrEnabled, _enabled));
            _xElement.Add(new XAttribute(XMLAttrName, _name));
            _xElement.Add(new XAttribute(XMLAttrConfigPos, _cfgpos));
            _xElement.Add(new XAttribute(XMLAttrDependName, _depend));
            _xElement.Add(new XAttribute(XMLAttrStatus, _status));
            _xElement.Add(new XAttribute(XMLAttrOnDepStatus, _onDependancyStatus));
            _xElement.Add(new XAttribute(XMLAttrForceStatus, _forceStatus));
            _xElement.Add(new XAttribute(XMLAttrIsForced, _isForced));
            xml.Add(_xElement);
        }
        #endregion

        #region Public methods
        public override string ToString ()
        {
            //0         1     2    3        4        5
            //ConfigPos Valid Name DependOn depstate forcestate
            return JsonConvert.SerializeObject(this);
        }

        public void OnClicked()
        {
            var evt = Clicked;
            evt?.Invoke(this, new EventArgs());
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
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            _name = (string)_xElement.Attribute(XMLAttrName);
            _cfgpos = ((int?)_xElement.Attribute(XMLAttrConfigPos)).GetValueOrDefault(INVALID_VALUE);
            _depend = (string)_xElement.Attribute(XMLAttrDependName);
            _status = NSUUtils.Utils.GetStatusFromString(_xElement.Attribute(XMLAttrStatus)?.Value, Status.UNKNOWN);
            _onDependancyStatus = NSUUtils.Utils.GetStatusFromString(_xElement.Attribute(XMLAttrOnDepStatus)?.Value, Status.UNKNOWN);
            _forceStatus = NSUUtils.Utils.GetStatusFromString(_xElement.Attribute(XMLAttrForceStatus)?.Value, Status.UNKNOWN);
            _isForced = ((bool?)_xElement.Attribute(XMLAttrIsForced)).GetValueOrDefault(false);
        }
        #endregion
    }
}

