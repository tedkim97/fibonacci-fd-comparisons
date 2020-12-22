using System;
using System.Collections.Generic;
using System.Numerics;

namespace FibDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 100000;
            int repeat = 1000;

            var ITER_res_2 = Timer.TimeIt<ulong>(Fibonacci.FibIterative, N, repeat);
            Console.WriteLine($"Iteration({N}) <ulong> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(ITER_res_2)}");
            
            var FD_res_2 = Timer.TimeIt<ulong>(Fibonacci.FibFD, N, repeat);
            Console.WriteLine($"FDE({N}) <ulong> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FD_res_2)}");

            var FD_iter_2 = Timer.TimeIt<ulong>(Fibonacci.FibFDIter, N, repeat);
            Console.WriteLine($"FDE Iteration({N}) <ulong> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FD_iter_2)}");

            var FDTR_res_2 = Timer.TimeIt<ulong>(Fibonacci.FibFDTR, N, repeat);
            Console.WriteLine($"FDE Tail Recursion({N}) <ulong> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FDTR_res_2)}");
            
            Console.WriteLine("Systems.Numeric");
            // var TR_res = Timer.TimeIt<BigInteger>(FibonacciBigInt.FibTR, N, repeat);
            // Console.WriteLine($"Tail Recursion <BigInteger> {Timer.CalcPerformanceStatistic(TR_res)}");
            var ITER_res = Timer.TimeIt<BigInteger>(FibonacciBigInt.FibIterative, N, repeat);
            Console.WriteLine($"Iteration({N}) <BigInteger> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(ITER_res)}");

            var FD_res = Timer.TimeIt<BigInteger>(FibonacciBigInt.FibFD, N, repeat);
            Console.WriteLine($"FDE({N}) <BigInteger> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FD_res)}");
            
            var FD_iter = Timer.TimeIt<BigInteger>(FibonacciBigInt.FibFDIter, N, repeat);
            Console.WriteLine($"FDE Iteration({N}) <BigInteger> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FD_iter)}");

            var FDTR_res = Timer.TimeIt<BigInteger>(FibonacciBigInt.FibFDTR, N, repeat);
            Console.WriteLine($"FDE Tail Recursion({N}) <BigInteger> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FDTR_res)}");
        }
    }
}
