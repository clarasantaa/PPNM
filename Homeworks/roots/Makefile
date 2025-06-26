Out.txt : main.exe Makefile
	mono main.exe > $@

out.hydrogen.dat : main.exe 
	mono main.exe > $@

out.rmax_12.dat out.rmax_10.dat out.rmax_8.dat out.rmax_6.dat out.rmax_4.dat : main.exe
	mono main.exe > $@
	
out.rmin_1E-05.dat out.rmin_1E-06.dat out.rmin_1E-07.dat : main.exe
	mono main.exe > $@

out.acc_1E-05.dat out.acc_1E-06.dat out.acc_1E-07.dat : main.exe
	mono main.exe > $@

out.eps_1E-05.dat out.eps_1E-06.dat out.eps_1E-07.dat : main.exe
	mono main.exe > $@

main.exe : solve.dll solveODE.dll vector.dll matrix.dll main.cs
	mcs -target:exe -out:$@ -reference:solveODE.dll -reference:vector.dll -reference:matrix.dll -reference:solve.dll main.cs

solve.dll : vector.dll matrix.dll solveODE.dll solve.cs
	mcs -target:library -out:$@ -reference:vector.dll -reference:matrix.dll -reference:solveODE.dll solve.cs

matrix.dll : vector.dll matrix.cs
	mcs -target:library -out:$@ -reference:vector.dll matrix.cs

vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs

solveODE.dll : vector.dll solveODE.cs
	mcs -target:library -out:$@ -reference:vector.dll solveODE.cs

exB.png : out.hydrogen.dat
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "r (Bohr)";\
		set ylabel "f(r)";\
		set title "Schrodinger equation solutions";\
		plot "$<" using 1:2 with lines title "Numeric Solution",\
		"$<" using 1:3 with lines title "Exact Solution"\
		'| gnuplot

rmax.png : out.rmax_12.dat out.rmax_10.dat out.rmax_8.dat out.rmax_6.dat out.rmax_4.dat
	sed -i 's/,/./g' out.rmax_12.dat out.rmax_10.dat out.rmax_8.dat out.rmax_6.dat out.rmax_4.dat
	echo '\
                set terminal png;\
                set output "$@";\
                set xlabel "r (Bohr)";\
                set ylabel "f(r)";\
                set title "Effect of rmax on wavefunction";\
                plot "out.rmax_12.dat" using 1:2 with lines title "rmax = 12",\
                "out.rmax_10.dat" using 1:2 with lines title "rmax = 10",\
                "out.rmax_8.dat" using 1:2 with lines title "rmax = 8",\
                "out.rmax_6.dat" using 1:2 with lines title "rmax = 6",\
                "out.rmax_4.dat" using 1:2 with lines title "rmax = 4"\
                '| gnuplot

rmin.png : out.rmin_0,0001.dat out.rmin_0,001.dat out.rmin_1E-05.dat out.rmin_1E-06.dat out.rmin_1E-07.dat 
	sed -i 's/,/./g' out.rmin_0,0001.dat out.rmin_0,001.dat out.rmin_1E-05.dat out.rmin_1E-06.dat out.rmin_1E-07.dat
	
	echo '\
                set terminal png;\
                set output "$@";\
                set xlabel "r (Bohr)";\
                set ylabel "f(r)";\
                set title "Effect of rmin on wavefunction";\
                plot "out.rmin_1E-07.dat" using 1:2 with lines title "rmin = 1e-7",\
                "out.rmin_1E-06.dat" using 1:2 with lines title "rmin = 1e-6",\
                "out.rmin_1E-05.dat" using 1:2 with lines title "rmin = 1e-5",\
                "out.rmin_0,0001.dat" using 1:2 with lines title "rmin = 1e-4",\
                "out.rmin_0,001.dat" using 1:2 with lines title "rmin = 1e-3"\
                '| gnuplot

acc.png : out.acc_0,0001.dat out.acc_0,001.dat out.acc_1E-05.dat out.acc_1E-06.dat out.acc_1E-07.dat 
	sed -i 's/,/./g' out.acc_0,0001.dat out.acc_0,001.dat out.acc_1E-05.dat out.acc_1E-06.dat out.acc_1E-07.dat
	
	echo '\
                set terminal png;\
                set output "$@";\
                set xlabel "r (Bohr)";\
                set ylabel "f(r)";\
                set title "Effect of acc on wavefunction";\
                plot "out.acc_1E-07.dat" using 1:2 with lines title "acc = 1e-7",\
                "out.acc_1E-06.dat" using 1:2 with lines title "acc = 1e-6",\
                "out.acc_1E-05.dat" using 1:2 with lines title "acc = 1e-5",\
                "out.acc_0,0001.dat" using 1:2 with lines title "acc = 1e-4",\
                "out.acc_0,001.dat" using 1:2 with lines title "acc = 1e-3"\
                '| gnuplot


eps.png : out.eps_0,0001.dat out.eps_0,001.dat out.eps_1E-05.dat out.eps_1E-06.dat out.eps_1E-07.dat 
	sed -i 's/,/./g' out.eps_0,0001.dat out.eps_0,001.dat out.eps_1E-05.dat out.eps_1E-06.dat out.eps_1E-07.dat
	
	echo '\
                set terminal png;\
                set output "$@";\
                set xlabel "r (Bohr)";\
                set ylabel "f(r)";\
                set title "Effect of eps on wavefunction";\
                plot "out.eps_1E-07.dat" using 1:2 with lines title "eps = 1e-7",\
                "out.eps_1E-06.dat" using 1:2 with lines title "eps = 1e-6",\
                "out.eps_1E-05.dat" using 1:2 with lines title "eps = 1e-5",\
                "out.eps_0,0001.dat" using 1:2 with lines title "eps = 1e-4",\
                "out.eps_0,001.dat" using 1:2 with lines title "eps = 1e-3"\
                '| gnuplot

clean :
	$(RM) *.dll *.exe *.png Out.txt out.*.txt
