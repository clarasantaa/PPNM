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
			if(n==1){
				p[3*i]=0.0;
			}else{

			p[3*i]   =-1+2*i/(n-1);
			}
            		p[3*i+1] = 0.5+rnd.NextDouble();
            		p[3*i+2] = 1.0;
		}
	}

	public double response(double x){
		double sum=0;
		for(int i=0;i<n;i++){
			double ai=p[3*i];
			double bi=p[3*i+1];
			double wi=p[3*i+2];
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
				double zq=(xk-aiq)/biq;
				current_response+=f(zq)*wiq;
			}
			double d=current_response-yk;
			sum+=d*d;
		}
		return sum;
	}

	public void train(vector x, vector y){
		this.xs=x.copy();
		this.ys=y.copy();
		
		WriteLine($"Initial cost: {cost(p):F6}");

		var(p_opt,steps)=Newton.solve_central(cost,p);
		this.p=p_opt.copy();
		WriteLine($"Training completed in {steps} steps. Final cost: {cost(this.p):F6}");
	}
}
