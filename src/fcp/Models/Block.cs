using System;
using System.IO;

namespace fcp.Models
{
    public class Block
    {
        public int CurrentBlock { get; set; }
        public int TotalBlocks { get; set; }
        public string Message { get; set; }
        public string FileData { get; set; }
        public Status BlockStatus { get; set; }
    }
    public enum Status
    {
        Started,
        Fail,
        Completed
    }
}