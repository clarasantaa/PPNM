using static System.Console;
using static System.Math;
using System;

class main{
	static int Main(){
		int N=100;
		vector xs=new vector(N);
		vector ys=new vector(N);
		for(int k=0;k<N;k++){
			double x=-1+2*k/(double)(N-1);
			xs[k]=x;
			ys[k]=Cos(5*x-1)*Exp(-x*x);
		}

		int hidden=8;
		var net=new ann(hidden);
		net.train(xs,ys);

		WriteLine("\nComparing network vs true function:");
		for(double x=-1;x<=1;x+=0.2){
			double trueV=Cos(5*x-1)*Exp(-x*x);
			double predV=net.response(x);
			WriteLine($"{x:0.###}\ttrue = {trueV:0.###}\tnet = {predV:0.###}");
		}
		return 0;
	}
}
