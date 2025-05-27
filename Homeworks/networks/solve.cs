using static System.Console;
using static System.Math;
using System;
using System.Linq;

public class ann{
	int n;
	Func<double,double> f = z => z*Math.Exp(-z*z);
	Func<double,double> fp = z => Math.Exp(-z*z)*(1-2*z*z);
	public vector p;
	private	vector xs;
	private vector ys;

	public ann(int n){
		this.n=n;
		p=new vector(3*n);
		
		var rnd =new Random();
		
		for(int i=0;i<n;i++){
			p[3*i]   =-1+2*rnd.NextDouble(); 
            		p[3*i+1] = 0.5+0.5*rnd.NextDouble();
            		p[3*i+2] = 2.0*(rnd.NextDouble()-0.5);
		}
	}

	public double response(double x){
		double sum=0;
		for(int i=0;i<n;i++){
			double ai=p[3*i];
			double bi=p[3*i+1];
			double wi=p[3*i+2];
			if(Abs(bi)<1e-8) bi=Sign(bi)*1e-8;
			double z=(x-ai)/bi;
			sum+=f(z)*wi;
		}
		return sum;
	}

	public double cost(vector q){
		double sum=0;
			
		for(int k=0;k<xs.size;k++){
			double xk=xs[k], yk=ys[k];
			double current_response=0;
			for(int i=0;i<n;i++){
				double aiq=q[3*i], biq=q[3*i+1], wiq=q[3*i+2];
				if(Abs(biq)<1e-8) biq=Sign(biq)*1e-8;
				double zq=(xk-aiq)/biq;
				current_response+=f(zq)*wiq;
			}
			if(Double.IsNaN(current_response)||Double.IsInfinity(current_response)) return 1e100;
			double d=current_response-yk;
			sum+=d*d;
		}
		return sum;
	}

	public vector gradient(vector q){
		vector g=new vector(3*n);
		for(int k=0;k<xs.size;k++){
			double x=xs[k], y=ys[k];
			double current_response=0;
			for(int i=0;i<n;i++){
				double ai=q[3*i];
				double bi=q[3*i+1];
				double wi=q[3*i+2];
				if(Abs(bi)<1e-8) bi=Sign(bi)*1e-8;
				double z=(x-ai)/bi;
				current_response+=f(z)*wi;
			}
			double err=current_response-y;
			for(int i=0;i<n;i++){
				double ai=q[3*i];
				double bi=q[3*i+1];
				double wi=q[3*i+2];
				if(Abs(bi)<1e-8) bi=Sign(bi)*1e-8;
				double z=(x-ai)/bi;
				double fi=f(z);
				double fpi=fp(z);
				if (double.IsNaN(fi) || double.IsInfinity(fi) || double.IsNaN(fpi) || double.IsInfinity(fpi)) {
    					return new vector(3*n, double.NaN);
				}
				g[3*i+2]+=2*err*fi; //∂C/∂wi
				g[3*i]+=-2*err*wi*fpi*(1.0/bi); //∂C/∂ai
				g[3*i+1]+=-2*err*wi*fpi*(z/bi); //∂C/∂bi
			}
		}
		return g;
	}

	public void train(vector x, vector y){
		this.xs=x.copy();
		this.ys=y.copy();
		
		WriteLine($"Initial cost: {cost(p):F6}");
		double best_cost=Double.MaxValue;
		vector best_p=null;

		var rnd=new Random();
		for(int trial=0;trial<1000;trial++){
			for(int i=0;i<n;i++){
				p[3*i]   =-1+2*rnd.NextDouble(); 
            			p[3*i+1] = 0.5+0.5*rnd.NextDouble();
            			p[3*i+2] = 2.0*(rnd.NextDouble()-0.5);
			}
			double current_cost=cost(p);
			if(current_cost<best_cost){
				best_cost=current_cost;
				best_p=p.copy();
			}
			if(current_cost<100){
				WriteLine($"Found good inital parameters at trial {trial+1}. Initial cost: {current_cost:F6}");
				break;
			}
			if(trial==1000-1){
				WriteLine($"Warning: Could not find initial parameters");
			}
		}

		if(best_p!=null){
			p=best_p;
			WriteLine($"Best initial cost fount: {best_cost:F6}");
		}

		var(p_opt,steps)=Newton.solve(cost,p,gradient);
		this.p=p_opt.copy();
		WriteLine($"Training completed in {steps} steps. Final cost: {cost(this.p):F6}");
	}
}
