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
		WriteLine($"x\t\tTrue\t\tNetwork\t\tError");
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
		
		var netB=new ann(5);
		netB.train(xs,ys);
		double C = netB.response_anti(-1.0);

		WriteLine("\n\n\nFunction comparison:");
		WriteLine($"x\t\tTrue\t\tNetwork\t\tError");
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
		WriteLine($"x\t\tTrue\t\tNetwork\t\tError");
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
		WriteLine($"x\t\tTrue\t\tNetwork\t\tError");
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
		WriteLine($"x\t\tTrue\t\tNetwork\t\tError");
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
		WriteLine($"\nAverage error: {total_error/test_points:F6}\n");
		using(var File =new StreamWriter("outantiB.dat")){
			for(double x=-1;x<=1;x+=2.0/120){
				File.WriteLine($"{x} {netB.response_anti(x)-C}");
			}
		}

		/*EXERCISE C*/
		//y''(x)-2*x*y'(x)+2*y(x)=0 in [-1,1] with y(0)=1, y'(0)=0
		//It has the soluction exp(-x²)
		Func<double,double,double,double,double> phi = (y2,y1,y0,x) => {
			return y2+2*x*y1+2*y0;
		};

		double a=-1.0, b=1.0, c=0.0, yc=1, ycp=0;
		var netODE =new ann(12);
		netODE.trainODE(phi,a,b,c,yc,ycp);

		//The associated first order ODE is y0'=y1 , y1'=-2*x*y1-2*y0 with y0=y, y1=y'
		Func<double,vector,vector> sys = (x, yvec) => {
			vector dy=new vector(2);
			dy[0]=yvec[1];
			dy[1]=-2*x*yvec[1]-2*yvec[0];
			return dy;
		};
		vector yinit=new vector(new double[] {Exp(-a*a),-2*a*Exp(-a*a)});
		var (xlist, ylist) = ODESolver.driver(sys, (a,b), yinit);
		WriteLine($"\nODE comparison");
		WriteLine($"x\t\tRK\t\tNetwork\t\tReal\t\tError RK\tError ANN");
		double total_errorODE=0;
		double total_errorANN=0;
		test_points=0;
		for(int i=0;i<xlist.Count;i++){
			double x=xlist[i];
			double y_ref=ylist[i][0];
			double y_ann=netODE.response(x);
			double y_true=Exp(-x*x);
			double errODE=Abs(y_ref-y_true);
			double errANN=Abs(y_ann-y_true);
			total_errorODE+=errODE;
			total_errorANN+=errANN;
			test_points++;
			WriteLine($"{x:F3}\t\t{y_ref:F6}\t{y_ann:F6}\t{y_true:F6}\t{errODE:F6}\t{errANN:F6}");
		}
		WriteLine($"\nAverage error ODE: {total_errorODE/test_points:F6}");
		WriteLine($"\nAvarage error ANN: {total_errorANN/test_points:F6}\n");
		using(var File =new StreamWriter("outexC.dat")){
			for(int i=0;i<xlist.Count;i++){
				File.WriteLine($"{xlist[i]} {ylist[i][0]} {netODE.response(xlist[i])}");
			}
		}
		
		return 0;
	}
}
