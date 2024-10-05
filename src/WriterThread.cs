using Logger.Interfaces;

namespace Logger
{
    /// <summary>
    /// Creates new thread  
    /// </summary>
    public class WriterThread : IWriterThread
    {
        private Thread _thread;
        private ThreadParams _params;
        private bool _threadStarted;

        public WriterThread(IFileWriter fileWriter)
        {
            _params = new ThreadParams(fileWriter);
            _thread = new Thread((obj) =>
            {
                ThreadParams? tp = obj as ThreadParams;
                if (tp == null)
                {
                    throw new Exception("No thread parameters passed");
                }
                tp.ThreadId = Thread.CurrentThread.ManagedThreadId;
                try
                {
                    for (int j = 0; j < 10; j++)
                    {
                        tp.FileWriter.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    tp.Exception = ex;
                }
            });
            _threadStarted = false;
        }

        /// <summary>
        /// Starts the thread execution
        /// </summary>
        /// <exception cref="Exception">No thread parameters passed</exception>
        public void Run()
        {
            _thread.Start(_params);
            _threadStarted = true;
        }

        /// <summary>
        /// Exception caught during thread execution, if any or null
        /// </summary>
        public Exception? Exception {  get { return _params.Exception; } }

        /// <summary>
        /// Waits for the thread to run to completion
        /// </summary>
        /// <exception cref="Exception">Thread not started.</exception>
        public void Wait()
        {
            if (_threadStarted == false)
                throw new Exception("Thread not started.");
            _thread.Join();
        }
    }
}
