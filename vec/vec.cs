using static System.Console;
using static System.Math;
public class vec{
	public double x,y,z;
	public vec(){
		x=y=z=0;
	}
	public vec(double x, double y, double z){
		this.x=x;
		this.y=y;
		this.z=z;
	}
	
	public static vec operator+(vec u, vec v){
		return new vec(u.x+v.x, u.y+v.y, u.z+v.z);
	}

	public static vec operator-(vec u, vec v){
		return new vec(u.x-v.x, u.y-v.y, u.z-v.z);
	}

	public static vec operator*(vec v, double c){
		return new vec(c*v.x, c*v.y, c*v.z);
	}


	public static double operator*(vec u, vec v){
		return u.x*v.x+u.y*v.y+u.z*v.z;
	}

	public void print(string s=""){
		Write(s); WriteLine($" [{x:F6}, {y:F6}, {z:F6}]");
	}

	public override string ToString(){
		return $"[{x:F6}, {y:F6}, {z:F6}]";
	}

	public static bool approx(double a, double b, double acc=1e-6, double eps=1e-6){
		if(Abs(a-b)<acc)return true;
		if(Abs(a-b)<(Abs(a)+Abs(b))*eps)return true;
		return false;
	}
	
	public bool approx(vec other){
	if(!approx(this.x,other.x))return false;
	if(!approx(this.y,other.y))return false;
	if(!approx(this.z,other.z))return false;
	return true;
	}
	
	public static bool approx(vec u, vec v) => u.approx(v);
}
