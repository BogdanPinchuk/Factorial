#region Resources
// 1. https://en.wikipedia.org/wiki/Factorial
// 2. https://uk.wikipedia.org/wiki/Факторіал
// 3. https://en.wikipedia.org/wiki/Gamma_function
// 4. https://uk.wikipedia.org/wiki/Гамма-функція
#endregion

using System;
using System.Numerics;

namespace Factorial
{
    /// <summary>
    /// Factorial number
    /// </summary>
    public class Factorial
    {
        private const string error = "Absolute values more than 11 are not accurate. You should use other method.";

        /// <summary>
        /// Ramanujan approximation power
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Factorial number</returns>
        public static double FactorialPow(uint n)
        {
            // fast answer
            if (n == 0 || n == 1)
                return 1;
            if (n == 2)
                return 2;
            if (n > 11)
                throw new Exception(error);

            // parts of equation
            double sr = Math.Sqrt(2 * Math.PI * n),
                pw = Math.Pow(n / Math.E, n),
                fr = Math.Pow(1 + 1 / (2.0 * n) + 1 / (8.0 * n * n), 1 / 6.0);    // fraction

            // Result
            double res = sr * pw * fr;

            return Math.Round(res, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Ramanujan approximation exponent
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Factorial number</returns>
        public static double FactorialExp(uint n)
        {
            // fast answer
            if (n == 0 || n == 1)
                return 1;
            if (n == 2)
                return 2;
            if (n > 11)
                throw new Exception(error);

            // parts of equation
            double sr = Math.Sqrt(2 * Math.PI * n),
                pw = Math.Pow(n / Math.E, n),
                fr = Math.Exp(1 / (12.0 * n) - 1 / (360.0 * n * n * n));

            // Result
            double res = sr * pw * fr;

            return Math.Round(res, MidpointRounding.AwayFromZero);
        }


        /// <summary>
        /// Slow calculate but accurate result, can calculate absolute value more then 70
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Factorial number</returns>
        public static unsafe BigInteger FactorialSlow(uint n)
        {
            // absolutely value of n 
            uint value = n;

            // fast answer or start data for calculate
            if (n == 0 || n == 1)
                return 1;
            else if (value == 2)
                return 2;

            // calculate
            BigInteger result = 2;

            for (uint i = 3; i <= value; i++)
                result *= i;

            return result;
        }








    }
}
