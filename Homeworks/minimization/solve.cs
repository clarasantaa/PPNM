using static System.Console;
using static System.Math;
using System;

public static class Newton{
	public static (vector, int) solve(Func<vector,double> phi, vector x, double acc=1e-3){
		int n=x.size, c=0;
		do{
			vector g=gradient(phi,x);
			if(g.norm()<acc) break;
			matrix H=hessian(phi,x);
			(matrix Q, matrix R)=QR.decomp(H);
			vector dx=QR.solve(Q,R,-g);
			double lambda=1;
			while(lambda>=1.0/1024){
				if(phi(x+lambda*dx)<phi(x)) break;
				lambda/=2;
			}
			x+=lambda*dx;
			c++;
		}while(true);
		return (x, c);
	}

	public static vector gradient(Func<vector,double> phi, vector x){
		double phix=phi(x);
		vector gphi=new vector(x.size);
		for(int i=0;i<x.size;i++){
			double dxi=(1+Abs(x[i]))*Math.Pow(2,-26);
			x[i]+=dxi;
			gphi[i]=(phi(x)-phix)/dxi;
			x[i]-=dxi;
		}
		return gphi;
	}

	public static matrix hessian(Func<vector,double> phi, vector x){
		matrix H=new matrix(x.size,x.size);
		vector gphix=new vector(x.size);
		gphix=gradient(phi,x);
		for(int j=0;j<x.size;j++){
			double dxj=(1+Abs(x[j]))*Math.Pow(2,-13);
			x[j]+=dxj;
			vector dgphi=new vector(x.size);
			dgphi=gradient(phi,x)-gphix;
			for(int i=0;i<x.size;i++){
				H[i,j]=dgphi[i]/dxj;
			}
			x[j]-=dxj;
		}
		return H;
	}
}


		
