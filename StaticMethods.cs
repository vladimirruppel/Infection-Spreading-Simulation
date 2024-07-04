namespace SummerPractice
{
    public class StaticMethods
    {
        public static double GetRandomDouble(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static int GetRandomInt(int minimum, int maximum)
        {   Random random = new Random();
            return random.Next(minimum, maximum);
        }
    }
}
