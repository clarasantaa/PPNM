using static System.Console;
using static System.Math;
using System;

class main{
	static int Main(string[] args){
		
		/*EXERCISE A*/
		vector a =new vector(new double[] {-1,-1});
		vector b =new vector(new double[] {1,1});
		Func<vector,double> circle = v=>(v[0]*v[0]+v[1]*v[1]<=1.0)? 1.0 : 0.0;
		double areaEst=0, errEst=0;
		for(int N=10; N<=1000; N+=50){
			var result=MC.plainmc(circle,a,b,N);
			areaEst=result.Item1;
			errEst=result.Item2;
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
		
		/*EXERCISE B*/
		a =new vector(new double[] {-1,-1});
		b =new vector(new double[] {1,1});
		var resultQ=MC.quasimc(circle,a,b,1000);
		double areaQ=resultQ.Item1;
		double errQ=resultQ.Item2;
		WriteLine($"\nMethod\t\tArea Circle\tErr Est\t\tErr Real\t");
		WriteLine($"QuasiMC\t\t{areaQ:F6}\t{errQ:E3}\t{Math.Abs(areaQ-PI):E3}");
		WriteLine($"PseudoRandom\t{areaEst:F6}\t{errEst:E3}\t{Math.Abs(areaEst-PI):E3}"); 
		
		/*EXERCISE C*/
		var resultS=MC.stratifiedmc(circle,a,b);
		double areaS=resultS.Item1;
		double errS=resultS.Item2;
		WriteLine($"StratifiedMC\t{areaS:F6}\t{errS:E3}\t{Math.Abs(areaS-PI):E3}");
		return 0;
	
	}
}	
