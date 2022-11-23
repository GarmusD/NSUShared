using NSU.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DTO.NsuNet
{
    public struct KTypeTempChanged
    {
        public string Target { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public int Temperature { get; set; }

        public static KTypeTempChanged Create(string name, int temperature)
        {
            return new KTypeTempChanged() 
            {
                Target = JKeys.KType.TargetName,
                Action = JKeys.Action.Info,
                Name = name,
                Temperature = temperature
            };
        }
    }
}
