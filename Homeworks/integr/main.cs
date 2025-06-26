using static System.Console;
using static System.Math;
using System.IO;
using System;

class main{
	static double ErfTabulated(double z){
		double[] xs = {0,0.02,0.04,0.06,0.08,0.1,0.2,0.3,0.4,0.5,0.6,0.7,0.8,0.9,1,1.1,1.2,1.3,1.4,1.5,1.6,1.7,1.8,1.9,2,2.1,2.2,2.3,2.4,2.5,3,3.5};
    		double[] erfs = {0,0.022564575,0.045111106,0.067621594,0.090078126,0.112462916,
                     0.222702589,0.328626759,0.428392355,0.520499878,0.603856091,0.677801194,
                     0.742100965,0.796908212,0.842700793,0.880205070,0.910313978,0.934007945,
                     0.952285120,0.966105146,0.976348383,0.983790459,0.989090502,0.992790429,
                     0.995322265,0.997020533,0.998137154,0.998856823,0.999311486,0.999593048,
                     0.999977910,0.999999257};

    		if(z < 0) return -ErfTabulated(-z); // simetry
	
    		for(int i=0; i < xs.Length - 1; i++){
        		if(z >= xs[i] && z < xs[i+1]){
         			double t = (z - xs[i]) / (xs[i+1] - xs[i]);
            			return erfs[i] * (1 - t) + erfs[i+1] * t;
        		}
    		}

		return 0;
	}
	static int Main(){
		
		/*EXERCISE A*/
		WriteLine($"SECCION A: Simple integration");
		double I = program.integrate(x => Sqrt(x),0,1);
		WriteLine($"sqrt(x) [0,1] : {I:F6} , error : {Abs(I-2.0/3):E6}");
		I = program.integrate(x => 1/Sqrt(x),0,1);
		WriteLine($"1/sqrt(x) [0,1] : {I:F6} , error : {Abs(I-2):E6}");
		I = program.integrate(x => Sqrt(1-x*x),0,1);
		WriteLine($"sqrt(1-x²) [0,1] : {I:F6} , error : {Abs(I-PI/2):E6}");
		I = program.integrate(x => Log(x)/Sqrt(x),0,1);
		WriteLine($"log(x)/sqrt(x) [0,1] : {I:F6} , error : {Abs(I+4):E6}");

		double z=1.0;
		double erfValue=program.erf(z,0.001,0.001);
		WriteLine($"\nErf({z}) = {erfValue:F6}\n\n");
		
		using(var File =new StreamWriter("out.erfplot.dat")){
			for(double val=-3.0;val<=3.0;val+=0.05){
				double erfComputed=program.erf(val,0.001,0.001);
				double erfTabulated=ErfTabulated(val);
				File.WriteLine($"{val} {erfComputed} {erfTabulated}");
			}
		}

		double eps=0;
		using(var File =new StreamWriter("out.erf.txt")){
			for(int i=1;i<=10;i++){
				double acc=Pow(10,-i);
				double result=program.erf(z,acc,eps);
				double err=Abs(result-erfValue);
				File.WriteLine($"{acc}\t{err}");
			}
		}

		/*EXERCISE B*/
		WriteLine($"\nSECCION B: Clenshaw-Curtis variable transformation for integration");
		WriteLine($"Compare ordinary adaptive integration with CC integration");
		double ncalls=0;
		I=program.integrate(x => {ncalls++; return 1/Sqrt(x);},0,1);
		WriteLine($"\n1/sqrt(x) [0,1] ordinary : {I:F6}  evaluations : {ncalls}");
		ncalls=0;
		I=program.integrateCC(x => {ncalls++; return 1/Sqrt(x);},0,1);
		WriteLine($"1/sqrt(x) [0,1] Clenshaw-Curtis : {I:F6}  evaluations : {ncalls}");
		ncalls=0;
		I=program.integrate(x => {ncalls++; return Log(x)/Sqrt(x);},0,1);
		WriteLine($"\nlog(x)/sqrt(x) [0,1] ordinary : {I:F6}  evaluations : {ncalls}");
		ncalls=0;
		I=program.integrateCC(x => {ncalls++; return Log(x)/Sqrt(x);},0,1);
		WriteLine($"log(x)/sqrt(x) [0,1] Clenshaw-Curtis : {I:F6}  evaluations : {ncalls}");
		ncalls=0;
		I=program.integrateGeneral(x => {ncalls++; return Exp(-x*x);},0,double.PositiveInfinity,1e-6,0);
		WriteLine($"\nGeneralized integrator for inifinite limits");
		WriteLine($"exp(-x²) [0,∞) : {I:F6}  evaluations : {ncalls}");
		WriteLine($"\n");		

		/*EXERCISE C*/
		WriteLine($"\nSECCION C: Adaptive integrator with error estimate");
		var (I1, err1)=program.integratewitherr(x => Sqrt(x),0,1);
                WriteLine($"sqrt(x) [0,1] : {I1:F6} ± {err1:E6}");
                var (I2, err2) = program.integratewitherr(x => 1/Sqrt(x),0,1);
                WriteLine($"1/sqrt(x) [0,1] : {I2:F6} ± {err2:E6}");
                var (I3, err3) = program.integratewitherr(x => Sqrt(1-x*x),0,1);
                WriteLine($"sqrt(1-x²) [0,1] : {I3:F6} ± {err3:E6}");
                var (I4, err4) = program.integratewitherr(x => Log(x)/Sqrt(x),0,1);
                WriteLine($"log(x)/sqrt(x) [0,1] : {I4:F6} ± {err4:E6}");

		WriteLine($"\n\nCompare estimated errors with actual");
		WriteLine("Function\tEstimated Error\tReal Error");
		WriteLine($"sqrt(x)\t\t{err1:E6}\t{Abs(I1 - 2.0/3):E6}");
		WriteLine($"1/sqrt(x)\t{err2:E6}\t{Abs(I2 - 2.0):E6}");
		WriteLine($"sqrt(1-x²)\t{err3:E6}\t{Abs(I3 - PI / 4):E6}");
		WriteLine($"log(x)/sqrt(x)\t{err4:E6}\t{Abs(I4 + 4):E6}");

		return 0;
	}
}

