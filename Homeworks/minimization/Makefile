Out.txt out.higgs_dense.dat : main.exe higgs.data.txt Makefile
	mono main.exe < higgs.data.txt > $@

main.exe : solve.dll vector.dll matrix.dll main.cs
	mcs -target:exe -out:$@ -reference:vector.dll -reference:matrix.dll -reference:solve.dll main.cs

solve.dll : vector.dll matrix.dll solve.cs
	mcs -target:library -out:$@ -reference:vector.dll -reference:matrix.dll solve.cs

matrix.dll : vector.dll matrix.cs
	mcs -target:library -out:$@ -reference:vector.dll matrix.cs

vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs

exB.png : higgs.data.txt out.higgs_dense.dat Makefile
	sed -i 's/,/./g' higgs.data.txt
	sed -i 's/,/./g' out.higgs_dense.dat
	
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "E [GeV]";\
		set ylabel "F(E)";\
		set title "Higgs data vs. BW fit";\
		plot "higgs.data.txt" using 1:2:3 with yerrorbars title "Experimental data",\
		"out.higgs_dense.dat" using 1:2 with lines title "Fit";\
		'| gnuplot


clean :
	$(RM) *.dll *.exe *.png Out.txt out.*.txt
