using static System.Console;
class main{
	public class data { 
		public int a,b; 
		public double sum;
	}

	public static void harm(object obj){
		var arg = (data)obj;
		arg.sum = 0;
		for(int i=arg.a +1 ; i<= arg.b; i++) arg.sum+=1.0/i;
	}

	public static int Main(string[] args){
		int nthreads = 1, nterms = (int)1e8, argc = args.Length; 
		for(int i=0; i<argc; i++){
			string arg = args[i];
			if(arg=="-threads" && i+1<argc) nthreads = int.Parse(args[i+1]);
			if(arg=="-terms" && i+1<argc) nterms = (int)double.Parse(args[i+1]);
		}
	
		data[] param = new data[nthreads];
		
		for(int i=0; i<nthreads; i++){
   			param[i] = new data();
   			param[i].a = nterms/nthreads*i;
   			param[i].b = nterms/nthreads*(i+1);
   		}
		
		var threads = new System.Threading.Thread[nthreads];
		
		for(int i=0; i<nthreads; i++) {
   			threads[i] = new System.Threading.Thread(harm);
   			threads[i].Start(param[i]); 
   		} //Every thread does the harmonic sum

		foreach(var thread in threads) thread.Join(); //Joining the threads

		double total=0; 
		
		foreach(var p in param) total+=p.sum; //Joining the sums

		WriteLine($"Sum = {total}");
		return 0;
	}
}
