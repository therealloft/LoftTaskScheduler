using LoftTaskScheduler;
using System;
using System.IO;

namespace LoftTaskSchedulerConsoleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a new scheduler
            Scheduler scheduler = new Scheduler();
            //Create the task
            Task copyDirectoryTask = new Task("Copy Directory");
            //Create the handler for the task
            CopyDirectoryTaskHandler copyDirectoryTaskHandler = new CopyDirectoryTaskHandler();
            //Add the task to the scheduler
            scheduler.AddScheduledTask(copyDirectoryTask, TimeSpan.FromDays(1), true, copyDirectoryTaskHandler);
            //Start the scheduler service
            scheduler.StartService();
            Console.ReadLine();
        }
    }
    public class CopyDirectoryTaskHandler : ITaskHandler
    {
        public void DoTask(Task task)
        {
            DirectoryInfo source = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)));
            DirectoryInfo target = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "My Documents Backup"));
            CopyFilesRecursively(source, target);
        }
        private void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo directory in source.GetDirectories())
                CopyFilesRecursively(directory, target.CreateSubdirectory(directory.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }
    }
}
