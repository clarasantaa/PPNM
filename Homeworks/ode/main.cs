using static System.Console;
using System.Collections.Generic;
using static System.Math;
using System;
using System.IO;

class main{
	public static vector oscillator(double x, vector y){
		double u=y[0];
		double du=y[1];
		return new vector(new double[] {du,-u});
	}

	public static Func<double, vector, vector> makeoscillator(double eps){
		return (phi, y) => {
			double u=y[0];
			double du=y[1];
			return new vector(new double[] {du,1-u+eps*u*u});
		};
	}	

	public static int Main(){
		
		/*EXERCISE A*/
		vector yinit=new vector(new double[] {1.0,0.0});
		var (xList, yList)=ODESolver.driver(oscillator,(0,10),yinit);
		for(int i=0;i<xList.Count;i++){
			Write($"{xList[i],10:F3} ");
			yList[i].print();
		}

		/*EXERCISE B*/
		double rotations=4;
		double phiMax=2*PI*rotations;
		
		/*ε=0 u(0)=1 u'(0)=0*/
		var solver1=makeoscillator(0.0);
		vector yinit1=new vector(new double[] {1.0,0.0});
		/*we are going to force more steps*/
		double phi=0;
		double h=0.001;
		using (var circFile =new StreamWriter("out.circle.dat")){
                	while(phi<=phiMax){
				circFile.WriteLine($"{phi:F6} {yinit1[0]:F6}");
				var dydx=solver1(phi,yinit);
				yinit1=yinit1+h*dydx;
				phi+=h;
            		}
		}

		/*ε=0 u(0)=1 u'(0)=-0.5*/
		var solver2=makeoscillator(0.0);
		vector yinit2=new vector(new double[] {1.0,-0.5});
		var (phis2, ys2)=ODESolver.driver(solver2,(0,phiMax),yinit2);
		using (var ellipFile =new StreamWriter("out.ellipse.dat")){
            		for (int i=0; i<phis2.Count; i++){
                		ellipFile.WriteLine($"{phis2[i]:F6} {ys2[i][0]:F6}");
            		}
		}

		/*ε=0.01 u(0)=1 u'(0)=-0.5*/
		var solver3=makeoscillator(0.01);
		vector yinit3=new vector(new double[] {1.0,-0.5});
		var (phis3, ys3)=ODESolver.driver(solver3,(0,phiMax),yinit3);
		using (var relaFile =new StreamWriter("out.relativistic.dat")){
            		for (int i=0; i<phis3.Count; i++){
                		relaFile.WriteLine($"{phis3[i]:F6} {ys3[i][0]:F6}");
            		}
		}

		/*EXERCISE C*/
		Func<double,vector,vector> f=(x,y)=>
			new vector(new double[]{y[1],2*x});
		vector yinitC=new vector(new double[]{0.0,0.0});
		var (xC, yC, hC) =ODESolver.driver23(f,(0.0,1.0),yinitC,0.1);
		WriteLine($"\n");
		for(int i=0;i<xC.Count;i++){
		//	Write($"{hC[i],10:F3} {xC[i],10:F3}" );
		//	yC[i].print();
		}
		return 0;
	}

}
