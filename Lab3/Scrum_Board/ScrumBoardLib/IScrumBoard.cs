namespace ScrumBoardLib
{
    public interface IScrumBoard
    {
        void AddColumn(string name);
        void AddTask(string title, string description, Priority priority);
        void DeleteColumn(string name);
        void DeleteTask(string titleTask);
        void MoveTask(string titleTask, string targetName);
        void PrintScrumBoard();
    }
}