using static System.Console;
using static System.Math;
using System;

class main{
	static int Main(string[] args){
		if(args.Length < 2){
			Console.WriteLine("Error: we need two arguments\n");
			return 1;
		}
		int n = int.Parse(args[0]);
		int m = int.Parse(args[1]);
		matrix A =new matrix(n,m);

		/*Create random A*/
		var rnd = new System.Random(1);
		for(int i=0;i<n;i++){
			for(int j=0;j<m;j++){
				A[i,j]= rnd.NextDouble();
			}
		}
		WriteLine($"A =");
		A.print();

		/*Factorize it into QR*/
		(matrix Q, matrix R) = QR.decomp(A);

		/*Check that R is upper triangular*/
		double sum=0;
		for(int i=1;i<m;i++){
			for(int j=0;j<i;j++){
				sum+=Abs(R[i,j]);
			}
		}
		if(sum<1e-8){
			WriteLine($"\nR is triangular");
		}else{
			WriteLine($"\nR is not triangular");
			return 0;
		}

		/*Write Q and R*/
		WriteLine($"Q =");
		Q.print();
		WriteLine($"\nR =");
		R.print();

		/*Check Q is ortogonal*/
		matrix M =new matrix(n,n);
		for(int i=0;i<n;i++){
			for(int j=0;j<n;j++){
				sum=0;
				for(int k=0;k<m;k++){
					sum+=Q[i,k]*Q[j,k];
				}
			M[i,j]=sum;
			}
		}

		matrix Qt =new matrix(m, n);
		for(int i=0;i<n;i++){
			for(int j=0;j<m;j++){
				Qt[j,i]=Q[i,j];
			}
		}

		/*PRUEBA*/
		double suma;
		matrix P =new matrix(m,m);
		for(int i=0;i<m;i++){
			for(int j=0;j<m;j++){
				suma=0;
				for(int k=0;k<n;k++){
					suma+=Qt[i,k]*Q[k,j];
				}
				P[i,j]=suma;
			}
		}
		WriteLine($"\nPRUEBA =");
		P.print();

		matrix I = matrix.identity(n);
		if(matrix.equal(Qt,Q,I)){
			WriteLine($"\nQ is ortogonal");
		}else{
			WriteLine($"\nQ is not ortogonal");
			return 0;
		}


		/*Check QR = A*/
		if(matrix.equal(Q,R,A)){
			WriteLine($"QR is A");
		}else{
			WriteLine($"QR is not A");
			return 0;
		}
		
		/*Create random square matrix*/
		matrix A2 =new matrix(n,n);
		for(int i=0;i<n;i++){
			for(int j=0;j<n;j++){
				A2[i,j]= rnd.NextDouble();
			}
		}
		WriteLine($"\nA2 =");
		A2.print();
		(matrix Q2, matrix R2)=QR.decomp(A2);
		WriteLine($"\nQ2 =");
		Q2.print();
		WriteLine($"\nR2 =");
		R2.print();
		/*Generate random b*/
		vector b =new vector(n);
		for(int i=0;i<n;i++){
			b[i]= rnd.NextDouble();
		}
		WriteLine($"b =");
		b.print();


		/*Solve QRx=b*/
		vector x=new vector(n);
		x=QR.solve(Q2,R2,b);
		WriteLine($"x =");
		x.print();

		/*Check Ax=b*/
		vector Ax =new vector(n);
		for(int i=0;i<n;i++){
			sum=0;
			for(int j=0;j<n;j++){
				sum+=A2[i,j]*x[j];
			}
			Ax[i]=sum;
		}
		WriteLine($"Ax =");
		Ax.print();
		sum=0;
		for(int i=0;i<n;i++){
			sum+=Math.Abs(Ax[i]-b[i]);
		}
		if(sum<1e-8){
			WriteLine($"Ax = b");
		}else{
			WriteLine($"Ax != b");
			return 0;
		}
		
		return 0;
	}
}



			
