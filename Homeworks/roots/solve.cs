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
