namespace ScrumBoardLib
{
    public class Column
    {
        private string name;
        private readonly List<Task> _tasks;
        public Column(string name)
        {
            this.name = name;
            _tasks = new List<Task>();
        }

        public string GetName()
        {
            return name;
        }
        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }
        public Task RemoveTask(string title)
        {
            Task result;
            bool taskIsFound = false;
            foreach (Task task in _tasks)
            {
                if (task.Title == title)
                {
                    taskIsFound = true;
                    result = task;
                    _tasks.Remove(task);
                    return result;
                }
            }
            if (!taskIsFound)
            {
                throw new Exception("Task is not found");
            }
            return null!;
        }
        public List<Task> GetTasks()
        {
            return _tasks;
        }
    }
}
