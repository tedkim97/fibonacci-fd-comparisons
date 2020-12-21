#include "fibonacci.h"
#include <stdio.h> 
#include <math.h>
#include <stdint.h>
#include <stdbool.h>

uint64_t fib_naive(int n) {
    if(n == 0) {
        return 0;
    }
    if(n == 1) {
        return 1;
    }
    return fib_naive(n-1) + fib_naive(n-2);
}

uint64_t fib_iter(int n) {
    uint64_t fi1 = 0; 
    uint64_t fi2 = 1; 

    if(n == 0) {
        return fi1; 
    }
    if(n == 1) {
        return fi2; 
    }
    for(int i = 2; i <= n; i++) {
        fi2 = fi2 + fi1;
        fi1 = fi2 - fi1; 
    }
    return fi2; 
}

uint64_t fib_tr(int n, uint64_t n0, uint64_t n1) {
    if(n == 0) {
        return n0;
    }
    return fib_tr(n - 1, n1, n0 + n1);
}

uint64_t fib_fde_iter(int n) {
    if(n == 0) {
        return 0;
    }

    int num_fib_calls = floor(log2(n)) + 1;
    int nth_call[num_fib_calls];
    for(int i = 0; i < num_fib_calls; i++) {
        nth_call[num_fib_calls - i - 1] = n;
        n /= 2;
    }

    uint64_t c = 0, d = 1;
    uint64_t tempc, tempd;
    bool iseven;
    for(int j = 0; j < num_fib_calls; j++) {
        tempc = c * (d * 2 - c);
        tempd = (c * c) + (d * d);
        iseven = nth_call[j] % 2 == 0;
        c = (iseven) ? tempc : tempd; 
        d = (iseven) ? tempd : tempc + tempd;
    }

    return c;
}

uint64_t fib_fde_tail_recursive(int n, int ns[], int ind, int maxVal) {
    if(n == 0) {
        uint64_t tc = 0, td = 1;
        uint64_t tempc, tempd;
        bool iseven;

        for(int i = 0; i < maxVal; i++) {
            tempc = tc * (td * 2 - tc);
            tempd = (tc * tc) + (td * td);
            iseven = ns[i] % 2 == 0; 
            tc = (iseven) ? tempc : tempd;
            td = (iseven) ? tempd : tempc + tempd;
        }

        return tc;

    } else {
        ns[maxVal - ind - 1] = n;
        return fib_fde_tail_recursive(n/2, ns, ind+1, maxVal);
    }
}

uint64_t fib_fde_tr(int n) {
    if(n == 0) {
        return 0;
    }
    const int max_len = floor(log2(n)) + 1;
    int nth_call[max_len];
    return fib_fde_tail_recursive(n, nth_call, 0, max_len);
}