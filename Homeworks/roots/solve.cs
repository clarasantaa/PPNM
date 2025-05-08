using static System.Console;
using static System.Math;
using System;

public class RootFinding{
	public static vector newton(Func<vector,vector> f, vector start, double acc=1e-2, vector dx=null){
		double lambdamin=1.0/128;
		vector x=start.copy();
		int dim=x.size;
		vector fx=f(x);
		vector fz=null;
		vector z=null;
		do{
			if(fx.norm()<acc) break;
			matrix J=jacobian(f,x,fx,dx);
			(matrix Q, matrix R)=QR.decomp(J);
			vector Dx=QR.solve(Q,R,-fx);
			double lambda=1;
			do{
				z=x+lambda*Dx;
				fz=f(z);
				if(fz.norm()<(1-lambda/2)*fx.norm()) break;
				if(lambda<lambdamin) break;
				lambda/=2;
			}while(true);
			x=z;
			fx=fz;
		}while(true);
		return x;
	}

	public static matrix jacobian(Func<vector,vector> f, vector x, vector fx=null, vector dx=null){
		if(dx==null) dx = x.map(xi => Max(Math.Abs(xi),1)*Pow(2,-26));
		if(fx==null) fx = f(x);
		int m=fx.size, n=x.size;
		matrix J=new matrix(m,n);
		for(int j=0;j<n;j++){
			double aux=x[j];
			x[j]+=dx[j];
			vector df=f(x)-fx;
			for(int i=0;i<m;i++){
				J[i,j]=df[i]/dx[j];
			}
			x[j]=aux;
		}
		return J;
	}
}

public class Hydrogen{
	public static vector F(double r, vector y, double E){
		double f=y[0],fp=y[1];
		double fpp=-2*(E+1.0/r)*f;
		return new vector(new double[] {fp,fpp});
	}

	public static double M(double E, double rmin, double rmax){
		vector y0=new vector(new double[] {rmin-rmin*rmin,1-2*rmin});
		Func<double, vector, vector> rhs = (r,y) => Hydrogen.F(r,y,E);
		var (rList, yList)=ODESolver.driver(rhs,(rmin,rmax),y0);
		return yList[yList.Count-1][0]; //the last element of the list
	}

	public static double FindGroundState(double rmin, double rmax){
		double a=-1.0, b=-0.1;
		double fa=M(a,rmin,rmax), fb=M(b,rmin,rmax);
		if(fa*fb>0){
			WriteLine($"There is no root in the interval");
			return 1.0;
		}
		for(int i=0;i<50;i++){
			double mid=(a+b)/2;
			double fmid=M(mid,rmin,rmax);
			if(Math.Abs(fmid)<1e-6) return mid;
			if(fa*fmid<0){
				b=mid;
				fb=fmid;
			}else{
				a=mid;
				fa=fmid;
			}
		}
		return (a+b)/2;
	}
}

