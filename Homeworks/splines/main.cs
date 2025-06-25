using static System.Console;
using static System.Math;
using System.IO;
using System;

class main{
	static int Main(){
		vector x=new vector(10);
		vector y=new vector(10);
		
		/*EXERCISE A*/
		for(int i=0;i<10;i++){
			x[i]=i;
			y[i]=Math.Cos(i);
		}
		double cosz, areaz;
		
		using(var File =new StreamWriter("out.linear.txt")){
			for(double z=x[0];z<x[x.size-1];z+=1.0/8){
				cosz=interpolation.linterp(x,y,z);
				areaz=interpolation.linterpInteg(x,y,z);
				File.WriteLine($"{z} {cosz} {areaz}");
			}
		}

		/*EXERCISE B*/
		for(int i=0;i<10;i++){
			y[i]=Math.Sin(i);
		}
		qspline spline=new qspline(x,y);
		double sinz, derivz;
		using(var File =new StreamWriter("out.qspline.txt")){
			for(double z=x[0];z<x[x.size-1];z+=1.0/8){
				sinz=spline.evaluate(z);
				derivz=spline.derivate(z);
				areaz=spline.integral(z);
				File.WriteLine($"{z} {sinz} {derivz} {areaz}");
			}
		}

		/*EXERCISE C*/
                cspline splinec=new cspline(x,y);
		using(var File =new StreamWriter("out.cspline.txt")){
                	for(double z=x[0];z<x[x.size-1];z+=1.0/8){
                        	sinz=splinec.evaluate(z);
                        	derivz=splinec.derivate(z);
                        	areaz=splinec.integral(z);
                        	File.WriteLine($"{z} {sinz} {derivz} {areaz}");
                	}
		}

		return 0;
	}
}
