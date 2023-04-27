using System.Collections.Generic;

namespace LoftTaskScheduler
{
    public class Task
    {
        public object id;
        public Dictionary<object, object> parameters;
        public bool active = true;
        public Task()
        {
            parameters = new Dictionary<object, object>();
        }
        public Task(object id)
        {
            this.parameters = new Dictionary<object, object>();
            this.id = id;
        }
        public Task(object id, Dictionary<object, object> parameters)
        {
            this.id = id;
            this.parameters = parameters;
        }

    }
}
