using static System.Console;
using static System.Math;
using System;

class main{
	static int Main(){
		Func<vector,double> rosenbrock = v => (1-v[0])*(1-v[0])+100*(v[1]-v[0]*v[0])*(v[1]-v[0]*v[0]);
		Func<vector,double> himmelblau = v => (v[0]*v[0]+v[1]-11)*(v[0]*v[0]+v[1]-11)+(v[0]+v[1]*v[1]-7)*(v[0]+v[1]*v[1]-7);
		vector x=new vector(new double[] {-1.2,1.1});
		var (x1,c1)=Newton.solve(rosenbrock,x);
		x=new vector(new double[] {-2.0,4.0});
		var(x2,c2)=Newton.solve(himmelblau,x);
		Write($"n = {c1} Rosenbrock has a minimum in");
		x1.print();
		Write($"n = {c2} Himmelblau has a minimum in");
		x2.print();
		return 0;
	}
}

