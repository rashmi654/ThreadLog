using Logger.Interfaces;

namespace Logger
{
    /// <summary>
    /// Thread parameters
    /// </summary>
    public class ThreadParams
    {
        public IFileWriter? FileWriter { get; set; }
        public Exception? Exception { get; set; }    
        public int ThreadId { get; set; }
    }
}
