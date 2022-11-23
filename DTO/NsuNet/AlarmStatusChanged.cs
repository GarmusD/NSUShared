namespace NSU.Shared.DTO.NsuNet
{
    public class AlarmStatusChanged : NsuNetRequestBase
    {
        public AlarmStatusChanged()
        {
            Target = JKeys.Alarm.TargetName;
            Action = JKeys.Action.Info;
        }
    }
}
