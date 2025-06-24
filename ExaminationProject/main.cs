using static System.Console;
using static System.Math;
using System.Linq;
using System.Numerics;
using System.IO;
using System;

class main{
	static int Main(){
		int N=1024;
		double f0=5; /*our frequency*/
		var rnd =new Random(123);
		Complex[] x =new Complex[N];
		
		for(int i=0;i<N;i++){
			double t=i/(double) N;
			double signal=Cos(2*PI*f0*t);
			double noise=0.4*(rnd.NextDouble()-0.5);
			x[i]=new Complex(signal+noise,0);
		}/*signal with noise*/

		Complex[] spectrum=matlib.fft(x);
		Complex[] specAmp=(Complex[])spectrum.Clone();
		Complex[] specFreq=(Complex[])spectrum.Clone();

		double maxAmp=spectrum.Max(c=>c.Magnitude);	
		double threshold=0.1*maxAmp;
		for(int i=0;i<N;i++){
			if(specAmp[i].Magnitude<threshold) specAmp[i]=Complex.Zero;
		} /*filter by amplitud asking for 10% or more of the max*/

		Complex[] xAmpFiltered=matlib.ift(specAmp);

		int cutoff=50; /*low-pass we want to conserve i=0,...,50,974,...,1023*/
		for(int i=cutoff;i<N-cutoff;i++){
			specFreq[i]=Complex.Zero;
		}

		Complex[] xFreqFiltered=matlib.ift(specFreq);
		
		WriteLine("{0,8} {1,12} {2,12} {3,12} {4,12}", "t", "Original", "Noisy", "AmpFilt", "FreqFilt");
		for(int i=0;i<N;i++){
			double t=i/(double)N;
			double orig=Cos(2*PI*f0*t);
			double noise=x[i].Real;
			double ampF=xAmpFiltered[i].Real;
			double freqF=xFreqFiltered[i].Real;
			WriteLine("{0,8:F4} {1,12:F4} {2,12:F4} {3,12:F4} {4,12:F4}", t, orig, noise, ampF, freqF);
		}

		return 0;
	}
}	
