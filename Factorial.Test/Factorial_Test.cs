using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Factorial.Test
{
    [TestClass]
    public class Factorial_Test
    {
        private delegate T del_Factorial<T, K>(K n);

        /// <summary>
        /// Method for testing max value before add exception,
        /// for use this method, you should comment "throw" block and change private -> public
        /// </summary>
        [TestMethod]
        public void MaxNumberWithAcurateValue()
        {
            // For present result
            var str = new StringBuilder("\nResult data:\n\n");

            // maximum number for other formula which dependes on acurate of "double" type
            uint maxN = 0;

            BigInteger expected = default,
                actual = default;

            // for Ramanujan approximation power formula
            for (maxN = 0; expected == actual; maxN++)
            {
                expected = Factorial.FactorialSlow(maxN);
                actual = (BigInteger)Factorial.FactorialExp(maxN);
            }

            maxN -= 2;
            expected = Factorial.FactorialSlow(maxN);
            actual = (BigInteger)Factorial.FactorialExp(maxN);

            str.Append("Acurate of Ramanujan approximation power formula:\n")
                .Append($"\tmax number: {maxN};\n")
                .Append($"\texpected value:\t{expected}\n")
                .Append($"\tactual value:\t{actual}\n");

            maxN++;
            expected = Factorial.FactorialSlow(maxN);
            actual = (BigInteger)Factorial.FactorialExp(maxN);

            // next value
            str.Append($"\n\tnext number: {maxN};\n")
                .Append($"\texpected value:\t{expected}\n")
                .Append($"\tactual value:\t{actual}\n");

            expected = actual = default;

            // for Ramanujan approximation exponent formula
            for (maxN = 0; expected == actual; maxN++)
            {
                expected = Factorial.FactorialSlow(maxN);
                actual = (BigInteger)Factorial.FactorialExp(maxN);
            }

            maxN -= 2;
            expected = Factorial.FactorialSlow(maxN);
            actual = (BigInteger)Factorial.FactorialExp(maxN);

            str.Append("\nAcurate of Ramanujan approximation exponent formula:\n")
                .Append($"\tmax number: {maxN};\n")
                .Append($"\texpected value:\t{expected}\n")
                .Append($"\tactual value:\t{actual}\n");

            maxN++;
            expected = Factorial.FactorialSlow(maxN);
            actual = (BigInteger)Factorial.FactorialExp(maxN);

            // next value
            str.Append($"\n\tnext number: {maxN};\n")
                .Append($"\texpected value:\t{expected}\n")
                .Append($"\tactual value:\t{actual}\n");

            // present
            Debug.WriteLine(str);
        }

        [TestMethod]
        public void CompareSpeedMethods()
        {
            // avarrage
            uint[] stub = Enumerable
                .Range(0, 71)
                .Select(t => (uint)t)
                .ToArray();

            // act
            Stopwatch timer = new Stopwatch();

            timer.Start();
            foreach (var i in stub)
                Factorial.FactorialPow(i);
            timer.Stop();
            Debug.WriteLine($"\nRamanujan approximation power: {timer.Elapsed.TotalMilliseconds} ms\n");

            timer.Restart();
            foreach (var i in stub)
                Factorial.FactorialExp(i);
            timer.Stop();
            Debug.WriteLine($"Ramanujan approximation exponent: {timer.Elapsed.TotalMilliseconds} ms\n");
        }

    }
}
