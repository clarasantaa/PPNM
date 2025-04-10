using static System.Console;
using System.Collections.Generic;
using static System.Math;
using System;

class main{
	public static vector oscillator(double x, vector y){
		double u=y[0];
		double du=y[1];
		return new vector(new double[] {du,-u});
	}

	public static int Main(){
		vector yinit=new vector(new double[] {1.0,0.0});
		var (xList, yList)=ODESolver.driver(oscillator,(0,10),yinit);
		for(int i=0;i<xList.Count;i++){
			Write($"{xList[i],10:F3} ");
			yList[i].print();
		}
		return 0;
	}

}
