using static System.Console;
using static System.Math;
using System;

class main{
	static int Main(){
		
		Func<vector,vector> rosenGradient = v => {
			double x=v[0], y=v[1];
			double dg_dx=-2*(1-x)-400*x*(y-x*x);
			double dg_dy=200*(y-x*x);
			return new vector(new double[]{dg_dx,dg_dy});
		};
		
		Func<vector,vector> himmelGradient = v => {
			double x=v[0], y=v[1];
			double dg_dx=4*x*(x*x+y-11)+2*(x+y*y-7);
			double dg_dy=2*(x*x+y-11)+4*y*(x+y*y-7);
			return new vector(new double[]{dg_dx,dg_dy});
		};

		vector start =new vector(new double[] {-1.2,1.0});
		double acc=1e-6;
		vector rootRosembrock=RootFinding.newton(rosenGradient,start,acc);
		Write($"Root found for Rosembrock : x = ");
		rootRosembrock.print();
		
		start =new vector(new double[] {-2.0,3.0});
		vector rootHimmelblau=RootFinding.newton(himmelGradient,start,acc);
		Write($"Root found for Himmelblau : x = ");
		rootHimmelblau.print();
		return 0;
	}
}

