using Logger.Interfaces;

namespace Logger
{
    /// <summary>
    /// Creates the log file and writes into the file 
    /// </summary>
    public class FileWriter : IFileWriter
    {
        private string _filePath = String.Empty;
        private object _writeLock = new object();
        private int _lineNumber = 0;
        private bool _initialized = false;
        private Random rand = new Random();

        /// <summary>
        /// Initialize the file, if already exists overwrites. 
        /// If folder doesn't exists, it creates the folder
        /// </summary>
        /// <param name="filePath">filepath to create the file</param>
        /// <exception cref="Exception">If exception occrred during file/folder create: Error during Initialize.</exception>
        public void Initialize(string filePath)
        {
            lock (_writeLock)
            {
                if (_initialized) return;

                try
                {
                    string? directoryName = Path.GetDirectoryName(filePath);
                    if (!String.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    _filePath = filePath;
                    string log = $"0, 0, {DateTime.Now.ToString("HH:mm:ss:fff")} {Environment.NewLine}";
                    File.WriteAllText(_filePath, log);
                    _initialized = true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error during Initialize.", ex);
                }
            }
        }

        /// <summary>
        /// Writes line into the exisiting file
        /// </summary>
        /// <exception cref="Exception">If initializing of the file is not done: Initialize call missing.
        /// If any error during file write: Error during write to file
        /// </exception>
        public void WriteLine()
        {
            //int v = rand.Next(1, 100);
            //if (v < 10)
            //    throw new Exception("Simulating error " + v);
            if (!_initialized)
            {
                throw new Exception("Initialize call missing.");
            }
            lock (_writeLock)
            {
                try
                {
                    _lineNumber++;
                    int threadid = Thread.CurrentThread.ManagedThreadId;
                    string date = DateTime.Now.ToString("HH:mm:ss:fff");
                    string log = $"{_lineNumber}, {threadid}, {date} {Environment.NewLine}";
                    File.AppendAllText(_filePath, log);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error during write to file", ex);
                }
            }

        }
    }
}
