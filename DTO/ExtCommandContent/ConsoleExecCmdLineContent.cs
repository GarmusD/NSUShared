namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct ConsoleExecCmdLineContent
    {
        public string CmdLine { get; }

        public ConsoleExecCmdLineContent(string cmdLine)
        {
            CmdLine = cmdLine;
        }
    }
}
