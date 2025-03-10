using static System.Console;
using System;

class exercise{
	static int Main(string[] argv){
		int n=5; /*preditermined*/
		int argc=argv.Length;
		for(int i=0;i<argc;i++){
			if(argv[i]=="-size" && i<argc+1) n = int.Parse(argv[i+1]);
		}
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
		jacobi.cyclic(A, V);
		return 0;
	}
}
