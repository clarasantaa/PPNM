using static System.Console;
using static System.Math;
using System;
using System.Linq;

public class ann{
	int n;
	Func<double,double> f = z => z*Math.Exp(-z*z);
	Func<double,double> fp = z => Math.Exp(-z*z)*(1-2*z*z);
	Func<double,double> fpp = z => Math.Exp(-z*z)*(-6*z+4*z*z*z);
	Func<double,double> F = z => -Math.Exp(-z*z)/2;
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

			p[3*i] = -1+2*i/(n-1);
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

	public double response_derivative(double x){
		double sum=0;
		for(int i=0;i<n;i++){
			double ai=p[3*i];
			double bi=p[3*i+1];
			double wi=p[3*i+2];
			double z=(x-ai)/bi;
			sum+=fp(z)*wi/bi;
		}
		return sum;
	}

	public double response_derivative2(double x){
		double sum=0;
		for(int i=0;i<n;i++){
			double ai=p[3*i];
			double bi=p[3*i+1];
			double wi=p[3*i+2];
			double z=(x-ai)/bi;
			sum+=fpp(z)*wi/(bi*bi);
		}
		return sum;
	}
	public double response_anti(double x){
		double sum=0;
		for(int i=0;i<n;i++){
			double ai=p[3*i];
			double bi=p[3*i+1];
			double wi=p[3*i+2];
			double z=(x-ai)/bi;
			sum+=F(z)*wi*bi;
		}
		return sum;
	}

	private double EvalResponse(vector q, double x){
		double sum=0;
		for(int i=0;i<n;i++){	
			double ai=q[3*i];
                        double bi=q[3*i+1];
                        double wi=q[3*i+2];
                        double z=(x-ai)/bi;
                        sum+=f(z)*wi;
                }
                return sum;
        }

        private double EvalResponse_derivative(vector q, double x){
                double sum=0;
                for(int i=0;i<n;i++){
                        double ai=q[3*i];
                        double bi=q[3*i+1];
                        double wi=q[3*i+2];
                        double z=(x-ai)/bi;
                        sum+=fp(z)*wi/bi;
                }
                return sum;
        }
        private double EvalResponse_derivative2(vector q, double x){
                double sum=0;
                for(int i=0;i<n;i++){
                        double ai=q[3*i];
                        double bi=q[3*i+1];
                        double wi=q[3*i+2];
                        double z=(x-ai)/bi;
                        sum+=fpp(z)*wi/(bi*bi);
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

	public double costODE(vector q, Func<double,double,double,double,double> Phi, double a, double b, double c, double yc, double ycp, double alpha=1.0, double beta=1.0){
		Func<double,double> g = x => {
			double y0=EvalResponse(q,x);
			double y1=EvalResponse_derivative(q,x);
			double y2=EvalResponse_derivative2(q,x);
			double PhiVal=Phi(y2,y1,y0,x);
			return PhiVal*PhiVal;
		};
		double integralTerm;
		integralTerm=program.integrate(g,a,b);
		double yc_net=EvalResponse(q,c);
		double ycp_net=EvalResponse_derivative(q,c);
		double bc1=alpha*(yc_net-yc)*(yc_net-yc);
		double bc2=beta*(ycp_net-ycp)*(ycp_net-ycp);
		return integralTerm+bc1+bc2;
	}

	public void train(vector x, vector y){
		this.xs=x.copy();
		this.ys=y.copy();
		
		WriteLine($"Initial cost: {cost(p):F6}");

		var(p_opt,steps)=Newton.solve_central(cost,p);
		this.p=p_opt.copy();
		WriteLine($"Training completed in {steps} steps. Final cost: {cost(this.p):F6}");
	}

	public void trainODE(Func<double,double,double,double,double> Phi, double a, double b, double c, double yc, double ycp, double alpha=1.0, double beta=1.0){
		Func<vector,double> costPhi = q => costODE(q,Phi,a,b,c,yc,ycp,alpha,beta);
		WriteLine($"Initial cost ODE: {costPhi(this.p):F6}");
		var(p_opt,steps)=Newton.solve_central(costPhi,this.p);
		this.p=p_opt.copy();
		WriteLine($"ODE training completed in {steps} setps. Final cost: {costPhi(this.p):F6}");
	}
}
