using NSU.Shared.DataContracts;
using NSU.Shared.NSUSystemPart;
using System;
using System.Linq;

namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct TempTriggerSetupContent
    {
        public byte ConfigPos { get; }
        public bool Enabled { get; }
        public string Name { get; }
        public TriggerPiece[] TriggerPieces { get; }

        public TempTriggerSetupContent(byte configPos, bool enabled, string name, params TriggerPiece[] triggerPieces)
        {
            if (triggerPieces.Length > ITempTriggerDataContract.MAX_TEMPTRIGGERPIECES)
                throw new ArgumentOutOfRangeException(nameof(triggerPieces), "Contains too much TempTriggerPiece's.");
            ConfigPos = configPos;
            Enabled = enabled;
            Name = name;
            TriggerPieces = Enumerable.Range(0, ITempTriggerDataContract.MAX_TEMPTRIGGERPIECES).Select(_ => new TriggerPiece()).ToArray();
            int idx = 0;
            foreach (var item in triggerPieces)
            {
                TriggerPieces[idx++] = item;
            }
        }
    }

    public struct TriggerPiece
    {
        public bool Enabled { get; }
        public string TempSensorName { get; }
        public TriggerCondition Condition { get; }
        public double Temperature { get; }
        public double Histeresis { get; }

        public TriggerPiece(bool enabled, string tempSensorName, TriggerCondition condition, double temperature, double histeresis)
        {
            Enabled = enabled;
            TempSensorName = tempSensorName;
            Condition = condition;
            Temperature = temperature;
            Histeresis = histeresis;
        }
    }
}
