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
		using (var File=new StreamWriter("out.rk23.dat")){
			File.WriteLine($"\th\t x\ty_num\ty_exact\terror");
			for(int i=0;i<xC.Count;i++){
				double ynum=yC[i][0];
				double yexact=Pow(xC[i],3)/3.0;
				File.WriteLine($"{hC[i],10:F3} {xC[i],10:F3}\t{ynum:F3}\t{yexact:F3}\t{Abs(ynum-yexact):E6}");
			}
		}
		
		var qspline=ODESolver.make_ode_ivp_qspline(f,(0.0,1.0),yinitC);
		double x_eval=0.5;
		vector y_interp=qspline(x_eval);

		double y_exact=Pow(x_eval,3)/3.0, dy_exact=Pow(x_eval,2);

		WriteLine($"\n\nInterpolated values of y''=2x with y(0) = 0 y'(0) = 0");
		WriteLine($"y({x_eval}) = {y_interp[0]:F5} , exact = {y_exact:F5}, error = {Math.Abs(y_interp[0] - y_exact):E2}");
		WriteLine($"y'({x_eval}) = {y_interp[1]:F5} , exact = {dy_exact:F5}, error = {Math.Abs(y_interp[1] - dy_exact):E2}");
		
		Func<double,vector,vector> threebody = (t,z) =>{
			var dz =new vector(12);
			vector r1 =new vector(new double[] {z[6],z[7]});
			vector r2 =new vector(new double[] {z[8],z[9]});
			vector r3 =new vector(new double[] {z[10],z[11]});
			
			vector v1 =new vector(new double[] {z[0],z[1]});
			vector v2 =new vector(new double[] {z[2],z[3]});
			vector v3 =new vector(new double[] {z[4],z[5]});

			vector r12=r2-r1;
			vector r13=r3-r1;
			vector r23=r3-r2;

			double r12len=r12.norm(), r13len=r13.norm(), r23len=r23.norm();

			vector a1=r12/Pow(r12len,3)+r13/Pow(r13len,3);
			vector a2=-r12/Pow(r12len,3)+r23/Pow(r23len,3);
			vector a3=-r13/Pow(r13len,3)-r23/Pow(r23len,3);

			dz[0]=a1[0];
			dz[1]=a1[1];
			dz[2]=a2[0];
			dz[3]=a2[1];
			dz[4]=a3[0];
			dz[5]=a3[1];
			dz[6]=v1[0];
			dz[7]=v1[1];
			dz[8]=v2[0];
			dz[9]=v2[1];
			dz[10]=v3[0];
			dz[11]=v3[1];
			
			return dz;
		};
		
		vector z0 = new vector(0.4662036850,0.4323657300,-0.93240737,-0.86473146,0.4662036850,0.4323657300,-0.97000436,0.24308753,0.0,0.0,0.97000436,-0.24308753);
		
		double t_eval=6.3259; //we integrate over one third of a period
		var(xT,yT)=ODESolver.driver(threebody,(0,t_eval/3),z0,0.001,1e-4,1e-4);

		using (var File =new StreamWriter("out.threebody.dat")){
			for(int i=0;i<xT.Count;i++){
			vector zi=yT[i];
			File.WriteLine($"{zi[6]} {zi[7]} {zi[8]} {zi[9]} {zi[10]} {zi[11]}");
			}
		}

		return 0;
	}

}
