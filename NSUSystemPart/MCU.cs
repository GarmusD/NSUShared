using NSU.Shared.DataContracts;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace NSU.Shared.NSUSystemPart
{
    public class MCU : NSUPartBase, IMCUStatusDataContract
    {
        public MCUStatus Status { get => _mcuStatus; set { SetMcuStatus(value); } }
        public int FreeMem { get => _freeMem; set => SetFreeMem(value); }
        public int UpTime { get => _upTime; set => SetUpTime(value); }
        public bool RebootRequired { get => _rebootRequired; set => SetRebootRequired(value); }

        private MCUStatus _mcuStatus = MCUStatus.Off;
        private int _freeMem = 0;
        private int _upTime = 0;
        private bool _rebootRequired = false;

        public MCU()
        {

        }

        public void SetData(IMCUStatusDataContract data)
        {
            Status = data.Status;
            FreeMem = data.FreeMem;
            UpTime = data.UpTime;
            RebootRequired = data.RebootRequired;
        }

        public override void AttachXMLNode(XElement xml)
        {
            
        }

        public override void ReadXMLNode(XElement xml)
        {
            
        }

        private void SetMcuStatus(MCUStatus value, [CallerMemberName] string property = "")
        {
            if(_mcuStatus != value)
            {
                _mcuStatus = value;
                if(_mcuStatus == MCUStatus.Off)
                {
                    _freeMem = 0;
                    _upTime = 0;
                    _rebootRequired = false;
    }
                OnPropertyChanged(property);
            }
        }

        private void SetFreeMem(int value, [CallerMemberName] string property = "")
        {
            if(_freeMem != value)
            {
                _freeMem = value;
                OnPropertyChanged(property);
            }
        }

        private void SetUpTime(int value, [CallerMemberName] string property = "")
        {
            if(_upTime != value)
            {
                _upTime = value;
                OnPropertyChanged(property);
            }
        }

        private void SetRebootRequired(bool value, [CallerMemberName] string property = "")
        {
            if(_rebootRequired != value)
            {
                _rebootRequired = value;
                OnPropertyChanged(property);
            }
        }
    }
}
