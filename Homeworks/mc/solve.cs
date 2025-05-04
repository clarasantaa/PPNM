using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System;

public class MC{
	public static (double,double) plainmc(Func<vector,double> f, vector a, vector b, int N){

		int dim=a.size;
		double V=1, fx, sum=0, sum2=0;
		for(int i=0;i<dim;i++){
			V*=b[i]-a[i];
		}

		var x=new vector(dim);
		var rnd=new Random();
		for(int i=0;i<N;i++){
			for(int k=0;k<dim;k++){
				x[k]=a[k]+rnd.NextDouble()*(b[k]-a[k]);
			}
			fx=f(x);
			sum+=fx;
			sum2+=fx*fx;
		}
		double mean=sum/N, sigma=Sqrt(sum2/N-mean*mean);
		var result=(mean*V,sigma*V/Sqrt(N));
		return result;
	}

	public static (double,double) quasimc(Func<vector,double> f, vector a, vector b, int N){
		int dim=a.size;
		double V=1;
		for(int i=0;i<dim;i++){
			V*=b[i]-a[i];
		}

		var h1=new Halton(dim);
		var h2=new Halton(dim+11);
		double sum1=0, sum2=0;

		for(int i=0;i<N;i++){
			var x1=h1.get(i);
			var x2=h2.get(i);
			for(int k=0;k<dim;k++){
				x1[k]=a[k]+x1[k]*(b[k]-a[k]);
				x2[k]=a[k]+x2[k]*(b[k]-a[k]);
			}
			sum1+=f(x1);
			sum2+=f(x2);
		}

		double mean=(sum1+sum2)/(2.0*N);
		double sigma=Math.Abs(sum1-sum2)/(2.0*N);
		return (mean*V,sigma*V/Math.Sqrt(N));
	}

	public static (double, double) stratifiedmc(Func<vector,double> f,vector a, vector b, double acc = 1e-3, double eps = 1e-3, int n_reuse =0, double mean_reuse =0){
		int dim=a.size;
		int N=16*dim;
		double V=1;
		for(int i=0;i<dim;i++){
			V*=b[i]-a[i];
		}
		//Nmin = 32
		if(N<32){
			return plainmc(f,a,b,N);
		}

		int[] nLeft =new int[dim];
		int[] nRight =new int[dim];
		vector x =new vector(dim);
		vector meanLeft =new vector(dim);
		vector meanRight =new vector(dim);
		double mean=0;

		var rnd =new Random();

		for(int i=0;i<N;i++){
			for(int k=0;k<dim;k++){
				x[k]=a[k]+rnd.NextDouble()*(b[k]-a[k]);
			}
			double fx=f(x);
			mean+=fx;
			for(int k=0;k<dim;k++){
				if(x[k]>(a[k]+b[k])/2){
					meanRight[k]+=fx;
					nRight[k]++;
				}else{
					meanLeft[k]+=fx;
					nLeft[k]++;
				}
			}
		}

		mean/=N;
		for(int k=0;k<dim;k++){
			if(nLeft[k]>0) meanLeft[k]/=nLeft[k];
			if(nRight[k]>0) meanRight[k]/=nRight[k];
		}

		int kdiv=0;
		double maxVar=0;
		for(int k=1;k<dim;k++){
			double vara=Math.Abs(meanRight[k]-meanLeft[k]);
			if(vara>maxVar){
				maxVar=vara;
				kdiv=k;
			}
		}

		double integ=(mean*N+mean_reuse*n_reuse)/(N+n_reuse)*V;
		double error=Math.Abs(mean-mean_reuse)*V;
		double tolerance=acc+Math.Abs(integ)*eps;

		if(error<tolerance) return (integ,error);

		vector a2=a.copy(), b2=b.copy();
		double mid=(a[kdiv]+b[kdiv])/2;
		a2[kdiv]=mid;
		b2[kdiv]=mid;

		(double integLeft, double errLeft)=stratifiedmc(f,a,b2,acc/Math.Sqrt(2),eps,nLeft[kdiv],meanLeft[kdiv]);
		(double integRight, double errRight)=stratifiedmc(f,a2,b,acc/Math.Sqrt(2),eps,nRight[kdiv],meanRight[kdiv]);

		return (integLeft+integRight, Math.Sqrt(errLeft*errLeft+errRight*errRight));
	}		
}

public class Halton{
	List<int> bases;
	public Halton(int dim){
		bases=PrimeNumbers(dim);
	}
	public vector get(int n){
		int dim=bases.Count;
		var x =new vector(dim);
		for(int i=0;i<dim;i++){
			x[i]=Corput(n,bases[i]);
		}
		return x;
	}
	private static double Corput(int n, int b){
		double q=0;
		double bk=1.0/b;
		while(n>0){
			q+=(n%b)*bk;
			n/=b;
			bk/=b;
		}
		return q;
	}
	private static List<int> PrimeNumbers(int n){
		List<int> primes = new List<int>();
		int candidate=2;
		while(primes.Count<n){
			bool isPrime=true;
			foreach(int p in primes){
				if(p*p>candidate) break;
				if(candidate%p==0){
					isPrime=false;
					break;
				}
			}
			if(isPrime) primes.Add(candidate);
			candidate++;
		}
		return primes;
	}
}

