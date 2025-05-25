Out.txt : main.exe higgs.data.txt Makefile
	mono main.exe < higgs.data.txt > $@

main.exe : solve.dll vector.dll matrix.dll main.cs
	mcs -target:exe -out:$@ -reference:vector.dll -reference:matrix.dll -reference:solve.dll main.cs

solve.dll : vector.dll matrix.dll solve.cs
	mcs -target:library -out:$@ -reference:vector.dll -reference:matrix.dll solve.cs

matrix.dll : vector.dll matrix.cs
	mcs -target:library -out:$@ -reference:vector.dll matrix.cs

vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs

exB.png : higgs.dat higgs.data.txt Makefile
	sed -i 's/,/./g' higgs.dat
	sed -i 's/,/./g' higgs.data.txt
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "E";\
		set ylabel "F(E)";\
		set title "Higgs data vs. BW fit";\
		plot "higgs.data.txt" using 1:2:3 with yerrorbars title "Experimental data",\
		"higgs.dat" using 1:2 with lines title "Fit"\
		'| gnuplot
