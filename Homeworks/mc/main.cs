using static System.Console;
using static System.Math;
using System;

class main{
	static int Main(string[] args){
		vector a =new vector(new double[] {-1,-1});
		vector b =new vector(new double[] {1,1});
		Func<vector,double> circle = v=>(v[0]*v[0]+v[1]*v[1]<=1.0)? 1.0 : 0.0;
		for(int N=10; N<=1000; N+=50){
			var result=MC.plainmc(circle,a,b,N);
			double areaEst=result.Item1;
			double errEst=result.Item2;
			double errReal=Abs(areaEst-PI);
			WriteLine($"{1/Math.Sqrt(N):F6} {errEst:F6} {errReal:F6}");
		}
		a =new vector(new double[] {0,0,0});
		b =new vector(new double[] {PI,PI,PI});
		Func<vector,double> ftest = v=>1/(1-Cos(v[0])*Cos(v[1])*Cos(v[2]));
		var (result1, err)=MC.plainmc(ftest,a,b,1000000);
		result1/=(PI*PI*PI);
		err/=(PI*PI*PI);
		WriteLine($"\n∫dx/π ∫dy/π ∫dz/π [1-cos(x)cos(y)cos(z)]⁻1 = {result1}");
		return 0;
	}
}	
