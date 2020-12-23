# Iterative and Tail Recursive Fibonacci Sequence Implementations

Examining Iterative Variations and Tail Call Recursive implementations of the Fast Doubling Method for computing the Fibonacci sequence. These implementations were programmed in `C (c11 with gcc 7.3.0)`, `C# (.NET 5.0)` and `Python (3.7.2)`.


## C results
The implementations are called `fib_iter`, `fib_tr`, `fib_fd_iter`, and `fib_fd_tail_recursive` that correspond to the O(n) iterative, O(n) tail-recursive, O(log n) iterative, and O(log n) tail-recursive versions. The timing function checks how long it takes each implementation to calculate `fib(100000)` 1000 times. Read out the source for more details. 

Examining the assemblies, tail call optimization for the tail recursive naive method occurs at the `-O2` level while the FD (fast double exponentiation) implementation is optimized at `-O3`. Note that when I only ran the TCO optimization flag (`gcc -Wall -foptimize-sibling-calls ...`), the compiler didn't eliminate the recursion in the assembly, and the performance was the same as running no optimization. 

##### Using No GCC Optimizations
Use: `gcc -Wall -std=c11 -o compare_no_opt.out fibonacci.c time_fib.c -lm`

```bash
./compare_no_opt.out

(ITER) Time elapsed 0.306616
(TR NAIVE) Time elapsed 1.028053
(FD ITER) Time elapsed 0.000167
(FD TR) Time elapsed 0.000228
```

##### Using O2 Optimizations
Use: `gcc -Wall -std=c11 -O2 -o compare_O2.out fibonacci.c time_fib.c -lm`. Here is where TCO is applied to `fib_tr`.
```bash
./compare_O2.out

(ITER) Time elapsed 0.112924
(TR NAIVE) Time elapsed 0.038231
(FD ITER) Time elapsed 0.000077
(FD TR) Time elapsed 0.000076
```

##### Using O3 Optimizations
Use: `gcc -Wall -std=c11 -O3 -o compare_O3.out fibonacci.c time_fib.c -lm`. Here is where TCO is applied to `fib_fd_tail_recursive`.
```bash
./compare_O3.out

(ITER) Time elapsed 0.104128
(TR NAIVE) Time elapsed 0.038246
(FD ITER) Time elapsed 0.000078
(FD TR) Time elapsed 0.000076
```

#### Atomic Type Notes
Note that the unsigned 64bit integer (`uint64_t`) quickly overflows, so this ceases to be useful past the 93rd term (i.e It's very wrong, but fast). You can use some biginteger library in C (or build your own) to fix this behavior. 

<p align="center">
	<img src="assets/calculations_shen_comix.png" alt="using atomic types" width="300"/>
	<center>credit to shencomix</center>
</p>


## C# results

[I remember reading in a stack overflow comment that C# with .NET 4.0 provided TCO optimization support](https://stackoverflow.com/questions/55008730/why-is-my-tail-call-optimized-implementation-faster-than-the-normal-recursive-if) and I *naively* assumed that it would be supported in any c# code I wrote. However, the speed and opcode of my program indicated that it wasn't happening here. [This post gives a bit more insight about optimizations and TCO behavior in c#](https://stackoverflow.com/a/15865150/14634093). Ultimately if you really want TCO, F# would be a better .NET language choice. 
 
Just like the C implementation, unsigned 64bit integer (`ulong`) also overflows. Fortunately Microsoft provides a native "BigInteger" class that operates just like other number types! This comes with a mild performance hit. 

##### Uint64
```bash
dotnet run

Iteration(100000) <ulong> (ticks)
trials=1000 - avg=2763.548 - std=549.7612679118089 - min=2499 - max=8785
FD (100000) <ulong> (ticks)
trials=1000 - avg=9.499 - std=106.87950223967235 - min=3 - max=3374
FD Iteration(100000) <ulong> (ticks)
trials=1000 - avg=4.738 - std=70.77182600441799 - min=1 - max=2237
FD Tail Recursion(100000) <ulong> (ticks)
trials=1000 - avg=5.363 - std=76.05347612699912 - min=2 - max=2405
```

##### BigInteger
```bash
dotnet run

Iteration(100000) <BigInteger> (ticks)
trials=1000 - avg=2503169.817 - std=1036866.4614311559 - min=1230671 - max=5125936
FD(100000) <BigInteger> (ticks)
trials=1000 - avg=11635.472 - std=1352.9710873540496 - min=10651 - max=20619
FD Iteration(100000) <BigInteger> (ticks)
trials=1000 - avg=11425.559 - std=826.2267004394132 - min=10663 - max=17409
FD Tail Recursion(100000) <BigInteger> (ticks)
trials=1000 - avg=11331.992 - std=718.4570564313501 - min=10694 - max=17592
```

## Python results (absolutely no TCO)
Python numbers can hold an arbitrary number of digits, so we don't have to worry about overflow! However, python doesn't encourage recursion and support Tail Call Optimizations (which makes sense). Maybe other Python interpreters do/will. 

##### Performance
```bash
python time_fib.py

Calculating speed for fib(100000) - 1000 times
fib_iter(100000) => 113.6272065
fib_fd_iter(100000) => 4.4318095
fib_fd_tr(100000) => 3.607529299999996
```

# Recreating Results
Go to the corresponding subdirectory you want to go to and run the commands:

```bash
dotnet run
```

```bash
python time_fib.py -n 100000 -r 10
```

```bash
gcc -Wall -std=c11 -O3 -o compare_O3.out fibonacci.c time_fib.c -lm
gcc -Wall -std=c11 -O2 -o compare_O2.out fibonacci.c time_fib.c -lm
gcc -Wall -std=c11 -o compare_no_opt.out fibonacci.c time_fib.c -lm
```

### Examining Assemblies 
If you want to examine the assemblies of the C programs:

```bash
gcc -Wall -std=c11 -O3 -S -o fib_compiled_O3.s fibonacci.c -lm
gcc -Wall -std=c11 -O2 -S -o fib_compiled_O2.s fibonacci.c -lm
gcc -Wall -std=c11 -S -o fib_compiled_no_opt.s fibonacci.c -lm
```

### Why? 
As a programming exercise, I like to implement the iterative and tail recursive versions of recursive functions (even if the language doesn't support [Tail Call Optimizations](https://en.wikipedia.org/wiki/Tail_call)). 

## Design Notes

### C
Initially I was planning to implement the classic version of the FD method with a tuple-like struct. This method worked completely fine, but whenever I compiled the program with optimization flags, the output would returns 0 for all N (I'm not a C expert, but I believe it has something to do with undefined behavior). As a result I just excluded it from the comparisons. 

```c
typedef struct fibtuple{
	uint64_t t0;
	uint64_t t1;
} FibTuple;

void AssignFibTuple(FibTuple *ft, uint64_t t0, uint64_t t1){
	ft->t0 = t0;
	ft->t1 = t1;
	return;
}

FibTuple fibb_fd(int n){
	FibTuple f1; 
	if(n == 0){
		return newFT(0, 1);
	}else{
		FibTuple f1 = fibb_fd(floor(n / 2));
		uint64_t c = f1.t0 * (f1.t1 * 2 - f1.t0);
		uint64_t d = (f1.t0 * f1.t0) + (f1.t1 * f1.t1);
		if(n % 2 == 0){
			AssignFibTuple(&f1, c, d);
		}else{
			AssignFibTuple(&f1, d, c+d);
		}
	}
	return f1;
}
```

### C#
If you noticed - FibonacciBigInt.cs and Fibonnaci.cs are almost identical - meaning that the code could probably refactored somewhere. The code would be more concise if I could use `c#` generic typing for our return types, and cast the outputs to the generics. The dream would look something like this:

```cs
// This is NOT valid code
public T FibTR<T>(int n, T n0, T n1) where T: ulong, BigInteger{
    if(n == 0){
        return (T) (0);
    }
    return FibTR<T>(n-1, n1, n1 + n0);
}
```

Unfortunately, there is no "ergonomic" discriminated unions for c#. Furthermore trying to use `where T: IComparable` won't work as casting `(T) 0` or `(T) 1` won't work for several classes that interface with `IComparable` (but will work for `int`, `long`, `ulong`, `float`, `BigInteger`). The desire for discriminated unions is another reason why `F#` is more suited for this exercise.

