#region Resources
// 1. https://en.wikipedia.org/wiki/Factorial
// 2. https://uk.wikipedia.org/wiki/Факторіал
// 3. https://en.wikipedia.org/wiki/Gamma_function
// 4. https://uk.wikipedia.org/wiki/Гамма-функція
#endregion

using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Factorial
{
    /// <summary>
    /// Factorial number
    /// </summary>
    public class Factorial
    {
        private const string error = "Absolute values more than 11 are not accurate. You should use other method.";

        /// <summary>
        /// Ramanujan approximation power formula
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Factorial number</returns>
        public static double FactorialPow(uint n)
        {
            // fast answer
            if (n <= 1)
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
        /// Ramanujan approximation power formula
        /// </summary>
        /// <param name="array">array value</param>
        /// <returns>array Factorial number</returns>
        public static unsafe double[] FibonacciBine(uint[] array)
        {
            // accurate
            if (array.Max() > 11)
                throw new Exception(error);

            // Square root of 2 * pi
            double sr2pi = Math.Sqrt(2 * Math.PI);

            // some part of formula
            double[] f_st = new double[array.Length],
                s_nd = new double[array.Length],
                t_rd = new double[array.Length],
                res = new double[array.Length];

            fixed (double* f = f_st, s = s_nd, t = t_rd, r = res)
            fixed (uint* a = array)
            {
                double* _f = f, _s = s, _t = t, _r = r;
                uint* _a = a;

                for (int i = 0; i < array.Length; i++, _f++, _s++, _t++, _a++, _r++)
                {
                    // fast answer
                    if (*_a == 0 || *_a == 1)
                    {
                        *_r = 1;
                        continue;
                    }
                    else if (*_a == 2)
                    {
                        *_r = 2;
                        continue;
                    }

                    *_f = sr2pi * Math.Sqrt(*_a);
                    *_s = Math.Pow(*_a / Math.E, *_a);
                    *_t = Math.Pow(1 + 1 / (2.0 * *_a) + 1 / (8.0 * *_a * *_a), 1 / 6.0);
                    *_r = *_f * *_s * *_t;
                    *_r = Math.Round(*_r, MidpointRounding.AwayFromZero);
                }
            }

            return res;
        }

        /// <summary>
        /// Ramanujan approximation power formula using parallel calculations
        /// </summary>
        /// <param name="array">array value</param>
        /// <returns>array Factorial number</returns>
        public static double[] FibonacciBine_Par(uint[] array)
        {
            // accurate
            if (array.Max() > 11)
                throw new Exception(error);

            // Square root of 2 * pi
            double sr2pi = Math.Sqrt(2 * Math.PI);

            double[] res = new double[array.Length];

            Parallel.For(0, array.Length, (i) =>
            {
                // fast answer
                if (Math.Abs(array[i]) <= 1)
                    res[i] = 1;
                else if (Math.Abs(array[i]) == 2)
                    res[i] = (array[i] > 0) ? 1 : -1;

                double phi_n = Math.Pow(phi, array[i]);
                // Result
                double result = (phi_n - Math.Pow(-1, array[i]) / phi_n) / sr5;

                res[i] = Math.Round(result, MidpointRounding.AwayFromZero);
            });

            return res;
        }







        /// <summary>
        /// Ramanujan approximation exponent formula
        /// </summary>
        /// <param name="n">value</param>
        /// <returns>Factorial number</returns>
        public static double FactorialExp(uint n)
        {
            // fast answer
            if (n <= 1)
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
