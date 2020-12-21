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

            // var TR_res_2 = Timer.TimeIt<ulong>(Fibonacci.FibTR, N, repeat);
            // Console.WriteLine($"Tail Recursion <ulong> {Timer.CalcPerformanceStatistic(TR_res_2)}");
            var ITER_res_2 = Timer.TimeIt<ulong>(Fibonacci.FibIterative, N, repeat);
            Console.WriteLine("Iteration <ulong> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(ITER_res_2)}");
            
            var FDE_res_2 = Timer.TimeIt<ulong>(Fibonacci.FibFDE, N, repeat);
            Console.WriteLine("FDE <ulong> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FDE_res_2)}");

            var FDE_iter_2 = Timer.TimeIt<ulong>(Fibonacci.FibFDEIter, N, repeat);
            Console.WriteLine("FDE Iteration <ulong> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FDE_iter_2)}");

            var FDETR_res_2 = Timer.TimeIt<ulong>(Fibonacci.FibFDETR, N, repeat);
            Console.WriteLine("FDE Tail Recursion <ulong>");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FDETR_res_2)}");
            
            Console.WriteLine("Systems.Numeric");
            // var TR_res = Timer.TimeIt<BigInteger>(FibonacciBigInt.FibTR, N, repeat);
            // Console.WriteLine($"Tail Recursion <BigInteger> {Timer.CalcPerformanceStatistic(TR_res)}");
            var ITER_res = Timer.TimeIt<BigInteger>(FibonacciBigInt.FibIterative, N, repeat);
            Console.WriteLine($"Iteration <BigInteger> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(ITER_res)}");

            var FDE_res = Timer.TimeIt<BigInteger>(FibonacciBigInt.FibFDE, N, repeat);
            Console.WriteLine("FDE <BigInteger> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FDE_res)}");
            
            var FDE_iter = Timer.TimeIt<BigInteger>(FibonacciBigInt.FibFDEIter, N, repeat);
            Console.WriteLine($"FDE Iteration <BigInteger> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FDE_iter)}");

            var FDETR_res = Timer.TimeIt<BigInteger>(FibonacciBigInt.FibFDETR, N, repeat);
            Console.WriteLine("FDE Tail Recursion <BigInteger> (ticks)");
            Console.WriteLine($"{Timer.CalcPerformanceStatistic(FDETR_res)}");
        }
    }
}
