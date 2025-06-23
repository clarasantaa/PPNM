using static System.Console;
using static System.Math;
using System.Diagnostics;
using System.Numerics;
using System;

class Program{
	static int Main(string[] args){
		int N=1024, reps=5; /*default*/
		for(int i=0;i+1<args.Length;i++){
			if(args[i]=="-size"){
				if(int.TryParse(args[i+1],out int v)) N=v;
				else{
					Console.Error.WriteLine($"Error: invalid value for -size: {args[i+1]}");
					return 1;
				}
			}
		}
		
		if((N&(N-1))!=0){
			Console.Error.WriteLine($"Warning: N={N} is not a power of 2. DFT will be used");
		}

		Complex[] baseArray=new Complex[N];
		var rnd=new Random(123);
		for(int i=0;i<N;i++){
			baseArray[i]=new Complex(rnd.NextDouble(),0);
		} /*we create the array outside the stopwatch*/

		Stopwatch sw = Stopwatch.StartNew();
		for(int i=0;i<reps;i++){
			var input=new Complex[N];
			Array.Copy(baseArray,input,N);
			matlib.fft(input);
		}
		sw.Stop();
		double avg=sw.Elapsed.TotalSeconds/reps;
		
		Console.WriteLine($"{N} {avg:E6}");
		
		return 0;
	}
}
