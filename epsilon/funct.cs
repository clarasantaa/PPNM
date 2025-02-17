using static System.Console;
using static System.Math;

public class funct{
	
	private int i, j;

	public funct(){
		i=j=1;
	}
	//asdf
	public int maxvalue(){
		while (i+1>i){
			i++;
		}
		return i;
	}

	public int minvalue(){
		while(j-1<j){
			j--;
		}
		return j;
	}

	public bool approx(double a, double b, double acc=1e-9, double eps=1e-9){
		if(Abs(b-a) <= acc) return true;
		if(Abs(b-a) <= Max(Abs(a),Abs(b))*eps) return true;
		return false;
	}
}	
