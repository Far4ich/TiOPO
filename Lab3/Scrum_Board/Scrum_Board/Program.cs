using System;
using ScrumBoardLib;

namespace program
{
    internal class Program
    {
        static void Main()
        {
            ScrumBoardLib.ScrumBoard board = CreateBoard();
            while (true)
            {
                Console.WriteLine("Enter operation number(enter nothing to help):");
                switch (Console.ReadLine())
                {
                    case "0":
                        Console.WriteLine("Exiting:\n");
                        Environment.Exit(1);
                        break;
                    case "1":
                        board.PrintScrumBoard();
                        break;
                    case "2":
                        AddColumn(board);
                        break;
                    case "3":
                        RemoveColumn(board);
                        break;
                    case "4":
                        AddTask(board);
                        break;
                    case "5":
                        RemoveTask(board);
                        break;
                    case "6":
                        MoveTask(board);
                        break;
                    default:
                        PrintHelp();
                        break;
                }
            }
        }

        static Priority ReadPriority()
        {
            int priority;
            Console.WriteLine("Enter priority:\n" +
                "1 for low \n" +
                "2 for medium \n" +
                "3 for high ");
            try
            {
                priority = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ReadPriority();
            }

            switch (priority)
            {
                case 1:
                    return Priority.low;
                case 2:
                    return Priority.medium;
                case 3:
                    return Priority.high;
                default:
                    Console.WriteLine("Incorect priority number");
                    return ReadPriority();
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine("0 for exit\n" +
                "1 for print board \n" +
                "2 for creation column \n" +
                "3 for deleting column \n" +
                "4 for creating task \n" +
                "5 for deleting task \n" +
                "6 for moving task");
        }

        private static ScrumBoardLib.ScrumBoard CreateBoard()
        {
            Console.WriteLine("Enter board name");
            ScrumBoardLib.ScrumBoard board = null;
            string tmpString = TakeNotEmptyString();
            return board = new ScrumBoardLib.ScrumBoard(tmpString);
        }

        private static void MoveTask(ScrumBoardLib.ScrumBoard board)
        {
            Console.WriteLine("Enter task name");
            string tmpTitle = TakeNotEmptyString();
            Console.WriteLine("Enter column name");
            string tmpTargetName = TakeNotEmptyString();
            try
            {
                board.MoveTask(tmpTitle, tmpTargetName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void RemoveTask(ScrumBoardLib.ScrumBoard board)
        {
            Console.WriteLine("Enter task name");
            try
            {
                board.DeleteTask(TakeNotEmptyString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddTask(ScrumBoardLib.ScrumBoard board)
        {
            Console.WriteLine("Enter task name");
            string tmpTitle = TakeNotEmptyString();
            
            Console.WriteLine("Enter task discription");
            string tmpDescription = TakeNotEmptyString();

            try
            {
                board.AddTask(tmpTitle, tmpDescription, ReadPriority());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void RemoveColumn(ScrumBoardLib.ScrumBoard board)
        {
            Console.WriteLine("Enter column name");
            try
            {
                board.DeleteColumn(TakeNotEmptyString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddColumn(ScrumBoardLib.ScrumBoard board)
        {
            Console.WriteLine("Enter column name");
            try
            {
                board.AddColumn(TakeNotEmptyString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string TakeNotEmptyString()
        {
            string tmpString = Console.ReadLine();
            if (tmpString.Length == 0)
            {
                Console.WriteLine("You must enter at least 1 character");
                return TakeNotEmptyString();
            }
            return tmpString;
        }
    }
}

