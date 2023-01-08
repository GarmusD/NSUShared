namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct WoodBoilerSwitchManualMode
    {
        public string WoodBoilerName { get; }
        public string SwitchTarget { get; }

        public WoodBoilerSwitchManualMode(string wbName, string switchTarget)
        {
            WoodBoilerName = wbName;
            SwitchTarget = switchTarget;
        }
    }
}
