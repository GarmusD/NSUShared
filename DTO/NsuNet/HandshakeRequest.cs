namespace NSU.Shared.DTO.NsuNet
{
    public class HandshakeRequest : NsuNetRequestBase
    {
        public HandshakeRequest()
        {
            Target = JKeys.Syscmd.TargetName;
            Action = JKeys.SystemAction.Handshake;
        }
    }
}
