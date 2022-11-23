namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct BinUploadDataContent
    {
        public int Progress { get; }
        public string Content { get; }

        public BinUploadDataContent(int progress, string content)
        {
            Progress = progress;
            Content = content;
        }
    }
}
