using static System.Console;
using System;

public static class interpolation{
        public static double linterp(vector x, vector y, double z){
                int i=binsearch(x,z);
                if(i==-1){
                        WriteLine($"z is not in the range");
                }
                double dx=x[i+1]-x[i], dy=y[i+1]-y[i];
                if(dx<=0){
                        WriteLine($"Error in the data");
                }
                return y[i]+dy/dx*(z-x[i]);
        }

        public static int binsearch(vector x, double z){
                if(z<x[0] || z>x[x.size-1]){
                        return -1;
                }
                int i=0, j=x.size-1;
                while(j-i>1){
                        int mid=(i+j)/2;
                        if(z>x[mid]){
                                i=mid;
                        }else{
                                j=mid;
                        }
                }
                return i;
        }

        public static double linterpInteg(vector x, vector y, double z){
                double area=0;
                int i=binsearch(x,z);
                if(i==-1){
                        WriteLine($"z is not in the range");
                }
                vector p=new vector(x.size);
                for(int k=0;k<i;k++){
                        p[k]=(y[k+1]-y[k])/(x[k+1]-x[k]);
                        area+=y[k]*(x[k+1]-x[k])+p[k]*(x[k+1]-x[k])*(x[k+1]-x[k])/2;
                }
                p[i]=(y[i+1]-y[i])/(x[i+1]-x[i]);
                area+=y[i]*(z-x[i])+p[i]*(z-x[i])*(z-x[i])/2;
                return area;
        }
} 

public class qspline{
	public vector x, y, b, c;
	public qspline(vector xs, vector ys){
		this.x=xs.copy();
		this.y=ys.copy();
		int n=xs.size;

		this.b = new vector(n-1);
		this.c = new vector(n-1);
		vector p = new vector(n-1); /*slopes*/
		vector h = new vector(n-1); 
		
		for(int i=0;i<n-1;i++){
			h[i]=xs[i+1]-xs[i];
			p[i]=(ys[i+1]-ys[i])/(xs[i+1]-xs[i]);
		}

		this.c[0]=0;
		for(int i=0;i<n-2;i++){
			this.c[i+1]=(p[i+1]-p[i]-this.c[i]*h[i])/h[i+1];
		}
		this.c[n-2]/=2;
		for(int i=n-3;i>=0;i--){
			this.c[i]=(p[i+1]-p[i]-this.c[i+1]*h[i+1])/h[i];
		}

		for(int i=0;i<n-1;i++){
			this.b[i]=p[i]-this.c[i]*h[i];
		}
	}

	public double evaluate(double z){
		int n=x.size;
        	int i=binsearch(z);
                if(i==-1){
                        WriteLine($"z is not in the range");
                }
        	return y[i]+(z-x[i])*(b[i]+c[i]*(z-x[i]));
    	}

	public double derivate(double z){
		int n=x.size;
		int i=binsearch(z);
                if(i==-1){
                        WriteLine($"z is not in the range");
                }
		return b[i]+2*c[i]*(z-x[i]);
	}

	public double integral(double z){
		int n=x.size;
		int i=binsearch(z);
		if(i==-1){
			WriteLine($"z is not in the range");
		}
		double area=0;
		for(int k=0;k<i;k++){
			double dx=x[k+1]-x[k];
			area+=y[k]*dx+b[k]*dx*dx/2.0+c[k]*dx*dx*dx/3.0;
		}
		double dxi=z-x[i];
		area+=y[i]*dxi+b[i]*dxi*dxi/2.0+c[i]*dxi*dxi*dxi/3.0;
		return area;
	}
	private int binsearch(double z){
    		if(z<x[0] || z>x[x.size-1]){
        		return -1;
		}
 		int i=0, j=x.size-1;
    		while(j-i>1){
         		int mid=(i+j)/2;
         		if(z>x[mid]){
             			i=mid;
			}else{
             			j=mid;
    			}
		}
    	return i;
	}
}

public class cspline{
	private int n;
	private vector x, y, b, c, d;

	public cspline(vector xVals, vector yVals){
		n=xVals.size;
		x=xVals.copy();
		y=yVals.copy();
		b=new vector(n-1);
		c=new vector(n-1);
		d=new vector(n-1);

		vector h= new vector(n-1);
		for(int i=0;i<n-1;i++){
			h[i]=x[i+1]-x[i];
		}

		vector alpha=new vector(n-1);
		for(int i=1;i<n-1;i++){
			alpha[i]=(3/h[i])*(y[i+1]-y[i])-(3/h[i-1])*(y[i]-y[i-1]);
		}

		vector l=new vector(n);
		vector mu=new vector(n);
		vector z=new vector(n);
		l[0]=1;
		mu[0]=0;
		z[0]=0;

		for(int i=1;i<n-1;i++){
			l[i]=2*(x[i+1]-x[i-1])-h[i-1]*mu[i-1];
			mu[i]=h[i]/l[i];
			z[i]=(alpha[i]-h[i-1]*z[i-1])/l[i];
		}

		l[n-1]=1;
		z[n-1]=0;
		c[n-2]=z[n-2];

		for(int j=n-3;j>=0;j--){
			c[j]=z[j]-mu[j]*c[j+1];
			b[j]=(y[j+1]-y[j])/h[j]-h[j]*(c[j+1]+2*c[j])/3;
			d[j]=(c[j+1]-c[j])/(3*h[j]);
		}
	}

	public double evaluate(double z){
		int i=binsearch(z);
                if(i==-1){
                        WriteLine($"z is not in the range");
                }
		double dx=z-x[i];
		return y[i]+b[i]*dx+c[i]*dx*dx+d[i]*dx*dx*dx;
	}
	
	
	public double derivate(double z){
		int i=binsearch(z);
                if(i==-1){
                        WriteLine($"z is not in the range");
                }
		double dx=z-x[i];
		return b[i]+2*c[i]*dx+3*d[i]*dx*dx;
	}

	public double integral(double z){
		int i=binsearch(z);
		if(i==-1){
			WriteLine($"z is not in the range");
		}
		double area=0;
		for(int k=0;k<i;k++){
			double dx=x[k+1]-x[k];
			area+=y[k]*dx+b[k]*dx*dx/2.0+c[k]*dx*dx*dx/3.0+d[k]*dx*dx*dx*dx/4.0;
		}
		double dxi=z-x[i];
		area+=y[i]*dxi+b[i]*dxi*dxi/2.0+c[i]*dxi*dxi*dxi/3.0+d[i]*dxi*dxi*dxi*dxi/4.0;
		return area;
	}
	private int binsearch(double z){
                if(z<x[0] || z>x[x.size-1]){
                        return -1;
                }
                int i=0, j=x.size-1;
                while(j-i>1){
                        int mid=(i+j)/2;
                        if(z>x[mid]){
                                i=mid;
                        }else{
                                j=mid;
                        }
                }
        return i;
        }
}


		



			
