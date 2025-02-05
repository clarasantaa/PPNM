using static System.Math;
static class ex1{
	static int Main(){
		double sqrt2=Sqrt(2.0), x;
		System.Console.WriteLine($"sqrt2²={sqrt2*sqrt2}");
		x=System.Math.Pow(2, 1.0/5);
		System.Console.WriteLine($"{x}");
		x=System.Math.Exp(PI);
		System.Console.WriteLine($"{x}");
		x=System.Math.Pow(PI, Exp(1));
		System.Console.WriteLine($"{x}");
		return 0;
	}
}
