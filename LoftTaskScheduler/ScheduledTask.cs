using System;

namespace LoftTaskScheduler
{
    public class ScheduledTask
    {
        private DateTime expiry;
        private int interval;
        private bool loop;
        private ITaskHandler callback;
        private Task task;
        public ScheduledTask(Task task, int interval, bool loop, ITaskHandler callback)
        {
            this.task = task;
            this.interval = interval;
            this.expiry = DateTime.UtcNow.AddSeconds(interval);
            this.callback = callback;
            this.loop = loop;
        }
        public int GetInterval() => this.interval;
        public Task GetTask() => this.task;
        public ITaskHandler GetCallback() => this.callback;
        public DateTime GetExpiry() => this.expiry;
        public void SetExpiry(DateTime expiry) => this.expiry = expiry;
        public bool IsLooping() => this.loop;
    }
}
