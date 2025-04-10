using static System.Console;
using System.Collections.Generic;
using static System.Math;
using System;

public class ODESolver{
	public static (vector, vector) rkstep12(Func<double,vector,vector> f, double x, vector y, double h){
		vector k0=f(x,y);
		vector k1=f(x+h/2,y+k0*(h/2));
		vector yh=y+k1*h;
		vector dy=(k1-k0)*h;
		return (yh,dy);
	}

	public static (List<double>, List<vector>) driver(Func<double,vector,vector> F, (double,double) interval, vector yinit, double h=0.125, double acc=0.01, double eps=0.01){
		var (a,b)=interval;
		double x=a;
		vector y=yinit.copy();

		List<double> xList =new List<double>();
		List<vector> yList =new List<vector>();

		xList.Add(x);
		yList.Add(y);

		do{
			if(x>=b){
				return (xList, yList);
			}
			if(x+h>b){
				h=b-x;
			}
			var (yh,dy)=rkstep12(F,x,y,h);
			double tol=(acc+eps*yh.norm())*Sqrt(h/(b-a));
			double err=dy.norm();
			if(err<=tol){
				x=x+h;
				y=yh;
				xList.Add(x);
				yList.Add(y);
			}
			if(err>0){
				h*=Min(Pow(tol/err,0.25)*0.95,2);
			}else{
				h*=2;
			}
		}while(true);
	}
}
