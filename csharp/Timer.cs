using System;
using System.Collections.Generic;
using System.Linq;

namespace FibDemo
{
    class Timer
    {
        private static long Time<T>(Func<int, T> funcToBeTimed, int argument) {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            funcToBeTimed(argument);
            watch.Stop();
            long elapsedTime = watch.ElapsedTicks;
            return elapsedTime;
        }

        public static List<long> TimeIt<T>(Func<int, T> funcToBeTimed, int argument, int numTimes = 50) {
            List<long> timeResults = new List<long>(numTimes);
            for (int i = 0; i < numTimes; i++) {
                timeResults.Add(Time<T>(funcToBeTimed, argument));
            }
            return timeResults;
        }

        public static string CalcPerformanceStatistic(List<long> times) {
            int N = times.Count;
            long longestExecTime = times.Max();
            long shortestExecTime = times.Min();
            double averageExecTime = times.Average();
            double stdExecTime = 0;
            if (N > 1) {
                double ssq = times.Sum(d => Math.Pow((d - averageExecTime), 2));
                stdExecTime = Math.Sqrt(ssq / N);
            }
            return $"N={N} - avg={averageExecTime} - std={stdExecTime} - min={shortestExecTime} - max={longestExecTime}";
        }
    }
}