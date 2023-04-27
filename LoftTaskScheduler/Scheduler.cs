using System;
using System.Collections.Generic;
using System.Threading;

namespace LoftTaskScheduler
{
    public class Scheduler
    {
        private Thread thread;
        private List<ScheduledTask> taskList;
        private List<ScheduledTask> addList;
        private bool running = false;
        public Scheduler()
        {
            taskList = new List<ScheduledTask>();
            addList = new List<ScheduledTask>();
        }
        public void StartService() {
            running = true;
            thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }
        public void StopService() {
            running = false;
            thread.Abort();
        }
        private void Run()
        {
            while (running)
            {
                try
                {
                    ExecuteTasks();
                    Thread.Sleep(1000);
                }
                catch(Exception e)
                {

                }
            }
        }
        public void AddScheduledTask(Task task, int interval, bool loop, ITaskHandler callback) {
            addList.Add(new ScheduledTask(task, interval, loop, callback));
        }
        public void AddScheduledTask(Task task, TimeSpan interval, bool loop, ITaskHandler callback)
        {
            addList.Add(new ScheduledTask(task, (int)interval.TotalSeconds, loop, callback));
        }
        public void RemoveScheduledTask(Task task) {
            foreach (ScheduledTask t in taskList)
            {
                if(t.GetTask() == task)
                {
                    taskList.Remove(t);
                    break;
                }
            }
        }
        public bool IsRunning() => this.running;
        private void ExecuteTasks()
        {
            DateTime now = DateTime.UtcNow;
            foreach (ScheduledTask task in taskList)
            {
                if (!task.GetTask().active)
                {
                    taskList.Remove(task);
                    continue;
                }
                if (now >= task.GetExpiry())
                {
                    try
                    {
                        task.GetCallback().DoTask(task.GetTask());
                    }
                    catch (Exception e)
                    {

                    }
                    if (task.IsLooping())
                    {
                        task.SetExpiry(task.GetExpiry().AddSeconds(task.GetInterval()));
                        continue;
                    }
                    taskList.Remove(task);
                }
            }
            if(addList.Count > 0)
            {
                taskList.AddRange(addList);
                addList.Clear();
            }
        }
    }
}
