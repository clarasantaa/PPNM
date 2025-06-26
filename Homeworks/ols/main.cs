using static System.Console;
using static System.Math;
using System.IO;
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
		matrix cov =new matrix(y.size, y.size);
		(coeffs,cov)=LeastSquaresFit.lsfit(basisFunctions, t, logY, dLogY);
		
		double lnA = coeffs[0], lambda = coeffs[1], dlnA = Math.Sqrt(cov[0,0]), dlambda = Math.Sqrt(cov[1,1]);
		double T_half = Math.Log(2) / lambda; 
		
		using (StreamWriter outFile = new StreamWriter("Out.txt", true)){
			Console.SetOut(outFile);
			WriteLine($"SECTION A: Least-Squares fit by QR decomposition");

			WriteLine($"Fitting ln(y) = ln(a) - λ·t using ols");
			WriteLine($"λ = {lambda} ± {dlambda}\n");
			WriteLine($"Covariance matrix =");
			cov.print();
			WriteLine($"");
			outFile.Flush();
		}

		
		using(StreamWriter RAFile = new StreamWriter("out.data.txt", false)){
			Console.SetOut(RAFile);
			WriteLine($"{lnA} {lambda} {dlnA} {dlambda}");
			for(int i=0;i<t.size;i++){
				WriteLine($"{t[i]} {y[i]} {dy[i]}");
			}
       		RAFile.Flush();
                }
		
		using (StreamWriter outFile = new StreamWriter("Out.txt", true)){
			Console.SetOut(outFile);
			/*ΔT_1/2 = |dT_1/2 / dλ| Δλ = ln2 / λ^2 *Δλ*/
			
			WriteLine($"\nSECTION B: Uncertanty of the Half-life");
			WriteLine($"T_half = {T_half} days");
			double dT_half=Math.Log(2)/(lambda*lambda)*Math.Sqrt(cov[1,1]);
			WriteLine($"dT_half = {dT_half}");

			/*The real T_half is approx 3,66 days. Let's see if it's inside our interval*/

			if(Math.Abs(T_half-3.66)<dT_half){
				WriteLine($"The T_half ({T_half} days) calculated agrees with the modern value (3.66 days)");
			}else{
				WriteLine($"The T_half ({T_half} days) calculated dos NOT agree with the modern value (3.66 days) by {Math.Abs(T_half-3.66)}");
			}

			outFile.Flush();
		}

		return 0;
	}
}



