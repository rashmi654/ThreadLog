using Logger.Interfaces;

namespace Logger
{
    public class Program
    {
        static void Main(string[] args)
        {
            int numThreads = 10;
             List<IWriterThread> writerThreads = new List<IWriterThread>();

            IFileWriter fileWriter = new FileWriter();
            fileWriter.Initialize("/log/out.text");

            for (int i = 0; i < numThreads; i++)
            {

                IWriterThread wThread = new WriterThread(fileWriter);
                wThread.Run();
                writerThreads.Add(wThread);
            }

            foreach (IWriterThread thread in writerThreads)
            {
                thread.Wait();
                if (thread.Exception != null)
                {
                    Console.WriteLine($"Thread failed with error:{thread.Exception.Message} ");
                }
            }
            
            Console.Write("Press any key to continue...");
            Console.Read();

        }
    }
}
