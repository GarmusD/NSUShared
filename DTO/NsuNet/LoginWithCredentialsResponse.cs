using Newtonsoft.Json;
using NSU.Shared;
using System;

namespace NSUShared.DTO.NsuNet
{
#nullable enable
    public struct LoginWithCredentialsResponse
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }

        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }

        [JsonProperty(JKeys.Generic.Result)]
        public string Result { get; set; }

        [JsonProperty(JKeys.ActionLogin.DeviceID)]
        public string DeviceId { get; set; }

        [JsonProperty(JKeys.ActionLogin.Hash)]
        public string Token { get; set; }

        [JsonProperty(JKeys.ActionLogin.NeedChangePassword)]
        public bool NeedChangePassword { get; set; }

        [JsonProperty(JKeys.Generic.CommandID)]
        public string? CommandId { get; set; }

        public LoginWithCredentialsResponse(string token, string? commandId)
        {
            Target = JKeys.Syscmd.TargetName;
            Action = JKeys.SystemAction.Login;
            Result = JKeys.Result.Ok;
            DeviceId = Guid.NewGuid().ToString().Replace("{", string.Empty).Replace("}", string.Empty);
            NeedChangePassword = false;
            Token = token;
            CommandId = commandId;
        }
    }
}
