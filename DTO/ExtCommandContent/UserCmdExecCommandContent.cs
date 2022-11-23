namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct UserCmdExecCommandContent
    {
        public string Command { get; }

        public UserCmdExecCommandContent(string command)
        {
            Command = command;
        }
    }
}
