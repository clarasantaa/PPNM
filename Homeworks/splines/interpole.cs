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
