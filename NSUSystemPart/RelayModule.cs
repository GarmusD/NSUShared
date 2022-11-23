using NSU.Shared.DataContracts;
using NSUWatcher.Interfaces.MCUCommands.From;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.NSUSystemPart
{
    public class RelayModule : NSUPartBase, IRelayModuleDataContract
    {
        #region Constants
        private const string XMLAttrConfigPos = "cfgpos";
        private const string XMLAttrEnabled = "enabled";
        private const string XMLAttrActiveLow = "activelow";
        private const string XMLAttrInverted = "inverted";
        private const string XMLAttrFlags = "flags";
        #endregion

        #region Properties
        public byte ConfigPos { get => _cfgPos; set => SetConfigPos(value); }
        public bool Enabled { get => _enabled; set => SetEnabled(value); }
        public bool ActiveLow { get => _activeLow; set => SetActiveLow(value); }
        public bool ReversedOrder { get => _reversed; set => SetReversed(value); }
        public byte StatusFlags { get => _flags; set => throw new InvalidOperationException("Flags cannot be set directly. Use SetStatus() method instead."); }
        public byte LockFlags { get => _flags; set => throw new InvalidOperationException("Flags cannot be set directly. Use SetStatus() method instead."); }
        #endregion

        #region Private fields
        private bool _enabled;
        private byte _cfgPos;
        private bool _activeLow;
        private bool _reversed;
        private byte _flags;
        private byte _lockFlags;
        XElement? _xElement = null;
        #endregion
        
        
        public RelayModule()
        {
            _cfgPos = INVALID_VALUE;
            _enabled = false;
            _activeLow = false;
            _reversed = false;
            _flags = 0;
            _lockFlags = 0;
            _xElement = null;
        }

        public RelayModule(IRelayModuleDataContract dataContract)
        {
            _cfgPos = dataContract.ConfigPos;
            _enabled = dataContract.Enabled;
            _activeLow = dataContract.ActiveLow;
            _reversed = dataContract.ReversedOrder;
            _flags = dataContract.StatusFlags;
            _lockFlags = dataContract.LockFlags;
            _xElement = null;
        }

        #region Private methods
        private void SetConfigPos(byte value)
        {
            _cfgPos = value;
            _xElement?.SetAttributeValue(XMLAttrConfigPos, _cfgPos);
        }

        private void SetEnabled(bool value)
        {
            if (_enabled != value)
            {
                _enabled = value;
                _xElement?.SetAttributeValue(XMLAttrEnabled, _enabled);
            }
        }

        private void SetActiveLow(bool value)
        {
            _activeLow = value;
            _xElement?.SetAttributeValue(XMLAttrActiveLow, _activeLow);
        }

        private void SetReversed(bool value)
        {
            _reversed = value;
            _xElement?.SetAttributeValue(XMLAttrInverted, _reversed);
        }

        private void SetFlags(byte value)
        {
            _flags = value;
            _xElement?.SetAttributeValue(XMLAttrFlags, _flags);
        }

        private bool FlagIsSet(byte idx)
        {
            return (_flags & (1 << idx)) != 0;
        }

        #endregion

        #region Public methods
        public void SetStatus(IRelayModuleStatus status)
        {
            if(_flags != status.StatusFlags || _lockFlags != status.LockFlags)
            {
                _flags = status.StatusFlags;
                _lockFlags = status.LockFlags;
                OnPropertyChanged(nameof(StatusFlags));
            }
        }

        public void SetFlag(byte idx)
        {
            if (!FlagIsSet(idx))
            {
                SetFlags((byte)(_flags | (1 << idx)));

            }
        }

        public void ClearFlag(byte idx)
        {
            if (FlagIsSet(idx))
            {
                SetFlags((byte)(_flags ^ (1 << idx)));
            }
        }

        override public void AttachXMLNode(XElement xml)
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml), "XElement cannot be null.");
            
            if (_xElement == null)
                _xElement = xml.Elements().FirstOrDefault(item => item.Attribute(XMLAttrConfigPos)?.Value == _cfgPos.ToString());
            
            if (_xElement != null)
                ReadXMLNode(_xElement);
            else
                CreateNodeDefaults(xml);
        }

        private void CreateNodeDefaults(XElement xml)
        {
            _xElement = new XElement("RelayModule");
            _xElement.Add(new XAttribute(XMLAttrConfigPos, _cfgPos));
            _xElement.Add(new XAttribute(XMLAttrEnabled, _enabled));
            _xElement.Add(new XAttribute(XMLAttrActiveLow, _activeLow));
            _xElement.Add(new XAttribute(XMLAttrInverted, _reversed));
            _xElement.Add(new XAttribute(XMLAttrFlags, _flags));
            xml.Add(_xElement);
        }

        override public void ReadXMLNode(XElement xml)
        {
            _xElement = xml;
            _cfgPos = (byte)((int?)_xElement.Attribute(XMLAttrConfigPos)).GetValueOrDefault(INVALID_VALUE);
            _enabled = ((bool?)_xElement.Attribute(XMLAttrEnabled)).GetValueOrDefault(false);
            _activeLow = ((bool?)_xElement.Attribute(XMLAttrActiveLow)).GetValueOrDefault(false);
            _reversed = ((bool?)_xElement.Attribute(XMLAttrInverted)).GetValueOrDefault(false);
            _flags = ((byte?)(int?)_xElement.Attribute(XMLAttrFlags)).GetValueOrDefault(0);
        }
        #endregion
    }
}
