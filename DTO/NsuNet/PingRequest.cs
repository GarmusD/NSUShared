namespace NSU.Shared.DTO.NsuNet
{
    public class PingRequest : NsuNetRequestBase
    {
        public PingRequest()
        {
            Target = JKeys.Syscmd.TargetName;
            Action = JKeys.SystemAction.Ping;
        }
    }
}
