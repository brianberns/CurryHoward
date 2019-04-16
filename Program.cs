using System;

namespace CurryHoward
{
    class Program
    {
        public static void EitherDemo()
        {
            Either<float, string> Divide(float num, float den)
            {
                if (den == 0.0)
                    return "Illegal division by zero";
                else
                    return num / den;
            }

            string ToString(Either<float, string> either)
            {
                return either.Match(
                    result => $"Result: {result}",
                    error => $"Error: {error}");
            }

            Console.WriteLine(ToString(Divide(1, 2)));
            Console.WriteLine(ToString(Divide(-3, 4)));
            Console.WriteLine(ToString(Divide(2, 0)));
        }

        static void Main(string[] args)
        {
            EitherDemo();
        }
    }
}
