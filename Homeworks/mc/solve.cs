using static System.Console;
using static System.Math;
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
}
