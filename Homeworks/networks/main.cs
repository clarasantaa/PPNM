using static System.Console;
using static System.Math;
using System.IO;
using System;

class main{
	static int Main(){
		int N=300;
		vector xs=new vector(N);
		vector ys=new vector(N);
		for(int k=0;k<N;k++){
			double x=-1+2*k/(double)(N-1);
			xs[k]=x;
			ys[k]=Cos(5*x-1)*Exp(-x*x);
		}

		int hidden=3;
		WriteLine($"Creating neural network with {hidden} hidden neurons...");
		var netA=new ann(hidden);
		WriteLine($"Training network...");
		netA.train(xs,ys);

		/*EXERCISE A*/

		WriteLine("\nFunction comparison:");
		WriteLine($"x\t\tTrue\t\tNework\t\tError");
		double total_error=0;
		int test_points=0;
		for(double x=-1;x<=1;x+=0.2){
			double trueV=Cos(5*x-1)*Exp(-x*x);
			double predV=netA.response(x);
			double error=Abs(trueV-predV);
			total_error+=error;
			test_points++;
			WriteLine($"{x:F1}\t\t{trueV:F3}\t\t{predV:F3}\t\t{error:F3}");
		}
		WriteLine($"\nAverage error: {total_error/test_points:F6}\n");
		using(var File =new StreamWriter("outexA.dat")){
			for(double x=-1;x<=1;x+=2.0/120){
				File.WriteLine($"{x} {netA.response(x)}");
			}
		}

		/*EXERCISE B*/
		//we will compare the function g(x) = x³

		for(int k=0;k<N;k++){
			double x=-1+2*k/(double)(N-1);
			xs[k]=x;
			ys[k]=x*x*x;
		}
		
		var netB=new ann(hidden);
		netB.train(xs,ys);
		double C = netB.response_anti(-1.0);

		WriteLine("\n\n\nFunction comparison:");
		WriteLine($"x\t\tTrue\t\tNework\t\tError");
		total_error=0;
		test_points=0;
		for(double x=-1;x<=1;x+=0.2){
			double trueV=x*x*x;
			double predV=netB.response(x);
			double error=Abs(trueV-predV);
			total_error+=error;
			test_points++;
			WriteLine($"{x:F1}\t\t{trueV:F3}\t\t{predV:F3}\t\t{error:F3}");
		}
		WriteLine($"\nAverage error: {total_error/test_points:F6}");
		using(var File =new StreamWriter("outfuncionB.dat")){
			for(double x=-1;x<=1;x+=2.0/120){
				File.WriteLine($"{x} {netB.response(x)}");
			}
		}
		
		WriteLine("\n\nDerivative comparison:");
		WriteLine($"x\t\tTrue\t\tNework\t\tError");
		total_error=0;
		test_points=0;
		for(double x=-1;x<=1;x+=0.2){
			double trueV=3*x*x;
			double predV=netB.response_derivative(x);
			double error=Abs(trueV-predV);
			total_error+=error;
			test_points++;
			WriteLine($"{x:F1}\t\t{trueV:F3}\t\t{predV:F3}\t\t{error:F3}");
		}
		WriteLine($"\nAverage error: {total_error/test_points:F6}");
		using(var File =new StreamWriter("outderivativeB.dat")){
			for(double x=-1;x<=1;x+=2.0/120){
				File.WriteLine($"{x} {netB.response_derivative(x)}");
			}
		}
		
		WriteLine("\n\nSecond Derivative comparison:");
		WriteLine($"x\t\tTrue\t\tNework\t\tError");
		total_error=0;
		test_points=0;
		for(double x=-1;x<=1;x+=0.2){
			double trueV=6*x;
			double predV=netB.response_derivative2(x);
			double error=Abs(trueV-predV);
			total_error+=error;
			test_points++;
			WriteLine($"{x:F1}\t\t{trueV:F3}\t\t{predV:F3}\t\t{error:F3}");
		}
		WriteLine($"\nAverage error: {total_error/test_points:F6}");
		using(var File =new StreamWriter("outsecondB.dat")){
			for(double x=-1;x<=1;x+=2.0/120){
				File.WriteLine($"{x} {netB.response_derivative2(x)}");
			}
		}

		WriteLine("\n\nAnti-derivative comparison:");
		WriteLine($"x\t\tTrue\t\tNework\t\tError");
		total_error=0;
		test_points=0;
		for(double x=-1;x<=1;x+=0.2){
			double trueV=(x*x*x*x-1.0)/4;
			double predV=netB.response_anti(x)-C;
			double error=Abs(trueV-predV);
			total_error+=error;
			test_points++;
			WriteLine($"{x:F1}\t\t{trueV:F3}\t\t{predV:F3}\t\t{error:F3}");
		}
		WriteLine($"\nAverage error: {total_error/test_points:F6}");
		using(var File =new StreamWriter("outantiB.dat")){
			for(double x=-1;x<=1;x+=2.0/120){
				File.WriteLine($"{x} {netB.response_anti(x)-C}");
			}
		}

		return 0;
	}
}
