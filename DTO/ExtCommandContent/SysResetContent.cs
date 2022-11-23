namespace NSU.Shared.DTO.ExtCommandContent
{
    public enum ResetType
    {
        Soft,
        Hard,
        Zero
    }

    public struct SysResetContent
    {
        public ResetType ResetType { get; }

        public SysResetContent(ResetType resetType)
        {
            ResetType = resetType;
        }
    }
}
