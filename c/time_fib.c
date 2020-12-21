#include "fibonacci.h"
#include <stdio.h> 
#include <math.h> 
#include <time.h>
#include <stdint.h>

int main(){
    clock_t b1 = clock();
	for(int j = 0; j < 10000; j++){
		for(int i = 0; i < 1000; i++){
			fib_iter(i);            
		}
	}
	clock_t e1 = clock();
	double time_elapsed_1 = (double)(e1 - b1) / CLOCKS_PER_SEC;
	printf("(ITER) Time elapsed %f\n", time_elapsed_1);

    clock_t b2 = clock();
	for(int j = 0; j < 10000; j++){
		for(int i = 0; i < 1000; i++){
			fib_tr(i, 0, 1);          
		}
	}
	clock_t e2 = clock();
	double time_elapsed_2 = (double)(e2 - b2) / CLOCKS_PER_SEC;
	printf("(TR) Time elapsed %f\n", time_elapsed_2);    

    clock_t b3 = clock();
	for(int j = 0; j < 10000; j++){
		for(int i = 0; i < 1000; i++){
			fib_fde_iter(i);            
		}
	}
	clock_t e3 = clock();
	double time_elapsed_3 = (double)(e3 - b3) / CLOCKS_PER_SEC;
	printf("(FDE ITER) Time elapsed %f\n", time_elapsed_3);

    clock_t b4 = clock();
	for(int j = 0; j < 10000; j++){
		for(int i = 0; i < 1000; i++){
			fib_fde_tr(i);            
		}
	}
	clock_t e4 = clock();
	double time_elapsed_4 = (double)(e4 - b4) / CLOCKS_PER_SEC;
	printf("(FDE TR) Time elapsed %f\n", time_elapsed_4);
}