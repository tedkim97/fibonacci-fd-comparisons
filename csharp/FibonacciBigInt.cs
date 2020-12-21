using System;
using System.Numerics;

namespace FibDemo
{
    class FibonacciBigInt
    {
        public static BigInteger FibNaive(int n) {
            if (n == 0) { return BigInteger.Zero; }
            if (n == 1) { return BigInteger.One; }
            return FibNaive(n-1) + FibNaive(n-2);
        }
        public static BigInteger FibIterative(int n) {
            BigInteger a = BigInteger.Zero;
            BigInteger b = BigInteger.One;
            BigInteger c;
            
            if (n == 0) {
                return a;
            }

            if (n == 1) {
                return b;
            }

            // I wasn't sure how the 
            // b = b + a; a = b - a; trick
            // worked for BigIntegers, so I 
            // stuck with the temp var trickdi
            for (int i = 2; i <= n; i++) {
                
                c = b + a;
                a = b;
                b = c;
            }
            
            return b;
        }

        public static BigInteger FibTR(int n) {
            return FibTailRecursive(n, BigInteger.Zero, BigInteger.One);
        }

        private static BigInteger FibTailRecursive(int n, BigInteger a, BigInteger b) {
            if (n == 0) {
                return a;
            }
            return FibTailRecursive(n-1, b, BigInteger.Add(a, b));
        }

        public static BigInteger FibFDE(int n) {
            return FibFastDouble(n).Item1;
        }

        private static Tuple<BigInteger, BigInteger> FibFastDouble(int n) {
            if (n == 0) {
                return new Tuple<BigInteger, BigInteger>(BigInteger.Zero, BigInteger.One);
            }

            Tuple<BigInteger, BigInteger> res = FibFastDouble(n / 2);
            BigInteger a = res.Item1;
            BigInteger b = res.Item2;
            BigInteger c = a * (b * 2 - a);
            BigInteger d = (a * a) + (b * b); 

            if (n % 2 == 0)
            {
                return new Tuple<BigInteger, BigInteger>(c, d);
            }
            else
            {
                return new Tuple<BigInteger, BigInteger>(d, c + d);
            }
        }

        public static BigInteger FibFDETR(int n) {
            if (n == 0) {
                return BigInteger.Zero;
            }
            int cache_size = (int) Math.Floor(Math.Log(n, 2)) + 1;
            int[] cache = new int[cache_size];
            return FibFDETailRecursion(n, cache, 0);
        }

        private static BigInteger FibFDETailRecursion(int n, int[] ns, int ind) {
            if (n == 0) {
                BigInteger c = BigInteger.Zero;
                BigInteger d = BigInteger.One;
                BigInteger tempc, tempd;
                foreach(int i in ns) {
                    tempc = c;
                    tempd = d;
                    c = tempc * (tempd * 2 - tempc);
                    d = (tempc * tempc) + (tempd * tempd);
                    if (i % 2 != 0) {
                        d = d + c;
                        c = d - c;
                    }
                }
                return c;
            }
            ns[ns.Length - ind - 1] = n;
            return FibFDETailRecursion(n / 2, ns, ind + 1);
        }

        public static BigInteger FibFDEIter(int n) {
            if (n == 0) {
                return BigInteger.Zero;
            }

            int num_fib_calls = (int) Math.Floor(Math.Log(n, 2)) + 1;
            int[] ns = new int[num_fib_calls];

            for (int i = 0; i < num_fib_calls; i++) {
                ns[num_fib_calls - i - 1] = n;
                n /= 2;
            }

            BigInteger tc = BigInteger.Zero;
            BigInteger td = BigInteger.One;
            BigInteger tempc, tempd;
            foreach (int tempN in ns) {
                tempc = tc;
                tempd = td;
                tc = tempc * (tempd * 2 - tempc);
                td = (tempc * tempc) + (tempd * tempd);
                if (tempN % 2 != 0) {
                    td = td + tc;
                    tc = td - tc;
                }
            }

            return tc;
        }


    }
}
