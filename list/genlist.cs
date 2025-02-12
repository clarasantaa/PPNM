using static System.Console;
using static System.Math;
using System;

public class genlist<T>{
	public genlist(){
		data = new T[0];
	}
	public T[] data;
	public int size => data.Length;
	public T this[int i] => data[i];
	public void add(T item){ 
		T[] newdata = new T[size+1];
		System.Array.Copy(data,newdata,size);
		newdata[size]=item;
		data=newdata;
	}
}
