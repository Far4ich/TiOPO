internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            TriangleCheck(args);
        }
        catch
        {
            Console.WriteLine("неизвестная ошибка");
        }

        static void TriangleCheck(string[] args)
        {
            if (args.Length != 3)
            {
                throw new Exception();
            }

            double a = Convert.ToDouble(args[0]);
            double b = Convert.ToDouble(args[1]);
            double c = Convert.ToDouble(args[2]);

            if (ValidTriangle(a, b, c))
            {
                switch (CountEquils(a, b, c))
                {
                    case 0:
                        Console.WriteLine("обычный");
                        break;
                    case 1:
                        Console.WriteLine("равнобедренный");
                        break;
                    case 3:
                        Console.WriteLine("равносторонний");
                        break;
                }
            }
        }
    }

    static int CountEquils(double a, double b, double c)
    {
        int i = 0;
        if (a == b)
        {
            i++;
        }
        if (a == c)
        {
            i++;
        }
        if (b == c)
        {
            i++;
        }
        return i;
    }

    private static bool ValidTriangle(double a, double b, double c)
    {
        if (a + b <= c || a + c <= b || c + b <= a)
        {
            Console.WriteLine("не треугольник");
            return false;
        }
        return true;
    }
}