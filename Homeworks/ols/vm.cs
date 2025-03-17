using static System.Console;
using static System.Math;
using System;

public class vector{
   public double[] data;
   public int size => data.Length;
   public double this[int i]{
      get => data[i];
      set => data[i]=value;
      }
   public vector(int n){
      data=new double[n];
   }
   public vector(double[] values){
      data = (double[])values.Clone();
   }
   public double norm(){
           double sum=0;
           foreach(double value in data){
                   sum+=value*value;
           }
           return Sqrt(sum);
   }
   public double dot(vector other){
           double sum=0;
           for(int i=0;i<this.size;i++){
                   sum+=this[i]*other[i];
           }
           return sum;
   }
   public static vector operator-(vector v1, vector v2){
           vector res=new vector(v1.size);
           for(int i=0;i<v1.size;i++){
                   res[i]=v1[i]-v2[i];
           }
           return res;
   }
   public static vector operator*(vector v, double c){
           vector res=new vector(v.size);
           for(int i=0;i<v.size;i++){
                   res[i]=v[i]*c;
           }
           return res;
   }
   public static vector operator/(vector v, double c){
           vector res=new vector(v.size);
           for(int i=0;i<v.size;i++){
                   res[i]=v[i]/c;
           }
           return res;
   }
   public void print(){
           for(int i=0;i<this.size;i++){
                   Write($"{this[i]:E6} ");
           }
           WriteLine();
   }

}

public class matrix{
        public readonly int size1,size2;
        private double[] data;
        public matrix(int n,int m){
                size1=n;
                size2=m;
                data = new double[size1*size2];
                }
        public double this[int i,int j]{
                get => data[i+j*size1];
                set => data[i+j*size1]=value;
        }
        public matrix copy(){
                matrix B =new matrix(this.size1, this.size2);
                for(int i=0;i<this.size1;i++){
                        for(int j=0;j<this.size2;j++){
                                B[i,j]=this[i,j];
                        }
                }
                return B;
        }
	public matrix transpose(){
		matrix B =new matrix(this.size2, this.size1);
		for(int i=0;i<this.size1;i++){
			for(int j=0;j<this.size2;j++){
				B[j,i]=this[i,j];
			}
		}
		return B;

	}
        public static matrix identity(int n){
                matrix I =new matrix(n,n);
                for(int i=0;i<n;i++){
                        I[i,i]=1.0;
                }
                return I;
        }
        public vector getColumn(int col){
                vector v =new vector(size1);
                for (int i=0;i<size1;i++){
                        v[i]=this[i,col];
                }
                return v;
        }
        public void setColumn(int col, vector v){
                for(int i=0;i<size1;i++){
                        this[i,col]=v[i];
                }
        }
        public static bool equal(matrix A, matrix B, matrix C){
                double sum=0;
                matrix AB =new matrix(A.size1,B.size2);
                for(int i=0; i<A.size1;i++){
                        for(int j=0;j<B.size2;j++){
                                sum=0;
                                for(int k=0;k<A.size2;k++){
                                        sum+=A[i,k]*B[k,j];
                                }
                        AB[i,j]=sum;
                        }
                }
                sum=0;
                for(int i=0;i<AB.size1;i++){
                        for(int j=0;j<AB.size2;j++){
                                sum+=Math.Abs(AB[i,j]-C[i,j]);
                        }
                }
                if(sum<1e-8){
                        return true;
                }else{
                        return false;
                }
        }

	 public void print(){
                for(int i=0;i<this.size1;i++){
                        for(int j=0;j<this.size2;j++){
                                Write($"{this[i,j]:E6} ");
                        }
                        WriteLine();
                }
        }
	public static matrix operator* (matrix a, matrix b){
        	var c = new matrix(a.size1,b.size2);
        	for (int k=0;k<a.size2;k++)
        	for (int j=0;j<b.size2;j++)
			{
                	double bkj=b[k,j];
                	var cj=c.data[j];
                	var ak=a.data[k];
                	int n=a.size1;
                	for (int i=0;i<n;i++){
                        	c[i,j]+=a[i,k]*b[k,j];
                      		//cj[i]+=ak[i]*bkj;
                        	}
                	}
        	return c;
        }
}

public static class QR{
        public static (matrix, matrix) decomp(matrix A){
                int m = A.size2;
                matrix Q=A.copy();
                matrix R=new matrix(m,m);
                for(int i=0;i<m;i++){
                        vector Qi = Q.getColumn(i);
                        R[i,i]=Qi.norm();
                        Qi/=R[i,i];
                        Q.setColumn(i,Qi);
                        for(int j=i+1;j<m;j++){
                                vector Qj = Q.getColumn(j);
                                R[i,j]=Qi.dot(Qj);
                                Qj-=Qi*R[i,j];
                                Q.setColumn(j,Qj);
                        }
                }
                return (Q,R);
        }

        public static vector solve(matrix Q, matrix R, vector b){
                vector x=new vector(R.size1);
                vector y=new vector(R.size1); /*y=Q^T*b*/
                for(int j=0;j<Q.size2;j++){
                        double sum=0;
                        for(int i=0;i<Q.size1;i++){
                                sum+=Q[i,j]*b[i];
                        }
                        y[j]=sum;
                }
                for(int i=R.size1-1;i>=0;i--){
                        double sum=0;
                        for(int k=i+1;k<R.size1;k++){
                                sum+=R[i,k]*x[k];
                        }
                        x[i]=(y[i]-sum)/R[i,i];
                }
                return x;
        }

        public static double det(matrix R){
                double d=1.0;
                for(int i=0;i<R.size1;i++){
                        d*=R[i,i];
                }
                return d;
        } /*Only gives the right determine if the matrix is triangular*/

        public static matrix inverse(matrix Q,matrix R){
                int n = R.size1;
                matrix Ainv =new matrix(n,n);
                matrix I = matrix.identity(n);
                for(int i=0;i<n;i++){
                        vector ei = I.getColumn(i);
                        vector x = QR.solve(Q,R,ei);
                        Ainv.setColumn(i,x);
                }
                return Ainv;
        }
}

public class LeastSquaresFit{
	public static (vector,matrix) lsfit(Func<double,double>[] fs, vector x, vector y, vector dy){
		int n = x.size, m = fs.Length;
		matrix A = new matrix(n,m);
		for(int i=0;i<n;i++){
			for(int j=0;j<m;j++){
				A[i,j]=fs[j](x[i])/dy[i];
			}
		}
		vector b = new vector(n);
		for(int i=0;i<n;i++){
			b[i]=y[i]/dy[i];
		}
		(matrix Q, matrix R)=QR.decomp(A);
		vector c=new vector(n);
		c=QR.solve(Q,R,b);
		matrix At =new matrix(n,n);
		At=A.transpose();
		matrix AtA =new matrix(n,n);
		AtA=At*A;
		(matrix Q2, matrix R2)=QR.decomp(AtA);
		matrix cov =new matrix(n,n);
		cov=QR.inverse(Q2,R2);

		return (c,cov);
	}
}














