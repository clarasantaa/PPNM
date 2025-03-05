using static System.Console;
using static System.Math;
using System.IO;
using System;

class main{
	static int Main(string[] args){
		int n = int.Parse(args[0]);
		matrix A =new matrix(n,n);
		
		var rnd = new System.Random(1);
		for(int i=0;i<n;i++){
			for(int j=0;j<i;j++){
				A[i,j]=A[j,i];
			}
			for(int j=i;j<n;j++){
				A[i,j]= rnd.NextDouble();
			}
		}
		
		matrix V =new matrix(n,n);
		V.setid();
		matrix A1 =new matrix (n,n);
		A1=A.copy();
		jacobi.cyclic(A, V);
		
		matrix Vt =new matrix(n,n);
		Vt=V.transpose();

		int rmax = int.Parse(args[1]);
		double dr = double.Parse(args[2]);
		int npoints = (int)(rmax/dr)-1;
		vector r = new vector(npoints);
		for(int i=0;i<npoints;i++)r[i]=dr*(i+1);
		matrix H = new matrix(npoints,npoints);
		for(int i=0;i<npoints-1;i++){
   			H[i,i]  =-2*(-0.5/dr/dr);
   			H[i,i+1]= 1*(-0.5/dr/dr);
   			H[i+1,i]= 1*(-0.5/dr/dr);
  		}
		H[npoints-1,npoints-1]=-2*(-0.5/dr/dr); /*We create K*/ 
		for(int i=0;i<npoints;i++)H[i,i]+=-1/r[i]; /*We add W*/
		
		matrix Id =new matrix(npoints,npoints);
		Id.setid();
		matrix H1 =new matrix(npoints,npoints);
		H1=H.copy();
		jacobi.cyclic(H,Id);
		
		/*WRITING IN OUT.TXT*/
		using (StreamWriter outFile = new StreamWriter("Out.txt", false)){
			Console.SetOut(outFile);
	
			WriteLine($"A =");
			A1.print();
		
			WriteLine($"\nD =");
                	A.print();

                	WriteLine($"\nV =");
                	V.print();
		
			matrix M= new matrix(n,n);
                	M=Vt*A1*V;
                	WriteLine($"\nVt * A * V = D {M.approx(A)}");

                	M=V*A*Vt;
                	WriteLine($"\nV * D * Vt = A {M.approx(A1)}");

                	M=Vt*V;
                	matrix I =new matrix(n,n);
                	I.setid();
                	WriteLine($"\nVt * V = Id {M.approx(I)}");
		
                	M=V*Vt;
                	WriteLine($"\nV * Vt = Id {M.approx(I)}");


			WriteLine($"\nH =");
			H1.print();
				
			WriteLine($"\nEn =");
			for(int i=0;i<npoints;i++){
				Write($"{H[i,i]:e4} ");
			}
			WriteLine($"\n\nfn(r) =");
			Id.print();
			
			outFile.Flush();
		}

		using (StreamWriter drFile = new StreamWriter("out.dr.txt", true)){
			Console.SetOut(drFile);
			for(int i=0;i<npoints;i++)r[i]=dr*(i+1);
			for(int i=0;i<npoints-1;i++){
   				H[i,i]  =-2*(-0.5/dr/dr);
   				H[i,i+1]= 1*(-0.5/dr/dr);
   				H[i+1,i]= 1*(-0.5/dr/dr);
  			}
			H[npoints-1,npoints-1]=-2*(-0.5/dr/dr); /*We create K*/ 
			for(int i=0;i<npoints;i++)H[i,i]+=-1/r[i]; /*We add W*/
		
			Id.setid();
			jacobi.cyclic(H,Id);
			
			WriteLine($"{dr} {H[0,0]}");
			drFile.Flush();
		}
		
		using (StreamWriter rmaxFile = new StreamWriter("out.rmax.txt", true)){
			Console.SetOut(rmaxFile);
			for(int i=0;i<npoints;i++)r[i]=dr*(i+1);
			for(int i=0;i<npoints-1;i++){
   				H[i,i]  =-2*(-0.5/dr/dr);
   				H[i,i+1]= 1*(-0.5/dr/dr);
   				H[i+1,i]= 1*(-0.5/dr/dr);
  			}
			H[npoints-1,npoints-1]=-2*(-0.5/dr/dr); /*We create K*/ 
			for(int i=0;i<npoints;i++)H[i,i]+=-1/r[i]; /*We add W*/
			Id.setid();
			jacobi.cyclic(H,Id);
			WriteLine($"{rmax} {H[0,0]}");
			rmaxFile.Flush();
		}
		
		using (StreamWriter wfFile = new StreamWriter("out.wavefunct.txt", false)){
			Console.SetOut(wfFile);
			int numStates = 3;
			double cons = 1.0/Sqrt(dr);
			for(int i=0;i<npoints;i++)r[i]=dr*(i+1);
			for(int i=0;i<npoints-1;i++){
   				H[i,i]  =-2*(-0.5/dr/dr);
   				H[i,i+1]= 1*(-0.5/dr/dr);
   				H[i+1,i]= 1*(-0.5/dr/dr);
  			}
			H[npoints-1,npoints-1]=-2*(-0.5/dr/dr); /*We create K*/ 
			for(int i=0;i<npoints;i++)H[i,i]+=-1/r[i]; /*We add W*/
			Id.setid();
			jacobi.cyclic(H,Id);
			for(int i=0;i<npoints;i++){
				Write($"{r[i]} ");
				for(int k=0;k<numStates;k++){
					Write($"{Id[i,k]*cons} ");
				}
				WriteLine($"{1.0/Sqrt(Math.PI)*Math.Exp(-r[i])}");
			}
			wfFile.Flush();
		}

		return 0;
	}
}
