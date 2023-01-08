using Newtonsoft.Json;
using NSU.Shared;

namespace NSUShared.DTO.NsuNet
{
#nullable enable
    public struct SnapshotResponse
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }

        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }

        [JsonProperty(JKeys.Generic.Result)]
        public string Result { get; set; }

        [JsonProperty(JKeys.Generic.Content)]
        public string Content { get; set; }

        [JsonProperty(JKeys.Generic.Value)]
        public string Value { get; set; }

        [JsonProperty(JKeys.Generic.CommandID)]
        public string? CommandID { get; set; }

        public SnapshotResponse(string snapshot, string? commandId, string content = "xml")
        {
            Target = JKeys.Syscmd.TargetName;
            Action = JKeys.Syscmd.Snapshot;
            Result = JKeys.Result.Ok;
            Content = content;
            Value = snapshot;
            CommandID = commandId;
        }
    }
}
