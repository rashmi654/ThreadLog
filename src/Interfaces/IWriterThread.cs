namespace Logger.Interfaces
{
    public interface IWriterThread
    {
        public void Run();
        public void Wait();
        public Exception? Exception { get; }
    }
}
