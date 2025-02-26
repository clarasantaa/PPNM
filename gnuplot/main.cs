using static System.Console;

class main{
	static int Main(){
		for(double i=-4;i<4;i+=1.0/8){
			WriteLine($"{i} {funct.erf(i)}");
		}
		WriteLine($"\n");
		for(double i=-4;i<4;i+=1.0/8){
			WriteLine($"{i} {funct.sgamma(i)}");
		}

		return 0;
	}
}
