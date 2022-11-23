using Newtonsoft.Json;

namespace NSU.Shared.DTO.NsuNet
{
    public struct SystemFanPwmChanged
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }
        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }
        [JsonProperty(JKeys.Generic.Name)]
        public string Name { get; set; }
        [JsonProperty(JKeys.SystemFan.CurrentPWM)]
        public int CurrentPWM { get; set; }

        public static SystemFanPwmChanged Create(string name, int pwm)
        {
            return new SystemFanPwmChanged()
            {
                Target = JKeys.SystemFan.TargetName,
                Action = JKeys.Action.Info,
                Name = name,
                CurrentPWM = pwm
            };
        }
    }
}
