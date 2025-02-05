using static System.Math;
public static class sfuns{
	public static double fgamma(double x){
		if(x<0)return PI/Sin(PI*x)/fgamma(1-x);
		if(x<9)return fgamma(x+1)/x;
		double lnfgamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
		return Exp(lnfgamma);
	}
}
