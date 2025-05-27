using static System.Console;
using static System.Math;
using System;

public class ann{
	int n;
	Func<double,double> f = z => z*Math.Exp(-z*z);
	Func<double,double> fp = z => Math.Exp(-z*z)*(1-2*z*z);
	public vector p;
	vector xs, ys;

	public ann(int n){
		this.n=n;
		p=new vector(3*n);
		
		var rnd =new Random();
		
		for(int i=0;i<n;i++){
			p[3*i]=-1+2*rnd.NextDouble();
			p[3*i+1]=Math.Log(0.3)+0.1*(rnd.NextDouble()-0.5);;
			p[3*i+2]=0.01*(rnd.NextDouble()-0.5);
		}
	}

	public double response(double x){
		double sum=0;
		for(int i=0;i<n;i++){
			double ai=p[3*i];
			double bi=Exp(p[3*i+1]);
			double wi=p[3*i+2];
			double z=(x-ai)/bi;
			sum+=f(z)*wi;
		}
		return sum;
	}

	public double cost(vector q){
		p=q.copy();
		double sum=0;
			
		for(int k=0;k<xs.size;k++){
			double resp=response(xs[k]);
			if(Double.IsNaN(resp) || Double.IsInfinity(resp)) return 1e10;
			double d=resp-ys[k];
			sum+=d*d;
		}
		return sum;
	}
	
	public vector gradient(vector q){
		p=q.copy();
		vector g=new vector(3*n);
		for(int k=0;k<xs.size;k++){
			double x=xs[k], y=ys[k];
			double err=response(x)-y;
			for(int i=0;i<n;i++){
				double ai=q[3*i];
				double bi=Exp(p[3*i+1]);
				double wi=q[3*i+2];
				double z=(x-ai)/bi;
				double fi=f(z);
				double fpi=fp(z);
				g[3*i+2]+=2*err*fi; //∂/∂wi
				g[3*i]+=-2*err*wi*fpi*(1.0/bi); //∂/∂ai
				g[3*i+1]+=-2*err*wi*fpi*z; //∂/∂bi_log
			}
		}
		return g;
	}

	public void train(vector x, vector y){
		this.xs=x.copy();
		this.ys=y.copy();
		
		var rnd=new Random();
		do{
			for(int i=0;i<n;i++){
				p[3*i]=-1+2*rnd.NextDouble();
				p[3*i+1]=Math.Log(0.3)+0.1*(rnd.NextDouble()-0.5);
				p[3*i+2]=0.01*(rnd.NextDouble()-0.5);
			}
		}while(cost(p)>1e3);

		var(p_opt,steps)=Newton.solve(cost,p,gradient);
		p=p_opt.copy();
		WriteLine($"Training completed in {steps} steps");
	}
}
