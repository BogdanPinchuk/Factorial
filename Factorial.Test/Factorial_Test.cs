using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Diagnostics;
using System.Linq;

namespace Factorial.Test
{
    [TestClass]
    public class Factorial_Test
    {
        private delegate T del_Factorial<T, K>(K n);

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
