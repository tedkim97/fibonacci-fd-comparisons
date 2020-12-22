import argparse
import math
import sys
import timeit

def load_file(fname: str):
    with open(fname, 'r') as file:
        code = file.read()
    return code

if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='time fibonacci implementations')
    parser.add_argument('-n', '--nth', type=int, help='the N-th term of the fibonacci sequence')
    parser.add_argument('-r', '--runs', type= int, help='the number of times fib(N) is calculated')
    args = parser.parse_args()

    N = 100000 if (args.nth is None) else args.nth
    nruns = 1000 if (args.runs is None) else args.runs
    if(nruns <= 0 or N < 0):
        print('invalid parameter for N or number of runs')
        sys.exit()

    if (N > 10000000):
        sys.setrecursionlimit(math.floor(math.log(N, 2)) + 100)
        print(f"adjusting recursion limit to: {sys.getrecursionlimit()}")

    print(f"Calculating speed for fib({N}) - {nruns} times")
    func_calls = ['fib_iter({})', 'fib_fd_iter({})', 'fib_fd_tr({})']
    setup = load_file('fibonacci.py')
    for fc in func_calls:
        temp_stmt = fc.format(N)
        t = timeit.Timer(stmt=temp_stmt ,setup=setup)
        print(f"{temp_stmt} => {t.timeit(nruns)}")
