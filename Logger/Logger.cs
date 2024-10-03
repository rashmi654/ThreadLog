using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logger
{
    public class Logger
    {
        private string folder = "/log";
        private string fileName = "/log/out.text";
        Queue<string> queue;
        private static readonly object locker = new object();
        private int threadCount = 10;
        private int logCount = 10;
        private int lineNumber = 0;
        public Logger()
        {
            queue = new Queue<string>();
        }

        public  void StartLogger()
        {
            
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                Console.WriteLine("directory created");
            }

            File.WriteAllText(fileName, $"0, 0, {DateTime.Now.ToString("HH:MM:ss:fff")}\n");
            

            for (int i = 0; i < threadCount; i++)
            {
                Thread thread = new Thread(() => { dosomework(); });
                thread.Start();
            }

          
            WriteToFile();
            Console.ReadKey();
        }

        private void dosomework()
        {
           
            for (int j = 0; j < logCount; j++)
            {
                lock (locker)
                {
                    lineNumber++;
                    String date = DateTime.Now.ToString("HH:MM:ss:fff");
                    int threadid = Thread.CurrentThread.ManagedThreadId;
                    queue.Enqueue($"{lineNumber}, {threadid}, {date}");
                    
                  
                }
            }
            
           
            
        }

        private void WriteToFile()
        {
            using (StreamWriter sw = File.AppendText(fileName))
            {
               
                while (queue.TryDequeue(out string logLine))
                {
                    sw.WriteLine(logLine);
                }
            }
        }
        private async void WriteToConsole()
        {
            while (queue.TryDequeue(out string logLine))
            {
                Console.WriteLine(logLine);

            }
        }


        /*    public async void StartLogger()
            {
                Queue<string> queue = new Queue<string>();

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                    Console.WriteLine("directory created");
                }

                for (int i = 1; i <= 10; i++)
                {
                    new Thread(() => queue.Enqueue("test from thread " + i)).Start();
                }


                using (StreamWriter sw = File.AppendText(fileName))
                {
                    while (queue.TryDequeue(out string logLine))
                    {
                        Console.WriteLine(logLine);
                        await sw.WriteLineAsync(logLine);
                    }





                }
            }*/




    }
}
