Out.txt : main.exe Makefile
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

exB.png : hydrogen.dat
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "r";\
		set ylabel "f(r)";\
		set title "Schrodinger equation solutions";\
		plot "$<" using 1:2 with lines title "Numeric Solution",\
		"$<" using 1:3 with lines title "Exact Solution"\
		'| gnuplot

rmax.png : rmax_12.dat rmax_10.dat rmax_8.dat rmax_6.dat rmax_4.dat
	sed -i 's/,/./g' rmax_12.dat rmax_10.dat rmax_8.dat rmax_6.dat rmax_4.dat
	echo '\
                set terminal png;\
                set output "$@";\
                set xlabel "r";\
                set ylabel "f(r)";\
                set title "Changing rmax";\
                plot "rmax_12.dat" using 1:2 with lines title "rmax = 12",\
                "rmax_10.dat" using 1:2 with lines title "rmax = 10",\
                "rmax_8.dat" using 1:2 with lines title "rmax = 8",\
                "rmax_6.dat" using 1:2 with lines title "rmax = 6",\
                "rmax_4.dat" using 1:2 with lines title "rmax = 4"\
                '| gnuplot

rmin.png : rmin_0,0001.dat rmin_0,001.dat rmin_1E-05.dat rmin_1E-06.dat rmin_1E-07.dat 
	sed -i 's/,/./g' rmin_0,0001.dat rmin_0,001.dat rmin_1E-05.dat rmin_1E-06.dat rmin_1E-07.dat
	
	echo '\
                set terminal png;\
                set output "$@";\
                set xlabel "r";\
                set ylabel "f(r)";\
                set title "Changing rmin";\
                plot "rmin_1E-07.dat" using 1:2 with lines title "rmin = 1e-7",\
                "rmin_1E-06.dat" using 1:2 with lines title "rmin = 1e-6",\
                "rmin_1E-05.dat" using 1:2 with lines title "rmin = 1e-5",\
                "rmin_0,0001.dat" using 1:2 with lines title "rmin = 1e-4",\
                "rmin_0,001.dat" using 1:2 with lines title "rmin = 1e-3"\
                '| gnuplot

acc.png : acc_0,0001.dat acc_0,001.dat acc_1E-05.dat acc_1E-06.dat acc_1E-07.dat 
	sed -i 's/,/./g' acc_0,0001.dat acc_0,001.dat acc_1E-05.dat acc_1E-06.dat acc_1E-07.dat
	
	echo '\
                set terminal png;\
                set output "$@";\
                set xlabel "r";\
                set ylabel "f(r)";\
                set title "Changing acc";\
                plot "acc_1E-07.dat" using 1:2 with lines title "acc = 1e-7",\
                "acc_1E-06.dat" using 1:2 with lines title "acc = 1e-6",\
                "acc_1E-05.dat" using 1:2 with lines title "acc = 1e-5",\
                "acc_0,0001.dat" using 1:2 with lines title "acc = 1e-4",\
                "acc_0,001.dat" using 1:2 with lines title "acc = 1e-3"\
                '| gnuplot


eps.png : eps_0,0001.dat eps_0,001.dat eps_1E-05.dat eps_1E-06.dat eps_1E-07.dat 
	sed -i 's/,/./g' eps_0,0001.dat eps_0,001.dat eps_1E-05.dat eps_1E-06.dat eps_1E-07.dat
	
	echo '\
                set terminal png;\
                set output "$@";\
                set xlabel "r";\
                set ylabel "f(r)";\
                set title "Changing eps";\
                plot "eps_1E-07.dat" using 1:2 with lines title "eps = 1e-7",\
                "eps_1E-06.dat" using 1:2 with lines title "eps = 1e-6",\
                "eps_1E-05.dat" using 1:2 with lines title "eps = 1e-5",\
                "eps_0,0001.dat" using 1:2 with lines title "eps = 1e-4",\
                "eps_0,001.dat" using 1:2 with lines title "eps = 1e-3"\
                '| gnuplot
