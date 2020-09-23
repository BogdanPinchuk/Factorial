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
            int[] stub = Enumerable
                .Range(0, 71)
                .ToArray();

            // act
            Stopwatch timer = new Stopwatch();

            timer.Start();
            foreach (var i in stub)
                //Fibonacci.FibonacciBine(i);
            timer.Stop();

            Debug.WriteLine($"\nBinet's formula: {timer.Elapsed.TotalMilliseconds} ms\n");
        }
    }
}
