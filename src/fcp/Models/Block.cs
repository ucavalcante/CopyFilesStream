namespace fcp.Models
{
    public class Block
    {
        public int CurrentBlock { get; set; }
        public int TotalBlocks { get; set; }
        public string Message { get; set; }
        public Status BlockStatus { get; set; }
    }
    public enum Status
    {
        Started,
        Fail,
        Completed
    }
}