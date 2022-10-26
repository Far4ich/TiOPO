namespace ScrumBoardLib
{
    public enum Priority { low, medium, high }
    public class ScrumBoard : IScrumBoard
    {
        private readonly string _name = "Scrum-Board";

        private const int COLUMNS_MAX_COUNT = 10;

        private readonly List<Column> _сolumns = new List<Column>();
        public ScrumBoard(string name)
        {
            if (name == null)
            {
                throw new Exception("Name can't be null");
            }

            _name = name;
        }
        public ScrumBoard() {}

        public string GetName()
        {
            return _name;   
        }
        public Column GetColumn(int id)
        {
            return _сolumns[id];
        }
        public List<Column> GetColumns()
        {
            return _сolumns;
        }

        public void AddColumn(string name)
        {
            if (_сolumns.Count < COLUMNS_MAX_COUNT - 1)
            {
                Column column = new Column(name);
                _сolumns.Add(column);
            }
            else
            {
                throw new Exception("You don't have space on the board for another column");
            }
        }
        public void DeleteColumn(string name)
        {
            foreach (Column i in _сolumns)
            {
                if (i.GetName() == name)
                {
                    _сolumns.Remove(i);
                    return;
                }
            }
            throw new Exception("Column is not found");
        }
        public void AddTask(string title, string description, Priority priority)
        {
            if (_сolumns.Count == 0)
            {
                throw new Exception("You need at least one column");
            }

            Task task = new Task(title, description, priority);
            _сolumns[0].AddTask(task);
        }

        public void MoveTask(string titleTask, string targetName)
        {
            Task task = null;

            bool columnFound = false;

            foreach (Column column in _сolumns)
            {
                task = column.RemoveTask(titleTask);
                if (task != null)
                {
                    foreach (Column i in _сolumns)
                    {
                        if (i.GetName() == targetName)
                        {
                            i.AddTask(task);
                            columnFound = true;
                            break;
                        }
                    }
                    if (!columnFound)
                    {
                        throw new Exception("Column not found");
                    }
                    break;
                }
            }
            
            if (task == null)
            { 
                throw new Exception("Task not found");
            }
        }

        public void DeleteTask(string titleTask)
        {
            bool taskFound = false;
            foreach (Column column in _сolumns)
            {
                try
                {
                    column.RemoveTask(titleTask);
                    taskFound = true;
                    break;
                }
                catch{}
            }
            if (!taskFound)
            {
                throw new Exception("Task is not found");
            }
        }

        public void PrintScrumBoard()
        {
            Console.WriteLine(_name);
            foreach (Column column in _сolumns)
            {
                Console.WriteLine("  " + column.GetName());
                foreach (Task task in column.GetTasks())
                {
                    Console.WriteLine("    " + task.Title);
                    Console.WriteLine("     " + task.Priority);
                    Console.WriteLine("     " + task.Description);
                }
            }
        }
    }
}