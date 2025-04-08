using static System.Console;
using static System.Math;
using System;

class main{
	static int Main(){
		double I = program.integrate(x => Sqrt(x),0,1);
		WriteLine($"sqrt(x) [0,1] : {I}");
		I = program.integrate(x => 1/Sqrt(x),0,1);
		WriteLine($"1/sqrt(x) [0,1] : {I}");
		I = program.integrate(x => Sqrt(1-x*x),0,1);
		WriteLine($"sqrt(1-x²) [0,1] : {I}");
		I = program.integrate(x => Log(x)/Sqrt(x),0,1);
		WriteLine($"log(x)/sqrt(x) [0,1] : {I}");

		double z=1.0;
		double erfValue=program.erf(z,0.001,0.001);
		WriteLine($"Erf({z}) = {erfValue}\n\n");

		double eps=0;
		for(int i=1;i<=10;i++){
			double acc=Pow(10,-i);
			double result=program.erf(z,acc,eps);
			double err=Abs(result-erfValue);
			WriteLine($"{acc}\t{err}");
		}
		return 0;
	}
}

