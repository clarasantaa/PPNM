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

	public static (vector, vector) rkstep23(Func<double,vector,vector> f, double x, vector y, double h){
		vector k0=f(x,y);
		vector k1=f(x+h/2,y+k0*(h/2));
		vector k2=f(x+3.0/4*h,k1*3.0/4*h);
		vector k3=f(x+h,y+h*(2.0/9*k0+3.0/9*k1+4.0/9*k2));
		vector yh3=y+h*(2.0/9*k0+3.0/9*k1+4.0/9*k2);
		vector yh2=y+h*(7.0/24*k0+1.0/4*k1+1.0/3*k2+1.0/8*k3);
		vector dy=yh3-yh2;
		return (yh3,dy);
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

	public static (List<double>, List<vector>, List<double>) driver23(Func<double,vector,vector> F, (double, double) interval, vector yinit, double h=0.1){
		var (a,b)=interval;
		double x=a;
		vector y=yinit.copy();

		List<double> xList =new List<double>();
		List<vector> yList =new List<vector>();
		List<double> hList =new List<double>();

		xList.Add(x);
		yList.Add(y);
		hList.Add(h);

		do{
			if(x>=b){
				return(xList,yList,hList);
			}
			if(x+h>b){
				h=b-x;
			}
			var (yh3,dy)=rkstep23(F,x,y,h);
			/*acc=eps=0 always*/
			x=x+h;
			y=yh3;
			h*=2;
			xList.Add(x);
			yList.Add(y);
			hList.Add(h);
		}while(true);
	}

}
