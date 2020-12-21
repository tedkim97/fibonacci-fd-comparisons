import math
# from functools import lru_cache

# Simple Recursion - O(2^n)
def fib_naive(n: int):
    if(n <= 1):
        return n
    return fib_naive(n-1) + fib_naive(n-2)

# Simple Iteration - O(n)
def fib_iter(n: int):
    n0, n1 = 0, 1
    while(n > 0):
        n0, n1 = n1, n0 + n1
        n = n - 1
    return n0

# Simple Tail Recursion - O(n)
# Python Interpreter does not have TCO
def _fib_tail_recursive(n: int, n0, n1):
    if(n == 0):
        return n0
    return _fib_tail_recursive(n-1, n1, n0 + n1)

# Wrapper for the Tail Recursive Fibonacci Sequence
def fib_tr(n: int):
    return _fib_tail_recursive(n, 0, 1)

# Tail Recursive Fast Double Exponentiation - O(log n)
def _fib_fde_tail_recursive(n: int, listn: list, ind: int):
    if(n == 0):
        tc, td = 0, 1
        for i in listn:
            tc, td = tc * (td * 2 - tc), (tc * tc) + (td * td)
            if(i % 2 != 0):
                tc, td = td, tc + td
        return tc
    
    listn[len(listn) - ind - 1] = n
    return _fib_fde_tail_recursive(n // 2, listn, ind + 1)

# Wrapper for the Tail Recursive Fast Double Exponentiation
def fib_fde_tr(n: int):
    if(n == 0):
        return 0
    num_fib_calls = math.floor(math.log(n, 2)) + 1
    listn = [None] * num_fib_calls
    return _fib_fde_tail_recursive(n, listn, 0)

# Iterative Version of the Fast Double Exponentiation Method - O(log n)
def fib_fde_iter(n: int):
    if(n == 0):
        return 0

    num_fib_calls = math.floor(math.log(n, 2)) + 1
    listn = [None] * num_fib_calls
    for ind in range(num_fib_calls):
        listn[num_fib_calls - ind - 1] = n
        n //= 2

    tc, td = 0, 1
    for i in listn:
        tc, td = tc * (td * 2 - tc), (tc * tc) + (td * td)
        if(i % 2 != 0):
            tc, td = td, tc + td

    return tc

if __name__ == '__main__':
    print("printing fibonacci sequence")
    for n in range(301):
        n_fib_iter = fib_fde_iter(n)
        n_fib_tr = fib_fde_tr(n)
        print(f"Equality Check = {n_fib_iter == n_fib_tr}")
        print(f"{n}th term = {n_fib_tr} {n_fib_iter} ")
    