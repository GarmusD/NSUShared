using System;
using System.Runtime.CompilerServices;
using NSU.Shared.NSUUtils;
using System.Xml.Linq;
using System.Linq;
using NSU.Shared.DataContracts;

namespace NSU.Shared.NSUSystemPart
{
    public enum TriggerCondition
    {
        TrueIfLower,
        TrueIfHigher
    };

    public class TempTrigger : NSUPartBase, ITempTriggerDataContract
    {
        public const int MaxTempTriggerPieces = 4;

        private const string XMLAttrConfigPos = "cfgpos";
        private const string XMLAttrEnabled = "enabled";
        private const string XMLAttrName = "name";
        private const string XMLAttrStatus = "status";


        #region Properties
        public byte ConfigPos { get => _cfgPos; set => SetConfigPos(value); }
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public string Name { get => _name; set => SetName(value); }
        public Status Status { get => _status; set => SetStatus(value); }
        public ITempTriggerPieceDataContract[] TempTriggerPieces { get => _triggerPieces; set => throw new InvalidOperationException(); }

        [IndexerName("TempTriggerPiece")]
        public ITempTriggerPieceDataContract this[int index] => _triggerPieces[index];
        #endregion

        #region Private fields
        private byte _cfgPos;
        private bool _enabled;
        private string _name;
        private Status _status;
        private readonly TempTriggerPiece[] _triggerPieces;
        private XElement _xElement;
        #endregion
        
        public TempTrigger()
        {
            _cfgPos = INVALID_VALUE ;
            _enabled = false;
            _name = string.Empty;
            _status = Status.UNKNOWN;
            _triggerPieces = Enumerable
                                .Range(0, MaxTempTriggerPieces)
                                .Select(i => new TempTriggerPiece((byte)i))
                                .ToArray();
            _xElement = null;
        }

        public TempTrigger(ITempTriggerDataContract dataContract)
        {
            _cfgPos = dataContract.ConfigPos;
            _enabled = dataContract.Enabled;
            _name = dataContract.Name;
            _status = dataContract.Status;
            _triggerPieces = Enumerable
                                .Range(0, MaxTempTriggerPieces)
                                .Select(i => new TempTriggerPiece((byte)i) 
                                {
                                    Enabled = dataContract.TempTriggerPieces[i].Enabled,
                                    TSensorName = dataContract.TempTriggerPieces[i].TSensorName,
                                    Condition = dataContract.TempTriggerPieces[i].Condition,
                                    Temperature = dataContract.TempTriggerPieces[i].Temperature,
                                    Histeresis = dataContract.TempTriggerPieces[i].Histeresis,
                                })
                                .ToArray();
            _xElement = null;
        }

        #region Private methods
        /*************************************************************************
         * PRIVATE
         * ***********************************************************************/
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

        private void SetStatus(Status value)
        {
            if (_status != value)
            {
                _status = value;
                _xElement?.SetAttributeValue(XMLAttrStatus, _status);
                OnPropertyChanged(nameof(Status));
            }
        }

        #endregion

        #region Public methods
        /*************************************************************************
         * PUBLIC
         * ***********************************************************************/
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
            _xElement = new XElement("TempTrigger");
            _xElement.Add(new XAttribute(XMLAttrConfigPos, _cfgPos));
            _xElement.Add(new XAttribute(XMLAttrEnabled, _enabled));
            _xElement.Add(new XAttribute(XMLAttrName, _name));
            _xElement.Add(new XAttribute(XMLAttrStatus, _status));
            foreach (var item in _triggerPieces)
                item.AttachXMLNode(_xElement);
            xml.Add(_xElement);
        }

        public override void ReadXMLNode(XElement xml)
        {
            _xElement = xml;
            _cfgPos = ((byte?)(int?)_xElement.Attribute(XMLAttrConfigPos)).GetValueOrDefault(INVALID_VALUE);
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            _name = (string)_xElement.Attribute(XMLAttrName) ?? string.Empty;
            _status = Utils.GetStatusFromString(_xElement.Attribute(XMLAttrStatus)?.Value, Status.UNKNOWN);
            foreach (var item in _triggerPieces)
                item.AttachXMLNode(_xElement);
        }
        #endregion
    }
}
