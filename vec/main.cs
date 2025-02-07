using static System.Console;
using static System.Math;
static class main{
	static int Main(){
		var rnd = new System.Random();
		var u = new vec(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble());
		var v = new vec(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble());
		var t = new vec();
		
		System.Console.WriteLine($"Verify print function works\n");
		u.print("u =");
		v.print("v =");
		System.Console.WriteLine($"u = {u:F6}");
		System.Console.WriteLine($"v = {v:F6}");
		
		System.Console.WriteLine($"\nVerify plus function works\n");
		t = new vec(u.x+v.x, u.y+v.y, u.z+v.z);
		(u+v).print("u+v =");
		t.print("t =");
		if(vec.approx(t,u+v)){
			System.Console.WriteLine("It works\n");
		}else{
			System.Console.WriteLine("It doesn't work\n");
		}
		
		System.Console.WriteLine($"\nVerify minus function works\n");
		t = new vec(u.x-v.x, u.y-v.y, u.z-v.z);
		(u-v).print("u-v =");
		t.print("t =");
		if(vec.approx(t,u-v)){
			System.Console.WriteLine("It works\n");
		}else{
			System.Console.WriteLine("It doesn't work\n");
		}
		
		System.Console.WriteLine($"\nVerify multiplication by a scalar works\n");
		double c = rnd.NextDouble();
		t = new vec(u.x*c, u.y*c, u.z*c);
		var tmp=u*c;
		(tmp).print("u*c =");
		t.print("t =");
		if(vec.approx(t,tmp)){
			System.Console.WriteLine("It works\n");
		}else{
			System.Console.WriteLine("It doesn't work\n");
		}

		System.Console.WriteLine($"Verify the product of vectors works\n");
		double d = u.x*v.x+u.y*v.y+u.z*v.z;
		System.Console.WriteLine($"u*v ={u*v}");
		System.Console.WriteLine($"d ={d}");
		if(vec.approx(d,u*v)){
			WriteLine("It works\n");
		}else{
			WriteLine("It doesn't work\n");
		}

	return 0;
	}
}
