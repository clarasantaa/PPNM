using static System.Console;
using static System.Math;
using System;

class main{
	static int Main(){
		
		/*EXERCISE A*/
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
		WriteLine($"\n");

		/*EXERCISE B*/
		double ncalls=0;
		I=program.integrate(x => {ncalls++; return 1/Sqrt(x);},0,1);
		WriteLine($"1/sqrt(x) [0,1] ordinary : {I}  evaluations : {ncalls}");
		ncalls=0;
		I=program.integrateCC(x => {ncalls++; return 1/Sqrt(x);},0,1);
		WriteLine($"1/sqrt(x) [0,1] Clenshaw-Curtis : {I}  evaluations : {ncalls}");
		ncalls=0;
		I=program.integrate(x => {ncalls++; return Log(x)/Sqrt(x);},0,1);
		WriteLine($"log(x)/sqrt(x) [0,1] ordinary : {I}  evaluations : {ncalls}");
		ncalls=0;
		I=program.integrateCC(x => {ncalls++; return Log(x)/Sqrt(x);},0,1);
		WriteLine($"log(x)/sqrt(x) [0,1] Clenshaw-Curtis : {I}  evaluations : {ncalls}");
		ncalls=0;
		I=program.integrateGeneral(x => {ncalls++; return Exp(-x*x);},0,double.PositiveInfinity,1e-6,0);
		WriteLine($"exp(-x²) [0,∞) : {I}  evaluations : {ncalls}");
		WriteLine($"\n");		

		/*EXERCISE C*/
		var (I1, err1)=program.integratewitherr(x => Sqrt(x),0,1);
                WriteLine($"sqrt(x) [0,1] : {I1} ± {err1}");
                var (I2, err2) = program.integratewitherr(x => 1/Sqrt(x),0,1);
                WriteLine($"1/sqrt(x) [0,1] : {I2} ± {err2}");
                var (I3, err3) = program.integratewitherr(x => Sqrt(1-x*x),0,1);
                WriteLine($"sqrt(1-x²) [0,1] : {I3} ± {err3}");
                var (I4, err4) = program.integratewitherr(x => Log(x)/Sqrt(x),0,1);
                WriteLine($"log(x)/sqrt(x) [0,1] : {I4} ± {err4}");

		WriteLine($"\n");
		WriteLine("Function\tEstimated Error\tReal Error");
		WriteLine($"sqrt(x)\t\t{err1:E6}\t{Abs(I1 - 2.0/3):E6}");
		WriteLine($"1/sqrt(x)\t{err2:E6}\t{Abs(I2 - 2.0):E6}");
		WriteLine($"sqrt(1-x²)\t{err3:E6}\t{Abs(I3 - PI / 4):E6}");
		WriteLine($"log(x)/sqrt(x)\t{err4:E6}\t{Abs(I4 + 4):E6}");

		return 0;
	}
}

