#include <gmpxx.h>
#include <iostream>
#include <tuple>
#include <math.h>

using std::cout;
using std::tuple;

namespace fib {
	template <class numType>
	numType fib_naive(int n) {
		if(n <= 1){
			return n;
		}
		return fib_naive<numType>(n-1) + fib_naive<numType>(n-2);
	}

	template <class numType>
	numType fib_iter(int n) {
		numType fi1 = 0;
		numType fi2 = 1;
		
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

	template <class numType>
	numType fib_tr_int(int n, numType n0, numType n1){
		if(n == 0) {
			return n0;
		}
		return fib_tr_int<numType>(n - 1, n1, n0 + n1);
	}

	template <class numType>
	numType fib_tr_pub(int n) {
		return fib_tr_int<numType>(n, 0, 1);
	}

	template <class numType>
	tuple<numType, numType> fib_fd(int n) {
		if(n == 0) {
			return tuple<numType, numType> {0, 1};
		}

		tuple<numType, numType> res = fib_fd<numType>(n / 2);
		// Why don't we just use auto? CAUSES SEGFAULTS
		// https://gmplib.org/manual/C_002b_002b-Interface-Limitations
		numType t0 = std::get<0>(res);
		numType t1 = std::get<1>(res);
		numType c = t0 * (t1 * 2 - t0);
		numType d = (t0 * t0) + (t1 * t1);
		if(n % 2 == 0){
			return tuple<numType, numType> {c, d};
		} else {
			return tuple<numType, numType> {d, c + d};
		}
	}

	template <class numType>
	numType fib_fd_pub(int n) {
		return std::get<0>(fib_fd<numType>(n));
	}

	template <class numType>
	numType fib_fd_iter(int n) {
		if(n == 0) {
			return numType(0);
		}

		int num_fib_calls = floor(log2(n)) + 1;
		int nth_call[num_fib_calls];
	    for(int i = 0; i < num_fib_calls; i++) {
	        nth_call[num_fib_calls - i - 1] = n;
	        n /= 2;
	    }

	    numType c = 0, d = 1;
	    numType tempc, tempd;
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

	template <class numType>
	numType fib_fd_tr_int(int n, int ns[], int ind, int maxVal) {
	    if(n == 0) {
	        numType tc = 0, td = 1;
	        numType tempc, tempd;

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
	        return fib_fd_tr_int<numType>(n/2, ns, ind+1, maxVal);
	    }
	}

	template <class numType>
	numType fib_fd_tr(int n) {
	    if(n == 0) {
	        return numType(0);
	    }
	    const int max_len = floor(log2(n)) + 1;
	    int nth_call[max_len];
	    return fib_fd_tr_int<numType>(n, nth_call, 0, max_len);
	}

	template <class numType>
	void compare(int n) {
		cout << "calculating fib(" << n << ")" << std::endl;
		cout << "iterative: " <<fib_iter<numType>(n) << std::endl;
		cout << "fast double (recursive): " << fib_fd_pub<numType>(n) << std::endl;
		cout << "fast double (iterative): " << fib_fd_iter<numType>(n) << std::endl;
		cout << "fast double (tail recursive): " << fib_fd_tr<numType>(n) << std::endl;
	}
}