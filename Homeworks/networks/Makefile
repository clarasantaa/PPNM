Out.txt outexA.dat outfuncionB.dat outderivativeB.dat outsecondB.dat outantiB.dat : main.exe Makefile
	mono main.exe > Out.txt

main.exe : solve.dll vector.dll matrix.dll minimization.dll solveODE.dll integration.dll main.cs
	mcs -target:exe -out:$@ -reference:matrix.dll -reference:vector.dll -reference:minimization.dll -reference:solve.dll -reference:solveODE.dll -reference:integration.dll main.cs

solve.dll : vector.dll minimization.dll matrix.dll integration.dll solve.cs 
	mcs -target:library -out:$@ -reference:minimization.dll -reference:vector.dll -reference:matrix.dll -reference:integration.dll solve.cs

minimization.dll : vector.dll matrix.dll minimization.cs
	mcs -target:library -out:$@ -reference:vector.dll -reference:matrix.dll minimization.cs

matrix.dll : vector.dll matrix.cs
	mcs -target:library -out:$@ -reference:vector.dll matrix.cs

vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs

integration.dll : integration.cs
	mcs -target:library -out:$@ integration.cs

solveODE.dll : vector.dll solveODE.cs
	mcs -target:library -out:$@ -reference:vector.dll solveODE.cs

exA.png : outexA.dat
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "x";\
		set ylabel "f(x)";\
		set title "True value vs network value";\
		f(x)=cos(5*x-1)*exp(-x*x);\
		plot "$<" using 1:2 with points title "Network data",\
		f(x) with lines title "True values";\
		'| gnuplot

functionB.png : outfuncionB.dat 
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "x";\
		set ylabel "g(x)=x³";\
		set title "True value vs network value";\
		f(x)=x*x*x;\
		plot "$<" using 1:2 with points title "Network data",\
		f(x) with lines title "True values";\
		'| gnuplot

derivativeB.png : outderivativeB.dat 
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "x";\
		set ylabel "gp(x)=3x²";\
		set title "True value vs network value";\
		f(x)=3*x*x;\
		plot "$<" using 1:2 with points title "Network data",\
		f(x) with lines title "True values";\
		'| gnuplot

secondB.png : outsecondB.dat 
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "x";\
		set ylabel "gpp(x)=6x";\
		set title "True value vs network value";\
		f(x)=6*x;\
		plot "$<" using 1:2 with points title "Network data",\
		f(x) with lines title "True values";\
		'| gnuplot

antiderivativeB.png : outantiB.dat
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "x";\
		set ylabel "G(x)=(x⁴-1)/4";\
		set title "True value vs network value";\
		f(x)=(x*x*x*x-1)/4;\
		plot "$<" using 1:2 with points title "Network data",\
		f(x) with lines title "True values";\
		'| gnuplot

clean:
	$(RM) *.dll *.exe *.png [Oo]ut*
