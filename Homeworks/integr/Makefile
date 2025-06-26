Out.txt : Makefile main.exe
	mono main.exe > $@

out.erf.txt : main.exe
	mono main.exe > $@
	sed -i 's/,/./g' $@

out.erfplot.dat : main.exe
	mono main.exe > $@
	sed -i 's/,/./g' $@

main.exe : main.cs integration.dll
	mcs -target:exe -out:$@ -reference:integration.dll main.cs

integration.dll : integration.cs
	mcs -target:library -out:$@ integration.cs

erf_vs_acc.png : out.erf.txt
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set title "Absolute error erf(1) vs. acc";\
		set logscale xy;\
		set xlabel "acc";\
		set ylabel "err";\
		set grid;\
		plot "$<" with linespoint title "Error in erf(1)",\
		'| gnuplot

erf_comparison.png : out.erfplot.dat Makefile
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set title "erf(z): Computed vs. Tabulated";\
		set xlabel "z";\
		set ylabel "erf(z)";\
		set grid;\
		plot "$<" using 1:2 with lines lw 2 title "Computed",\
		     "$<" using 1:3 with lines lw 2 title "Tabulated";\
		'| gnuplot

erf_error.png : out.erfplot.dat
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set title "Absolute Error: |Computed - Tabulated|";\
		set xlabel "z";\
		set ylabel "abs error";\
		set grid;\
		set logscale y;\
		plot "$<" using 1:(abs($$2 - $$3)) with linespoints title "abs error"\
		' | gnuplot

clean :
	$(RM) *.dll *.exe *.png Out.txt out.*.txt
