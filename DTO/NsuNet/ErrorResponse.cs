using Newtonsoft.Json;
using NSU.Shared;

namespace NSUShared.DTO.NsuNet
{
#nullable enable
    public struct ErrorResponse
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }

        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }

        [JsonProperty(JKeys.Generic.Result)]
        public string Result { get; set; }

        [JsonProperty(JKeys.Generic.ErrCode)]
        public string ErrorCode { get; set; }

        [JsonProperty(JKeys.Generic.CommandID)]
        public string? CommandID { get; set; }

        public static ErrorResponse Create(string target, string action, string errCode, string? commandId)
        {
            return new ErrorResponse
            {
                Target = target,
                Action = action,
                Result = "error",
                ErrorCode = errCode,
                CommandID = commandId
            };
        }
    }
}
