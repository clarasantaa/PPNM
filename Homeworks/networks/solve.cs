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
			p[3*i+1]=0.1+0.5*rnd.NextDouble();
			p[3*i+2]=2*(rnd.NextDouble()-0.5);
		}
	}

	public double response(double x){
		double sum=0;
		for(int i=0;i<n;i++){
			double ai=p[3*i];
			double bi=p[3*i+1];
			double wi=p[3*i+2];
			if(Abs(bi)<1e-8) bi=1e-8;
			double z=(x-ai)/bi;
			sum+=f(z)*wi;
		}
		return sum;
	}

	public double cost(vector q){
		vector p_original=p.copy();
		p=q.copy();
		double sum=0;
			
		for(int k=0;k<xs.size;k++){
			double resp=response(xs[k]);
			if(Double.IsNaN(resp) || Double.IsInfinity(resp)){
			       	p=p_original;
				return 1e10;
			}
			double d=resp-ys[k];
			sum+=d*d;
		}
		p=p_original;
		return sum;
	}
	
	public vector gradient(vector q){
		vector p_original=p.copy();
		p=q.copy();
		vector g=new vector(3*n);
		for(int k=0;k<xs.size;k++){
			double x=xs[k], y=ys[k];
			double err=response(x)-y;
			for(int i=0;i<n;i++){
				double ai=q[3*i];
				double bi=p[3*i+1];
				double wi=q[3*i+2];
				if(Abs(bi)<1e-8) bi=1e-8;
				double z=(x-ai)/bi;
				double fi=f(z);
				double fpi=fp(z);
				g[3*i+2]+=2*err*fi; //∂C/∂wi
				g[3*i]+=-2*err*wi*fpi*(1.0/bi); //∂C/∂ai
				g[3*i+1]+=-2*err*wi*fpi*(-z/bi); //∂C/∂bi
			}
		}
		p=p_original.copy();
		return g;
	}

	public void train(vector x, vector y){
		this.xs=x.copy();
		this.ys=y.copy();
		
		WriteLine($"Initial cost: {cost(p):F6}");
		double best_cost=Double.MaxValue;
		vector best_p=null;

		var rnd=new Random();
		for(int trial=0;trial<10;trial++){
			for(int i=0;i<n;i++){
				p[3*i]=-1+2*rnd.NextDouble();
				p[3*i+1]=0.1+0.5*rnd.NextDouble();
				p[3*i+2]=2*(rnd.NextDouble()-0.5);
			}
			double current_cost=cost(p);
			if(current_cost<best_cost && current_cost<100){
				best_cost=current_cost;
				best_p=p.copy();
			}
		}

		if(best_p!=null){
			p=best_p;
			WriteLine($"Best initial cost fount: {best_cost:F6}");
		}else{
			WriteLine($"Warning: ALl initialization had high cost");
		}

		var(p_opt,steps)=Newton.solve(cost,p,gradient);
		p=p_opt.copy();
		WriteLine($"Training completed in {steps} steps");
	}
}
