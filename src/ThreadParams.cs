using Logger.Interfaces;

namespace Logger
{
    /// <summary>
    /// This class is used to pass the parameters to a thread
    /// Used in WriterThread class
    /// </summary>
    public class ThreadParams
    {
        public IFileWriter FileWriter { get; set; }
        public Exception? Exception { get; set; }    
        public int ThreadId { get; set; }
        public ThreadParams(IFileWriter fileWriter) { FileWriter = fileWriter; }
    }
}
