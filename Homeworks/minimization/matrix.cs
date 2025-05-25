using System;
using static System.Math;
using System.Runtime.CompilerServices;
public partial class matrix{

public readonly int size1, size2;
public double[][] data;

public matrix(int n, int m){
	size1=n; size2=m; data = new double[size2][];
	for(int j=0;j<size2;j++) data[j]=new double[size1];
	}
public matrix(int n) : this(n,n) {}

public double this[int r,int c]{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	get => data[c][r];
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	set => data[c][r]=value;
	}

public vector this[int c]{
	get{return (vector)data[c];}
	set{data[c]=(double[])value;}
	}

public matrix(string s){
        string[] rows = s.Split(';');
        size1 = rows.Length;
	char[] delimiters = {',',' '};
        var options = StringSplitOptions.RemoveEmptyEntries;
        size2 = rows[0].Split(delimiters,options).Length;
        data = new double[size2][];
	for(int j=0;j<size2;j++) data[j]=new double[size1];
        for(int i=0;i<size1;i++){
                string[] ws = rows[i].Split(delimiters,options);
                for(int j=0; j<size2; j++){
                        this[i,j]=double.Parse(ws[j]);
                        }
                }
        }

public static matrix operator+ (matrix a, matrix b){
	matrix c = new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]+b[i,j];
	return c;
	}

public static matrix operator-(matrix a){
	matrix c = new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=-a[i,j];
	return c;
	}

public static matrix operator-(matrix a, matrix b){
	matrix c = new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]-b[i,j];
	return c;
	}

public static matrix operator/(matrix a, double x){
	matrix c=new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]/x;
	return c;
}

public static matrix operator*(double x, matrix a){ return a*x; }
public static matrix operator*(matrix a, double x){
	matrix c=new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]*x;
	return c;
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
                        //c[i,j]+=a[i,k]*b[k,j];
                      cj[i]+=ak[i]*bkj;
                	}
        	}
        return c;
        }

public static vector operator* (matrix a, vector v){
	var u = new vector(a.size1);
	for(int k=0;k<a.size2;k++)
	for(int i=0;i<a.size1;i++)
		u[i]+=a[i,k]*v[k];
	return u;
	}

public static vector operator% (matrix a, vector v){
	var u = new vector(a.size2);
	for(int k=0;k<a.size1;k++)
	for(int i=0;i<a.size2;i++)
		u[i]+=a[k,i]*v[k];
	return u;
	}

public matrix(vector e) : this(e.size,e.size) { for(int i=0;i<e.size;i++)this[i,i]=e[i]; }

public void set(int r, int c, double value){ this[r,c]=value; }
public static void set(matrix A, int i, int j, double value){ A[i,j]=value; }
public double get(int i, int j){ return this[i,j]; }
public static double get(matrix A, int i, int j){ return A[i,j]; }

public matrix rows(int a, int b){
  matrix m = new matrix(b-a+1,size2);
  for(int i=0;i<m.size1;i++)
	for(int j=0;j<m.size2;j++)
    		m[i,j]=this[i+a,j];
  return m;
}

public matrix cols(int a, int b){
  matrix m = new matrix(size1,b-a+1);
  for(int i=0;i<m.size1;i++)for(int j=0;j<m.size2;j++)
    m[i,j]=this[i,j+a];
  return m;
  }

public static matrix id(int n){
	var m = new matrix(n,n);
	for(int i=0;i<n;i++)m[i,i]=1;
	return m;
	}

public void set_identity(){ this.set_unity(); }
public void set_unity(){
	for(int i=0;i<size1;i++){
		this[i,i]=1;
		for(int j=i+1;j<size2;j++){
			this[i,j]=0;this[j,i]=0;
		}
	}
}
public void setid(){
	for(int i=0;i<size1;i++){
		this[i,i]=1;
		for(int j=i+1;j<size2;j++){ this[i,j]=0;this[j,i]=0; }
	}
	}

public void set_zero(){
	for(int j=0;j<size2;j++)
		for(int i=0;i<size1;i++)
			this[i,j]=0;
	}

public static matrix outer(vector u, vector v){
	matrix c = new matrix(u.size,v.size);
	for(int i=0;i<c.size1;i++)for(int j=0;j<c.size2;j++) c[i,j]=u[i]*v[j];
	return c;
}

public void update(vector u, vector v, double s=1){
	for(int i=0;i<size1;i++)
	for(int j=0;j<size2;j++)
		this[i,j]+=u[i]*v[j]*s;
	}

public matrix copy(){
	matrix c = new matrix(size1,size2);
	for(int j=0;j<size2;j++)
		for(int i=0;i<size1;i++)
			c[i,j]=this[i,j];
	return c;
	}


public matrix T{
		get{return this.transpose();}
		set{}
}

public matrix submatrix(int ia, int ib, int ja, int jb){
	matrix m = new matrix(ib-ia+1,jb-ja+1);
	for(int i=ia;i<=ib;i++)
	for(int j=ja;j<=jb;j++) m[i-ia,j-ja]=this[i,j];
	return m;
}

public matrix transpose(){
	matrix c = new matrix(size2,size1);
	for(int j=0;j<size2;j++)
		for(int i=0;i<size1;i++)
			c[j,i]=this[i,j];
	return c;
	}

public static void scale(matrix M,double x){
	for(int j=0;j<M.size2;j++)
	for(int i=0;i<M.size1;i++)
		M[i,j]*=x;
	}
public vector getColumn(int c) {
        var col = new vector(this.size1);
        for (int i = 0; i < this.size1; i++) {
            col[i] = this[i, c];
        }
        return col;
    }

public void setColumn(int c, vector col) {
        for (int i = 0; i < this.size1; i++) {
            this[i, c] = col[i];
        }
    }

public static matrix identity(int n) {
        var m = new matrix(n, n);
        for (int i = 0; i < n; i++) {
            m[i, i] = 1.0; 
        }
        return m;
    }

public void print(string s="",string format="{0,10:g3} ",
		System.IO.TextWriter file=null){
	if(file==null)file=System.Console.Out;
	file.WriteLine(s);
	for(int ir=0;ir<this.size1;ir++){
	for(int ic=0;ic<this.size2;ic++)
		file.Write(format,this[ir,ic]);
		file.WriteLine();
		}
	}

public static bool approx(double a, double b, double acc=1e-6, double eps=1e-6){
	if(Abs(a-b)<acc)return true;
	if(Abs(a-b)/Max(Abs(a),Abs(b)) < eps)return true;
	return false;
}

public bool approx(matrix B,double acc=1e-6, double eps=1e-6){
	if(this.size1!=B.size1)return false;
	if(this.size2!=B.size2)return false;
	for(int i=0;i<size1;i++)
		for(int j=0;j<size2;j++)
			if(!approx(this[i,j],B[i,j],acc,eps))
				return false;
	return true;
}

}//matrix

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
