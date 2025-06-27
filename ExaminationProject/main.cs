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
		Complex[] specAmp=SignalCleaner.Amplitude((Complex[])spectrum.Clone());
		Complex[] specFreq=SignalCleaner.Frequency((Complex[])spectrum.Clone(), 15);

		Complex[] xAmpFiltered=matlib.ift(specAmp);
		Complex[] xFreqFiltered=matlib.ift(specFreq);
		
		using (var File =new StreamWriter("out.noise1.dat")){
			File.WriteLine("{0,8} {1,12} {2,12} {3,12} {4,12}", "t", "Original", "Noisy", "AmpFilt", "FreqFilt");
			for(int i=0;i<N;i++){
				double t=i/(double)N;
				double orig=Cos(2*PI*f0*t);
				double noise=x[i].Real;
				double ampF=xAmpFiltered[i].Real;
				double freqF=xFreqFiltered[i].Real;
				File.WriteLine("{0,8:F4} {1,12:F4} {2,12:F4} {3,12:F4} {4,12:F4}", t, orig, noise, ampF, freqF);
			}
		}

		//we will try with a more complex signal as well
		double f1=5, f2=20, f3=60;
		
		for(int i=0;i<N;i++){
			double t=i/(double) N;
			double signal=Cos(2*PI*f1*t)+0.5*Cos(2*PI*f2*t+PI/4)+0.3*Cos(2*PI*f3*t+PI/3);
			double noise=0.4*(rnd.NextDouble()-0.5);
			x[i]=new Complex(signal+noise,0);
		}/*signal with noise*/

		spectrum=matlib.fft(x);
		specAmp=SignalCleaner.Amplitude((Complex[])spectrum.Clone());
		specFreq=SignalCleaner.Frequency((Complex[])spectrum.Clone(),70);

		xAmpFiltered=matlib.ift(specAmp);
		xFreqFiltered=matlib.ift(specFreq);
		
		using (var File =new StreamWriter("out.noise2.dat")){
			File.WriteLine("{0,8} {1,12} {2,12} {3,12} {4,12}", "t", "Original", "Noisy", "AmpFilt", "FreqFilt");
			for(int i=0;i<N;i++){
				double t=i/(double)N;
				double signal=Cos(2*PI*f1*t)+0.5*Cos(2*PI*f2*t+PI/4)+0.3*Cos(2*PI*f3*t+PI/3);
				double noise=x[i].Real;
				double ampF=xAmpFiltered[i].Real;
				double freqF=xFreqFiltered[i].Real;
				File.WriteLine("{0,8:F4} {1,12:F4} {2,12:F4} {3,12:F4} {4,12:F4}", t, signal, noise, ampF, freqF);
			}
		}

		return 0;
	}
}	
