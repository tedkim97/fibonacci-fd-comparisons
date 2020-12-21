using System;

namespace FibDemo
{
    class Fibonacci
    {
        public static ulong FibNaive(int n) {
            if (n == 0) { return 0; }
            if (n == 1) { return 1; }
            return FibNaive(n-1) + FibNaive(n-2);
        }

        public static ulong FibIterative(int n) {
            ulong a = 0, b = 1;
            if (n == 0) {
                return a;
            }

            if (n == 1) {
                return b;
            }

            for (int i = 2; i <= n; i++) {
                b = b + a;
                a = b - a;
            }

            return b;
        }

        public static ulong FibTR(int n) {
            return FibTailRecursive(n, 0, 1);
        }

        private static ulong FibTailRecursive(int n, ulong a, ulong b) {
            if (n == 0) {
                return a;
            }
            return FibTailRecursive(n - 1, b, a + b);
        }

        public static ulong FibFDE(int n) {
            return FibFastDouble(n).Item1;
        }

        private static Tuple<ulong, ulong> FibFastDouble(int n) {
            if (n == 0) {
                return new Tuple<ulong, ulong>(0, 1);
            } 
            
            Tuple<ulong, ulong> res = FibFastDouble(n / 2);
            ulong a = res.Item1;
            ulong b = res.Item2;
            ulong c = a * (b * 2 - a);
            ulong d = (a * a) + (b * b);
            
            if (n % 2 == 0) {
                return new Tuple<ulong, ulong>(c, d);
            } else {
                return new Tuple<ulong, ulong>(d, c + d);
            }
            
        }

        public static ulong FibFDETR(int n) {
            if (n == 0) {
                return 0;
            }
            int cache_size = (int) Math.Floor(Math.Log(n, 2)) + 1;
            int[] cache = new int[cache_size];
            return FibFDETailRecursion(n, cache, 0);
        }

        private static ulong FibFDETailRecursion(int n, int[] ns, int ind) {
            if (n == 0) {
                ulong c = 0, d = 1;
                ulong tempc, tempd;
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

        public static ulong FibFDEIter(int n) {
            if (n == 0) {
                return 0;
            }

            int num_fib_calls = (int) Math.Floor(Math.Log(n, 2)) + 1;
            int[] ns = new int[num_fib_calls];

            for (int i = 0; i < num_fib_calls; i++) {
                ns[num_fib_calls - i - 1] = n;
                n /= 2;
            }

            ulong tc = 0, td = 1;
            ulong tempc, tempd;
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