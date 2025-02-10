using static System.Console;
using static System.Math;

static class main{
	static int Main(){
		int i;
		funct obj = new funct();

		i = obj.maxvalue();
		System.Console.WriteLine($"My max int is {i}\n");
		i =obj.minvalue();
		System.Console.WriteLine($"My min int is {i}\n");
		
		double x=1; 
		while(1+x!=1){
			x/=2;
		} 
		x*=2;
		System.Console.WriteLine($"My machine epsilon for double is {x}\n");

		float y=1F; 
		while((float)(1F+y) != 1F){
			y/=2F;
		} 
		y*=2F;
		System.Console.WriteLine($"My machine epsilon for float is {y}\n");
		
		double epsilon=Pow(2,-52), tiny=epsilon/2, a=1+tiny+tiny, b=tiny+tiny+1;
		System.Console.WriteLine($"a==b? {a==b}\n");
		System.Console.WriteLine($"a>1? {a>1}\n");
		System.Console.WriteLine($"b>1? {b>1}\n");

		double d1=0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1, d2 = 8*0.1; 
		Write($"0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1==0.8? {obj.approx(d1, d2)}\n");
		return 0;
	}
}
		
