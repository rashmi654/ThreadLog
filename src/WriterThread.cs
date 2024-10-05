using Logger.Interfaces;

namespace Logger
{
    /// <summary>
    /// Creates new thread  
    /// </summary>
    public class WriterThread : IWriterThread
    {
        private Thread? _thread;
        private ThreadParams _params;

        public WriterThread(IFileWriter fileWriter)
        {
            _params = new ThreadParams();
            _params.FileWriter = fileWriter;
        }

        /// <summary>
        /// Creates new thread and start the execution
        /// </summary>
        /// <exception cref="Exception">No thread parameters passed</exception>
        public void Run()
        {
            if (_thread != null)
            {
                throw new Exception("Thread already started.");
            }    
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
                        tp.FileWriter?.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    tp.Exception = ex;
                    
                }
            });

            _thread.Start(_params);
        }

        /// <summary>
        /// Exception caught during thread execution
        /// </summary>
        public Exception? Exception {  get { return _params.Exception; } }

        /// <summary>
        /// Waits for all threads to completion
        /// </summary>
        /// <exception cref="Exception">Thread not started.</exception>
        public void Wait()
        {
            if (_thread == null)
                throw new Exception("Thread not started.");
            _thread.Join();
        }
    }
}
