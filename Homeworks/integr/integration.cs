using static System.Console;
using static System.Math;
using System;

public class program{
	public static double integrate(Func<double,double> f, double a, double b, double acc=0.001, double eps=0.001, double f2=double.NaN, double f3=double.NaN){
		double h=b-a;
		if(double.IsNaN(f2)){
			f2=f(a+2*h/6);
			f3=f(a+4*h/6);
		}
		double f1=f(a+h/6);
		double f4=f(a+5*h/6);
		double Q=(2*f1+f2+f3+2*f4)/6*(b-a);
		double q=(f1+f2+f3+f4)/4*(b-a);
		double err=Math.Abs(Q-q);
		if(err<=acc+eps*Math.Abs(Q)){
			return Q;
		}else{
			double mid=(a+b)/2;
			return integrate(f,a,mid,acc/Math.Sqrt(2),eps,f1,f2)+integrate(f,mid,b,acc/Math.Sqrt(2),eps,f3,f4);
		}
	}

	public static double erf(double z, double acc, double eps){
		if(z<0){
			return -erf(-z,acc,eps);
		}else if(z<=1){
			double integral = integrate(x => Exp(-x*x),0,z,acc,eps);
			return (2/Sqrt(PI))*integral;
		}else{
			double integral = integrate(t => Exp(-Pow(z+(1-t)/t,2))/t/t,0,1,acc,eps);
			return 1-(2/Sqrt(PI))*integral;
		}
	}

}


