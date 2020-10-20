using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Factorial.Test
{
    [TestClass]
    public class Factorial_Test
    {
        private delegate T del_Factorial<T, K>(K n);

        private uint[] Stub
            => Enumerable
            .Range(0, 10)
            .Select(t => (uint)t)
            .ToArray();

        private double[] Expected
            => new double[]
            {
                1       ,
                1       ,
                2       ,
                6       ,
                24      ,
                120     ,
                720     ,
                5040    ,
                40320   ,
                362880  ,
                //3628800 ,
                //39916800,
            };
        private ulong[] Big_Expected
            => new ulong[]
            {
                1                  ,
                1                  ,
                2                  ,
                6                  ,
                24                 ,
                120                ,
                720                ,
                5040               ,
                40320              ,
                362880             ,
                3628800            ,
                39916800           ,
                479001600          ,
                6227020800         ,
                87178291200        ,
                1307674368000      ,
                20922789888000     ,
                355687428096000    ,
                6402373705728000   ,
                121645100408832000 ,
                2432902008176640000,
                // max value for ulong is 18446744073709551615, so max number is 20
            };


        [TestMethod]
        public void FactorialPow_OneInputValue()
            => Factorial_OneInputValue(Factorial.FactorialPow);

        [TestMethod]
        public void FactorialExp_OneInputValue()
            => Factorial_OneInputValue(Factorial.FactorialExp);

        [TestMethod]
        public void FactorialPow_ArrayValue()
            => Factorial_ArrayValue(Factorial.FactorialPow);

        [TestMethod]
        public void FactorialExp_ArrayValue()
            => Factorial_ArrayValue(Factorial.FactorialExp);

        [TestMethod]
        public void FactorialPow_Parallel()
            => Factorial_ArrayValue(Factorial.FactorialPow);

        [TestMethod]
        public void FactorialExp_Parallel()
            => Factorial_ArrayValue(Factorial.FactorialExp);

        [TestMethod]
        public void FactorialSlow_OneInputValue()
            => Factorial_OneInputValue(Factorial.FactorialSlow);

        //TODO: FactorialFast_OneInputValue()
        //[TestMethod]
        //public void FactorialFast_OneInputValue()
        //    => Factorial_OneInputValue(Factorial.FibonacciFast);


        private void Factorial_OneInputValue(del_Factorial<double, uint> fib)
        {
            // avarrage
            var str = new StringBuilder("\nInput data:\n");
            foreach (var i in Stub)
                str.Append(i).Append("\t");

            str.Append("\n\nExpected:\n");
            foreach (var i in Expected)
                str.Append(i).Append("\t");

            // act
            double[] actual = new double[Stub.Length];

            for (int i = 0; i < Stub.Length; i++)
                actual[i] = fib(Stub[i]);

            str.Append("\n\nActual:\n");
            foreach (var i in actual)
                str.Append(i).Append("\t");

            // assert
            for (int i = 0; i < actual.Length; i++)
                Assert.AreEqual(Expected[i], actual[i]);

            Debug.WriteLine(str.ToString());
        }

        private void Factorial_ArrayValue(del_Factorial<double[], uint[]> fib)
        {
            // avarrage
            var str = new StringBuilder("\nInput data:\n");
            foreach (var i in Stub)
                str.Append(i).Append("\t");

            str.Append("\n\nExpected:\n");
            foreach (var i in Expected)
                str.Append(i).Append("\t");

            // act
            double[] actual = fib(Stub);

            str.Append("\n\nActual:\n");
            foreach (var i in actual)
                str.Append(i).Append("\t");

            // assert
            for (int i = 0; i < actual.Length; i++)
                Assert.AreEqual(Expected[i], actual[i]);

            Debug.WriteLine(str.ToString());
        }



        [TestMethod]
        public void FactorialPow_OneInputValue_Exception()
            => Factorial_OneInputValueException_(Factorial.FactorialPow);

        [TestMethod]
        public void FactorialPow_ArrayValu_Exception()
            => Factorial_ArrayValue_Exception(Factorial.FactorialPow);

        [TestMethod]
        public void FactorialExp_OneInputValue_Exception()
            => Factorial_OneInputValueException_(Factorial.FactorialExp);

        [TestMethod]
        public void FactorialExp_ArrayValu_Exception()
            => Factorial_ArrayValue_Exception(Factorial.FactorialExp);



        private void Factorial_OneInputValueException_(del_Factorial<double, uint> fib)
        {
            // avarrage
            uint[] stub = new uint[] { 12 };

            string expected = "Absolute values more than 11 are not accurate. You should use other method.";

            // act - assert
            foreach (var i in stub)
                Assert.ThrowsException<Exception>(() => fib(i), expected);
        }

        private void Factorial_ArrayValue_Exception(del_Factorial<double[], uint[]> fib)
        {
            // avarrage
            uint[] stub = new uint[] { 12 };

            string expected = "Absolute values more than 11 are not accurate. You should use other method.";

            // act - assert
            Assert.ThrowsException<Exception>(() => fib(stub), expected);
        }


        private void Factorial_OneInputValue(del_Factorial<BigInteger, uint> fib)
        {
            // avarrage
            var str = new StringBuilder("\nInput data:\n");
            foreach (var i in Stub)
                str.Append(i).Append("\t");

            str.Append("\n\nExpected:\n");
            foreach (var i in Expected)
                str.Append(i).Append("\t");

            // act
            BigInteger[] actual = new BigInteger[Stub.Length];

            for (int i = 0; i < Stub.Length; i++)
                actual[i] = fib(Stub[i]);

            str.Append("\n\nActual:\n");
            foreach (var i in actual)
                str.Append(i).Append("\t");

            // assert
            for (int i = 0; i < actual.Length; i++)
                Assert.AreEqual((BigInteger)Expected[i], actual[i]);

            Debug.WriteLine(str.ToString());
        }


        [TestMethod]
        public void Factorial_Slow_OneInputValue_Par_0_93()
            => Factorial_OneInputValue_Par_0_20(Factorial.FactorialSlow);

        //TODO: Factorial_Fast_OneInputValue_Par_0_20()
        //[TestMethod]
        //public void Factorial_Fast_OneInputValue_Par_0_20()
        //    => Factorial_OneInputValue_Par_0_20(Factorial.FactorialFast);


        private void Factorial_OneInputValue_Par_0_20(del_Factorial<BigInteger, uint> fib)
        {
            // avarrage
            var str = new StringBuilder("\nResult data:\n\n");

            // act
            BigInteger[] actual = new BigInteger[Big_Expected.Length];

            Parallel.For(0, Big_Expected.Length, (i) => actual[i] = fib((uint)i));

            // assert
            Parallel.For(0, Big_Expected.Length, (i) => Assert.AreEqual(Big_Expected[i], actual[i]));

            // present
            for (int i = 0; i < actual.Length; i++)
                str.Append(i).Append(":\t").Append(actual[i]).Append("\n");

            Debug.WriteLine(str);
        }


        /// <summary>
        /// Method for testing max value before add exception,
        /// for use this method, you should comment "throw" block and change private -> public
        /// </summary>
        [TestMethod]
        private void MaxNumberWithAcurateValue()
        {
            // For present result
            var str = new StringBuilder("\nResult data:\n\n");

            // maximum number for other formula which dependes on acurate of "double" type
            uint maxN;

            BigInteger expected = default,
                actual = default;

            #region for Ramanujan approximation power formula
            for (maxN = 0; expected == actual; maxN++)
            {
                expected = Factorial.FactorialSlow(maxN);
                actual = (BigInteger)Factorial.FactorialPow(maxN);
            }

            maxN -= 2;
            expected = Factorial.FactorialSlow(maxN);
            actual = (BigInteger)Factorial.FactorialPow(maxN);

            str.Append("Acurate of Ramanujan approximation power formula:\n")
                .Append($"\tmax number: {maxN};\n")
                .Append($"\texpected value:\t{expected}\n")
                .Append($"\tactual value:\t{actual}\n");

            maxN++;
            expected = Factorial.FactorialSlow(maxN);
            actual = (BigInteger)Factorial.FactorialPow(maxN);

            // next value
            str.Append($"\n\tnext number: {maxN};\n")
                .Append($"\texpected value:\t{expected}\n")
                .Append($"\tactual value:\t{actual}\n");
            #endregion

            expected = actual = default;

            #region for Ramanujan approximation exponent formula
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
            #endregion

            // present
            Debug.WriteLine(str);
        }

        [TestMethod]
        public void CompareSpeedMethods()
        {
            // avarrage
            uint[] stub = Enumerable
                .Range(0, 10)
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

            timer.Restart();
            foreach (var i in stub)
                Factorial.FactorialSlow(i);
            timer.Stop();
            Debug.WriteLine($"Base calculate: {timer.Elapsed.TotalMilliseconds} ms\n");

        }

        // TODO: Extent the CompareSpeedMethods()
    }
}
