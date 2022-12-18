using NSU.Shared.NSUUtils;
using System.Xml.Linq;
using System;
using System.Linq;
using NSU.Shared.DataContracts;

namespace NSU.Shared.NSUSystemPart
{
    public enum WoodBoilerTempStatus
    {
        GrowingFast,
        Growing,
        Stable,
        Lowering,
        LoweringFast
    };

    public enum SwitchTarget
    {
        ExhaustFan,
        Ladomat
    }

    public enum NSUAction
    {
        WoodBoilerIkurimas
    }

    public class WoodBoiler : NSUPartBase, IWoodBoilerDataContract
    {
        private const string XMLAttrConfigPos = "cfgpos";
        private const string XMLAttrName = "name";
        private const string XMLAttrEnabled = "enabled";
        private const string XMLAttrTempSensorName = "tSensorName";
        private const string XMLAttrKTypeName = "kTypeName";
        private const string XMLAttrLadomatChannel = "ladomChannel";
        private const string XMLAttrLadomatTemp = "ladomatTemp";
        private const string XMLAttrLadomatStatus = "ladomStatus";
        private const string XMLAttrLadomatIsManual = "ladomIsManual";
        private const string XMLAttrLadomatTriggerName = "ladomatTriggerName";
        private const string XMLAttrExhaustFanChannel = "exhaustFanChannel";
        private const string XMLAttrExhaustFanStatus = "exhaustFanStatus";
        private const string XMLAttrExhaustFanIsManual = "exhaustIsManual";
        private const string XMLAttrWorkingTemp = "workingTemp";
        private const string XMLAttrHisteresis = "histeresis";
        private const string XMLAttrWaterBoilerName = "waterBName";
        private const string XMLAttrWoodBoilerStatus = "wbstatus";
        private const string XMLAttrWoodBoilerTempStatus = "tempStatus";
        private const string XMLAttrCurrentTemp = "currentTemp";

        public event EventHandler<SwitchStateChangedEventArgs> SwitchStateChanged;
        public event EventHandler<NSUActionEventArgs> Action;

        public byte ConfigPos { get => _cfgPos; set => SetConfigPos(value); }
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public string Name { get => _name; set => SetName(value); }
        public string TSensorName { get => _tSensorName; set => SetTSensorName(value); }
        public string KTypeName { get => _kTypeName; set => SetKTypeName(value); }
        public int LadomChannel { get => _ladomatChannel; set => SetLadomatChannel(value); }
        public Status LadomStatus { get => GetLadomatStatus(); set => SetLadomatStatus(value); }
        public bool LadomatIsManual { get => _ladomatIsManual; set => SetLadomatIsManual(value); }
        public int ExhaustFanChannel { get => _exhaustFanChannel; set => SetExhaustFanChannel(value); }
        public Status ExhaustFanStatus { get => GetExhaustFanStatus(); set => SetExhaustFanStatus(value); }
        public bool ExhaustFanIsManual { get => _exhaustFanIsManual; set => SetExhaustFanIsManual(value); }
        public double WorkingTemp { get => _workingTemp; set => SetWorkingTemp(value); }
        public double Histeresis { get => _histeresis; set => SetHisteresis(value); }
        public WoodBoilerStatus WBStatus { get => _status; set => SetWBStatus(value); }
        public double CurrentTemp { get => _currentTemp; set => SetCurrentTemp(value); }
        public WoodBoilerTempStatus TempStatus { get => _tempStatus; set => SetTempStatus(value); }
        public double LadomatTemp { get => _ladomatTemp; set => SetLadomatTemp(value); }
        public string LadomatTriggerName { get => _ladomatTriggerName; set => SetLadomatTriggerName(value); }
        public string WaterBoilerName { get => _wtrBName; set => SetWaterBoilerName(value); }


        private byte _cfgPos;
        private bool _enabled;
        private string _name;
        private string _tSensorName;
        private string _kTypeName;
        private int _ladomatChannel;
        private double _ladomatTemp;
        private bool _ladomatIsManual;
        private Status _ladomatStatus;
        private string _ladomatTriggerName;
        private int _exhaustFanChannel;
        private Status _exhaustFanStatus;
        private bool _exhaustFanIsManual;
        private double _workingTemp;
        private double _histeresis;
        private WoodBoilerStatus _status;
        private WoodBoilerTempStatus _tempStatus;
        private double _currentTemp;
        private string _wtrBName;
        private XElement _xElement;

        public WoodBoiler()
        {
            _cfgPos = 0xFF;
            _enabled = false;
            _name = string.Empty;
            _tSensorName = string.Empty;
            _kTypeName = string.Empty;
            _ladomatChannel = 0xFF;
            _ladomatTemp = 62.0f;
            _ladomatIsManual = false;
            _ladomatStatus = Status.OFF;
            _ladomatTriggerName = string.Empty;
            _exhaustFanChannel = 0xFF;
            _exhaustFanStatus = Status.OFF;
            _exhaustFanIsManual = false;
            _workingTemp = 75.0f;
            _histeresis = 3.0f;
            _wtrBName = string.Empty;
            _status = WoodBoilerStatus.UNKNOWN;
            _tempStatus = WoodBoilerTempStatus.Stable;
            _currentTemp = -127.0f;
            _xElement = null;
        }

        public WoodBoiler(IWoodBoilerDataContract dataContract)
        {
            _cfgPos = dataContract.ConfigPos;
            _enabled = dataContract.Enabled;
            _name = dataContract.Name;
            _tSensorName = dataContract.TSensorName;
            _kTypeName = dataContract.KTypeName;
            _ladomatChannel = dataContract.LadomChannel;
            _ladomatTemp = dataContract.LadomatTemp;
            _ladomatIsManual = dataContract.LadomatIsManual;
            _ladomatStatus = dataContract.LadomStatus;
            _ladomatTriggerName = dataContract.LadomatTriggerName;
            _exhaustFanChannel = dataContract.ExhaustFanChannel;
            _exhaustFanStatus = dataContract.ExhaustFanStatus;
            _exhaustFanIsManual = dataContract.ExhaustFanIsManual;
            _workingTemp = dataContract.WorkingTemp;
            _histeresis = dataContract.Histeresis;
            _wtrBName = dataContract.WaterBoilerName;
            _status = dataContract.WBStatus;
            _tempStatus = dataContract.TempStatus;
            _currentTemp = dataContract.CurrentTemp;
        }

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

        private void SetTSensorName(string value)
        {
            _tSensorName = value;
            _xElement?.SetAttributeValue(XMLAttrTempSensorName, _tSensorName);
        }

        private void SetKTypeName(string value)
        {
            _kTypeName = value;
            _xElement?.SetAttributeValue(XMLAttrKTypeName, _kTypeName);
        }

        private void SetLadomatChannel(int value)
        {
            _ladomatChannel = value;
            _xElement?.SetAttributeValue(XMLAttrLadomatChannel, _ladomatChannel);
        }

        private void SetLadomatStatus(Status value)
        {
            if (_ladomatStatus != value)
            {
                _ladomatStatus = value;
                if (_ladomatStatus == Status.UNKNOWN) throw new NotSupportedException($"Ladomat Status value cannot be set to '{value}'");
                
                _xElement?.SetAttributeValue(XMLAttrLadomatStatus, _ladomatStatus.ToString());
                OnPropertyChanged(nameof(LadomStatus));
            }
        }

        private Status GetLadomatStatus()
        {
            if (_ladomatIsManual)
            { return Status.MANUAL; }
            return _ladomatStatus;
        }

        private void SetLadomatIsManual(bool value)
        {
            if (_ladomatIsManual != value)
            {
                _ladomatIsManual = value;
                _xElement?.SetAttributeValue(XMLAttrLadomatIsManual, _ladomatIsManual);
                OnPropertyChanged(nameof(LadomStatus));
            }
        }

        private void SetExhaustFanChannel(int value)
        {
            _exhaustFanChannel = value;
            _xElement?.SetAttributeValue(XMLAttrExhaustFanChannel, _exhaustFanChannel);
        }

        private void SetExhaustFanStatus(Status value)
        {
            _exhaustFanStatus = value;
            if (_exhaustFanStatus == Status.UNKNOWN) throw new NotSupportedException("Something wrong: ExhaustFanStatus: UNKNOWN");

            _xElement?.SetAttributeValue(XMLAttrExhaustFanStatus, _exhaustFanStatus.ToString());
            OnPropertyChanged(nameof(ExhaustFanStatus));
        }

        private Status GetExhaustFanStatus()
        {
            if (_exhaustFanIsManual)
                return Status.MANUAL;
            return _exhaustFanStatus;
        }

        private void SetExhaustFanIsManual(bool value)
        {
            if (_exhaustFanIsManual != value)
            {
                _exhaustFanIsManual = value;
                _xElement?.SetAttributeValue(XMLAttrExhaustFanIsManual, _exhaustFanIsManual);
                OnPropertyChanged(nameof(ExhaustFanStatus));
            }
        }

        private void SetWorkingTemp(double value)
        {
            _workingTemp = value;
            _xElement?.SetAttributeValue(XMLAttrWorkingTemp, _workingTemp);
        }

        private void SetHisteresis(double value)
        {
            _histeresis = value;
            _xElement?.SetAttributeValue(XMLAttrHisteresis, _histeresis);
        }

        private void SetWBStatus(WoodBoilerStatus value)
        {
            if (_status != value)
            {
                _status = value;
                if (_status == WoodBoilerStatus.UNKNOWN) throw new NotSupportedException("Something wrong: WoodBoilerStatus: UNKNOWN");
                
                _xElement?.SetAttributeValue(XMLAttrWoodBoilerStatus, _status.ToString());
                OnPropertyChanged(nameof(WBStatus));
            }
        }

        private void SetCurrentTemp(double value)
        {
            if (_currentTemp != value)
            {
                _currentTemp = value;
                _xElement?.SetAttributeValue(XMLAttrCurrentTemp, _currentTemp);
                OnPropertyChanged(nameof(CurrentTemp));
            }
        }

        private void SetTempStatus(WoodBoilerTempStatus value)
        {
            if (_tempStatus != value)
            {
                _tempStatus = value;
                _xElement?.SetAttributeValue(XMLAttrWoodBoilerTempStatus, _tempStatus);
                OnPropertyChanged(nameof(TempStatus));
            }
        }

        private void SetLadomatTemp(double value)
        {
            _ladomatTemp = value;
            _xElement?.SetAttributeValue(XMLAttrLadomatTemp, _ladomatTemp);
        }

        private void SetLadomatTriggerName(string value)
        {
            _ladomatTriggerName = value;
            _xElement?.SetAttributeValue(XMLAttrLadomatTriggerName, _ladomatTriggerName);
        }

        private void SetWaterBoilerName(string value)
        {
            _wtrBName = value;
            _xElement?.SetAttributeValue(XMLAttrWaterBoilerName, _wtrBName);
        }

        public void SwitchState(SwitchTarget target)
        {
            SwitchStateChanged?.Invoke(this, new SwitchStateChangedEventArgs(target));
        }

        public void OnAction(NSUAction action)
        {
            Action?.Invoke(this, new NSUActionEventArgs(action));
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
            _xElement = new XElement("WoodBoiler");
            _xElement.Add(new XAttribute(XMLAttrConfigPos, _cfgPos));
            _xElement.Add(new XAttribute(XMLAttrEnabled, _enabled));
            _xElement.Add(new XAttribute(XMLAttrName, _name));
            _xElement.Add(new XAttribute(XMLAttrTempSensorName, _tSensorName));
            _xElement.Add(new XAttribute(XMLAttrKTypeName, _kTypeName));
            _xElement.Add(new XAttribute(XMLAttrLadomatChannel, _ladomatChannel));
            _xElement.Add(new XAttribute(XMLAttrLadomatTemp, _ladomatTemp));
            _xElement.Add(new XAttribute(XMLAttrLadomatStatus, _ladomatStatus));
            _xElement.Add(new XAttribute(XMLAttrLadomatIsManual, _ladomatIsManual));
            _xElement.Add(new XAttribute(XMLAttrLadomatTriggerName, _ladomatTriggerName));
            _xElement.Add(new XAttribute(XMLAttrExhaustFanChannel, _exhaustFanChannel));
            _xElement.Add(new XAttribute(XMLAttrExhaustFanStatus, _exhaustFanStatus));
            _xElement.Add(new XAttribute(XMLAttrExhaustFanIsManual, _exhaustFanIsManual));
            _xElement.Add(new XAttribute(XMLAttrWorkingTemp, _workingTemp));
            _xElement.Add(new XAttribute(XMLAttrHisteresis, _histeresis));
            _xElement.Add(new XAttribute(XMLAttrWoodBoilerStatus, _status));
            _xElement.Add(new XAttribute(XMLAttrWoodBoilerTempStatus, _tempStatus));
            _xElement.Add(new XAttribute(XMLAttrCurrentTemp, _currentTemp));
            xml.Add(_xElement);
        }

        public override void ReadXMLNode(XElement xml)
        {
            _xElement = xml ?? throw new ArgumentNullException();

            _cfgPos = ((byte?)(int?)_xElement.Attribute(XMLAttrConfigPos)).GetValueOrDefault(0xFF);
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            _name = (string)_xElement.Attribute(XMLAttrName) ?? string.Empty;
            _tSensorName = (string)_xElement.Attribute(XMLAttrTempSensorName) ?? string.Empty;
            _kTypeName = (string)_xElement.Attribute(XMLAttrKTypeName) ?? string.Empty;
            _ladomatChannel = ((int?)_xElement.Attribute(XMLAttrLadomatChannel)).GetValueOrDefault(0xFF);
            _ladomatTemp = ((double?)_xElement.Attribute(XMLAttrLadomatTemp)).GetValueOrDefault(0);
            _ladomatStatus = Utils.GetStatusFromString(_xElement.Attribute(XMLAttrLadomatStatus)?.Value, Status.OFF);
            _ladomatIsManual = ((bool?)_xElement.Attribute(XMLAttrLadomatIsManual)).GetValueOrDefault(false);
            _ladomatTriggerName = (string)_xElement.Attribute(XMLAttrLadomatTriggerName) ?? string.Empty;
            _exhaustFanChannel = ((int?)_xElement.Attribute(XMLAttrExhaustFanChannel)).GetValueOrDefault(0xFF);
            _exhaustFanStatus = Utils.GetStatusFromString(_xElement.Attribute(XMLAttrExhaustFanStatus)?.Value, Status.OFF);
            _exhaustFanIsManual = ((bool?)_xElement.Attribute(XMLAttrExhaustFanIsManual)).GetValueOrDefault(false);
            _workingTemp = ((double?)_xElement.Attribute(XMLAttrWorkingTemp)).GetValueOrDefault(0);
            _histeresis = ((double?)_xElement.Attribute(XMLAttrHisteresis)).GetValueOrDefault(0);
            _status = Utils.GetStatusFromString(_xElement.Attribute(XMLAttrWoodBoilerStatus)?.Value, WoodBoilerStatus.UNKNOWN);
            _tempStatus = Utils.GetStatusFromString(_xElement.Attribute(XMLAttrWoodBoilerTempStatus)?.Value, WoodBoilerTempStatus.Stable);
            _currentTemp = ((double?)_xElement.Attribute(XMLAttrCurrentTemp)).GetValueOrDefault(0);
        }
    }
}
