using static System.Console;
using static System.Math;
using System.Numerics;

public class funct{

        private int i, j;

        public funct(){
                i=j=1;
        }

        public bool approx(Complex a, Complex b, double acc=1e-9, double eps=1e-9){
                if(Complex.Abs(b-a) <= acc) return true;
                if(Complex.Abs(b-a) <= Max(Complex.Abs(a),Complex.Abs(b))*eps) return true;
                return false;
        }
}

