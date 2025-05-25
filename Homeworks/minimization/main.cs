using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System;

class main{
	static int Main(string[] args){
		
		/*EXERCISE A*/
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

		/*EXERCISE B*/
		var energy = new List<double>();
		var signal = new List<double>();
		var error  = new List<double>();
		var separators = new char[] {' ','\t'};
		var options = StringSplitOptions.RemoveEmptyEntries;
		string line;
		while((line=Console.In.ReadLine())!=null){
        		var parts=line.Split(separators,options);
        		energy.Add(double.Parse(parts[0], CultureInfo.InvariantCulture));
        		signal.Add(double.Parse(parts[1], CultureInfo.InvariantCulture));
        		error. Add(double.Parse(parts[2], CultureInfo.InvariantCulture));
		}
		Func<vector,double> D = v => {
			double m=v[0], G=v[1], A=v[2];
			if(m < 110 || m > 140 || G <= 0 || G > 30 || A < 0) return 1e10;
			double sum=0.0;
			for(int i=0;i<energy.Count;i++){
				double Fi=A/((energy[i]-m)*(energy[i]-m)+G*G/4.0);
				double resid=(Fi-signal[i])/error[i];
				sum+=resid*resid;
			}
			return sum;
		};
		vector v0=new vector(new double[] {125.0,1.5,10.0});
		var (v1, c)=Newton.solve(D,v0);
		WriteLine($"\nIt took {c} steps. Higgs bosson data:");
		WriteLine($"m = {v1[0]}");
		WriteLine($"Γ = {v1[1]}");
		WriteLine($"A = {v1[2]}");
				
		using(var higgsFile =new StreamWriter("higgs.dat")){
			for(int i=0;i<energy.Count;i++){
				higgsFile.WriteLine($"{energy[i]} {v1[2]/((energy[i]-v1[0])*(energy[i]-v1[0])+v1[1]*v1[1]/4.0)}");
			}
		}
		return 0;
	}
}

