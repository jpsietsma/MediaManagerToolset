using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace RemoteSortSyncService
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create watcher that listens for new files
            FileSystemWatcher RemoteSortWatcher = new FileSystemWatcher();
                RemoteSortWatcher.Path = ConfigurationManager.AppSettings["RemoteSortDirectory"];
                RemoteSortWatcher.Created += RemoteSortWatcher_NewSortFileCreated;
                RemoteSortWatcher.Filter = "*.*";
                RemoteSortWatcher.EnableRaisingEvents = true;

            //Create and start new thread for timer to allow program to wait for incoming files
            Thread timerThread = new Thread(new ThreadStart(ExecuteWorkerThread));                 
                       
            //Start the thread
            timerThread.Start();

        }

        //Timer thread to keep service running
        public static void ExecuteWorkerThread()
        {
            while (true)
            {
                Thread.Sleep(3000);
            }
        }

        private static void RemoteSortWatcher_NewSortFileCreated(object sender, FileSystemEventArgs e)
        {
            var newFile = e.FullPath;
            var newFileDestinationPath = ConfigurationManager.AppSettings["SortDirectory"] + e.Name;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("New file Detected: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(newFile);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Destination Path: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(newFileDestinationPath);
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();

            try
            {
                File.Move(newFile, newFileDestinationPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
