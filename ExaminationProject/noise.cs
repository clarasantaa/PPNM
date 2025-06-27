using static System.Console;
using static System.Math;
using System.Linq;
using System.Numerics;
using System;

public static class SignalCleaner{
	public static Complex[] Amplitude(Complex[] x){ 
		
		int N=x.Length;
		Complex [] filtered=(Complex[])x.Clone();
                double maxAmp=filtered.Max(c=>c.Magnitude);
                double threshold=0.1*maxAmp;
               
	       	for(int i=0;i<N;i++){
                        if(filtered[i].Magnitude<threshold) filtered[i]=Complex.Zero;
                } /*filter by amplitude asking for 10% or more of the max*/

		return filtered;
	}

        public static Complex[] Frequency(Complex[] x, int cutoff){

                int N=x.Length;
		Complex [] filtered=(Complex[])x.Clone();
                for(int i=cutoff;i<N-cutoff;i++){
			filtered[i]=Complex.Zero;
                }

		return filtered;
	}
}
