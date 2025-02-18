using static System.Console;
using static System.Math;
using System.Numerics;
using System;
class main{
        static int Main(){
                funct obj = new funct();
                Complex d;

                d=Complex.Sqrt(-1);
                WriteLine($"Sqrt(-1) = {d}");
                WriteLine($"Sqrt(-1) = i? {obj.approx(d, Complex.Sqrt(-1))}");
                WriteLine($"Sqrt(-1) = -i? {obj.approx(d,-Complex.Sqrt(-1))}\n");

                WriteLine($"Sqrt(i) = e⁽iPi/4)? {obj.approx(Complex.Sqrt(d), Complex.Exp(d*Math.PI/4))}");
                WriteLine($"Sqrt(i) = cos(Pi/4) + isin(Pi/4)? {obj.approx(Complex.Sqrt(d), Cos(Math.PI/4)+d*Sin(Math.PI/4))}");
                WriteLine($"Sqrt(i) =1/Sqrt(2) + i/Sqrt(2)? {obj.approx(Complex.Sqrt(d), 1/Sqrt(2)+d/Sqrt(2))}\n");

                WriteLine($"Ln(i) = i*Pi/2? {obj.approx(Complex.Log(d), d*Math.PI/2)}\n");

                WriteLine($"i^i = e⁽-Pi/2)? {obj.approx(Complex.Pow(d,d), Complex.Exp(-Math.PI/2))}\n");

                return 0;
        }
}
