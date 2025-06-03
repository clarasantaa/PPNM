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
		var net=new ann(hidden);
		WriteLine($"Training network...");
		net.train(xs,ys);

		WriteLine("\nComparing network vs true function:");
		WriteLine($"x\t\tTrue\t\tNework\t\tError");
		double total_error=0;
		int test_points=0;
		for(double x=-1;x<=1;x+=0.2){
			double trueV=Cos(5*x-1)*Exp(-x*x);
			double predV=net.response(x);
			double error=Abs(trueV-predV);
			total_error+=error;
			test_points++;
			WriteLine($"{x:F1}\t\t{trueV:F3}\t\t{predV:F3}\t\t{error:F3}");
		}
		WriteLine($"\nAverage error: {total_error/test_points:F6}");
		using(var File =new StreamWriter("outexA.dat")){
			for(double x=-1;x<=1;x+=2.0/120){
				File.WriteLine($"{x} {net.response(x)}");
			}
		}
		return 0;
	}
}
