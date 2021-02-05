#include "fibonacci.h"
#include <gmpxx.h>
#include <iostream>
#include <chrono>

using std::cout;

template <class numType>
void time(int N, int trials) {
	cout << "calculating Fib(" << N << ") with " << trials << std::endl;
	auto b1 = std::chrono::high_resolution_clock::now();
	for(int j = 0; j < trials; j++) {
		fib::fib_iter<numType>(N);
	}
	auto e1 = std::chrono::high_resolution_clock::now();
	std::chrono::duration<double> time_elapsed_1 = e1 - b1;
	cout << "(ITER) Time elapsed: " << time_elapsed_1.count() << std::endl;

	auto b2 = std::chrono::high_resolution_clock::now();
	for(int j = 0; j < trials; j++) {
		fib::fib_fd_pub<numType>(N);
	}
	auto e2 = std::chrono::high_resolution_clock::now();
	std::chrono::duration<double> time_elapsed_2 = e2 - b2;
	cout << "(Fast Doubling Recursion) Time elapsed: " << time_elapsed_2.count() << std::endl;

	auto b3 = std::chrono::high_resolution_clock::now();
	for(int j = 0; j < trials; j++) {
		fib::fib_fd_iter<numType>(N);
	}
	auto e3 = std::chrono::high_resolution_clock::now();
	std::chrono::duration<double> time_elapsed_3 = e3 - b3;
	cout << "(Fast Doubling ITER) Time elapsed: " << time_elapsed_3.count() << std::endl;

	auto b4 = std::chrono::high_resolution_clock::now();
	for(int j = 0; j < trials; j++) {
		fib::fib_fd_tr<numType>(N);
	}
	auto e4 = std::chrono::high_resolution_clock::now();
	std::chrono::duration<double> time_elapsed_4 = e4 - b4;
	cout << "(Fast Doubling Tail Recursive) Time elapsed: " << time_elapsed_4.count() << std::endl;
}

int main() {
	int N = 1000000;
	int trials = 10000;
	time<uint64_t>(N, trials);

}