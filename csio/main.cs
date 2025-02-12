using static System.Console;
using static System.Math;
using System;
class main{
	public static int Main(string[] args){
		foreach(string arg in args){
		       var words = arg.Split(':');
		       if(words[0]=="-numbers"){
			       var numbers=words[1].Split(',');
			       foreach(var number in numbers){
				       double x = double.Parse(number);
				       Write($"{x} {Sin(x):F6} {Cos(x):F6}\n");
			       }
		       }
		}
		char[] split_delimiters = {' ', '\t', '\n'};
		var split_options = StringSplitOptions.RemoveEmptyEntries;
		for(string line = ReadLine(); line != null; line = ReadLine()){
			var numbers = line.Split(split_delimiters, split_options);
			foreach(var number in numbers){
				double x = double.Parse(number);
				Error.WriteLine($"{x} {Sin(x):F6} {Cos(x)}");
			}
		}
		return 0;
	}
}
