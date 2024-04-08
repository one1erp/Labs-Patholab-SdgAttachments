namespace SdgAttachments
{
    public class Attached
    {
        public long AttachId { get; set; }
        public long? SdgId { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
}