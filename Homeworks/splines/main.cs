using static System.Console;
using static System.Math;
using System;

class main{
	static int Main(){
		vector x=new vector(10);
		vector y=new vector(10);
		for(int i=0;i<10;i++){
			x[i]=i;
			y[i]=Math.Cos(i);
		}
		double cosz, areaz;
		for(double z=x[0];z<x[x.size-1];z+=1.0/8){
			cosz=interpolation.linterp(x,y,z);
			areaz=interpolation.linterpInteg(x,y,z);
		WriteLine($"{z} {cosz} {areaz}");
		}
		return 0;
	}
}
