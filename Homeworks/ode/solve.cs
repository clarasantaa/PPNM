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
		vector k1=f(x,y);
		vector k2=f(x+h/2,y+k1*(h/2));
		vector k3=f(x+3.0/4*h,y+k2*3.0/4*h);
		vector yh3=y+h*(2.0/9*k1+3.0/9*k2+4.0/9*k3);
		vector yh2=y+h*(7.0/24*k1+1.0/4*k2+1.0/3*k3);
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
			xList.Add(x);
			yList.Add(y);
			hList.Add(h);
			h*=2;
		}while(true);
	}

	public static int binsearch(List<double> x, double z){
		int i=0, j=x.Count-1;
		while (j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid;
			else j=mid;
		}
		return i;
	}

	public static Func<double, vector> make_qspline(List<double> x, List<vector> y){
		int n=x.Count;
		List<vector> b =new List<vector>();
		List<vector> c =new List<vector>();

		for(int i=0;i<n-1;i++){
			double dx=x[i+1]-x[i];
			vector dy=y[i+1]-y[i];
			vector bi=dy/dx;
			b.Add(bi);
		}

		c.Add(new vector(y[0].size));
		for(int i=0;i<n-2;i++){
			double dx1=x[i+1]-x[i];
			double dx2=x[i+2]-x[i+1];
			vector c_next=(b[i+1]-b[i]-c[i]*dx1)/dx2;
			c.Add(c_next);
		}

		Func<double,vector> qspline = delegate(double z){
			int i=binsearch(x,z);
			if(i<0||i>=x.Count-1){
				WriteLine($"Invalid index: {i}, z = {z}, x.Count = {x.Count}");
			}
			double dx=z-x[i];
			return y[i]+dx*(b[i]+c[i]*dx);
		};
		
		return qspline;
	}

	public static Func<double,vector> make_ode_ivp_qspline(Func<double,vector,vector> F, (double,double) interval, vector yinit, double acc=0.01, double eps=0.01, double hstart=0.01){
		var(xList, yList) =driver(F,interval,yinit,hstart,acc,eps);
		List<double> xgen =new List<double>();
		List<vector> ygen =new List<vector>();
		for(int i=0;i<xList.Count;i++){
			xgen.Add(xList[i]);
			ygen.Add(yList[i]);
		}
	
		return make_qspline(xgen,ygen);
	}
	
}
