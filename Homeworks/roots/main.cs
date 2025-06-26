using static System.Console;
using static System.Math;
using System.IO;
using System;

class main{
	static int Main(){
		
		/*EXERCISE A*/
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
		
		/*EXERCISE B*/
		double rmin=1e-8, rmax=8;
		double E0=Hydrogen.FindGroundState(rmin,rmax);
		WriteLine($"\nGround state energy E0 = {E0:F6}");
		
		Func<double,vector,vector> funct = (r,y) => Hydrogen.F(r,y,E0);
		vector y0=new vector(new double[] {rmin-rmin*rmin,1-2*rmin});
		var(rList,yList)=ODESolver.driver(funct,(rmin,rmax),y0,0.1);

		using(var hydroFile =new StreamWriter("out.hydrogen.dat")){
			for(int i=0;i<rList.Count;i++){
				double r=rList[i];
				double fnum=yList[i][0];
				double fexact=r*Math.Exp(-r);
				hydroFile.WriteLine($"{r} {fnum} {fexact}");
			}
		}
		
		double[] rmaxVals={4,6,8,10,12};
		foreach (var rmaxVal in rmaxVals){
			var (rListTemp,yListTemp)=ODESolver.driver(funct,(rmin,rmaxVal),y0,0.1);
			using(var file =new StreamWriter($"out.rmax_{rmaxVal}.dat")){
				for(int i=0;i<rListTemp.Count;i++){
					double r=rListTemp[i];
					double f=yListTemp[i][0];
					file.WriteLine($"{r} {f}");
				}
			}
		}
		
		double[] rminVals={1e-7,1e-6,1e-5,1e-4,1e-3};
                foreach (var rminVal in rminVals){
                        var (rListTemp,yListTemp)=ODESolver.driver(funct,(rminVal,rmax),y0,0.1);
                        using(var file =new StreamWriter($"out.rmin_{rminVal}.dat")){
                                for(int i=0;i<rListTemp.Count;i++){
                                        double r=rListTemp[i];
                                        double f=yListTemp[i][0];
                                        file.WriteLine($"{r} {f}");
                                }
                        }
                }

		double[] accVals={1e-7,1e-6,1e-5,1e-4,1e-3};
                foreach (var accVal in accVals){
                        var (rListTemp,yListTemp)=ODESolver.driver(funct,(rmin,rmax),y0,0.1,accVal);
                        using(var file =new StreamWriter($"out.acc_{accVal}.dat")){
                                for(int i=0;i<rListTemp.Count;i++){
                                        double r=rListTemp[i];
                                        double f=yListTemp[i][0];
                                        file.WriteLine($"{r} {f}");
                                }
                        }
                }
		
		double[] epsVals={1e-7,1e-6,1e-5,1e-4,1e-3};
                foreach (var eps in epsVals){
                        var (rListTemp,yListTemp)=ODESolver.driver(funct,(rmin,rmax),y0,0.1,acc,eps);
                        using(var file =new StreamWriter($"out.eps_{eps}.dat")){
                                for(int i=0;i<rListTemp.Count;i++){
                                        double r=rListTemp[i];
                                        double f=yListTemp[i][0];
                                        file.WriteLine($"{r} {f}");
                                }
                        }
                }
		return 0;
	}
}

