using static System.Console;
using static System.Math;
using System;

class main{
	static int Main(){
		//Data
		vector t =new vector(new double[] {1, 2, 3, 4, 6, 9, 10, 13, 15});
		vector y = new vector(new double[] {117, 100, 88, 72, 53, 29.5, 25.2, 15.2, 11.1});
		vector dy = new vector(new double[] {6, 5, 4, 4, 4, 3, 3, 2, 2});
		
		vector logY =new vector(y.size);
		vector dLogY =new vector(y.size);
		// Using error propagation:
		// d(ln(y)) / dy = 1/y  → δln(y) ≈ (d ln(y) / dy) * δy = (1/y) * δy
		// Then, δln(y) = δy / y
		for (int i = 0; i < y.size; i++){
    			logY[i] = Log(y[i]);
    			dLogY[i] = dy[i] / y[i];
		}
		Func<double,double>[] basisFunctions = new Func<double, double>[] {
            		z => 1.0,
            		z => -z
		};
		vector coeffs =new vector(y.size);
		coeffs=LeastSquaresFit.lsfit(basisFunctions, t, logY, dLogY);
		double lnA = coeffs[0], lambda = coeffs[1];
		WriteLine($"{lnA} {lambda}");
		double T_half = Math.Log(2) / lambda;
		for(int i=0;i<t.size;i++){
			WriteLine($"{t[i]} {y[i]} {dy[i]} {Math.Exp(lnA)*Math.Exp(-lambda*t[i])}");
		}
		return 0;
	}
}



