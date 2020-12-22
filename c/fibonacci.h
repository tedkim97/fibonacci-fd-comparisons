#ifndef FIBONACCI_H
#define FIBONACCI_H

#include <stdio.h> 
#include <math.h>
#include <stdint.h>
#include <stdbool.h>

// Calculate the Nth term of the fibonacci sequence with naive recursion
uint64_t fib_naive(int n);

// Calculate the Nth term of the fibonacci sequence iteratively
uint64_t fib_iter(int n);

// Calculate the Nth term of the fibonacci sequence with tail recursion
uint64_t fib_tr(int n, uint64_t n0, uint64_t n1);

// Calculate the Nth term of the fibonacci sequence with the 
// Fast Double Exponentiation method (iteratively)
uint64_t fib_fd_iter(int n);

// Calculate the Nth term of the fibonacci sequence with the 
// Fast Double Exponentiation method (tail recursively)
uint64_t fib_fd_tail_recursive(int n, int ns[], int ind, int maxVal);

// Wrapper for fib_fd_tail_recursive
uint64_t fib_fd_tr(int n);

#endif