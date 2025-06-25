.PHONY: out.dr.txt
N ?= 5
RMAX ?= 10
DR ?= 0,3

Out.txt : main.exe Makefile
	sed -i 's/,/./g' $@
	mono main.exe $(N) $(RMAX) $(DR) > $@

main.exe : jacobi.dll matrix.dll main.cs
	mcs -target:exe -out:$@ -reference:jacobi.dll -reference:vector.dll -reference:matrix.dll main.cs

jacobi.dll : jacobi.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@ jacobi.cs

matrix.dll : matrix.cs vector.dll
	mcs -target:library -reference:vector.dll -out:$@ matrix.cs

vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs

out.dr.txt : main.exe Makefile
	> $@
	for dr in $$(seq 0.1 0.1 1); do\
		mono main.exe $(N) $(RMAX) $$dr >> $@;\
	done
	sed -i 's/,/./g' $@

out.rmax.txt : main.exe Makefile
	> $@
	for rmax in $$(seq 5 1 13); do\
		mono main.exe $(N) $$rmax $(DR) >> $@;\
	done
	sed -i 's/,/./g' $@

out.wavefunct.txt : main.exe Makefile
	> $@
	for dr in $$(seq 0.1 0.1 1); do\
		mono main.exe $(N) $(RMAX) $(DR) >> $@;\
	done
	sed -i 's/,/./g' $@

rmax.png : out.dr.txt
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "Δr";\
		set ylabel "ε0";\
		plot "$<" using 1:2 with lines title "energy convergence"\
		'| gnuplot

dr.png : out.rmax.txt
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "rmax";\
		set ylabel "ε0";\
		plot "$<" using 1:2 with lines title "energy convergence"\
		'| gnuplot

wavefunct.png : out.wavefunct.txt
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "r";\
		set key bottom right;\
		plot "$<" using 1:2 with lines title "f1" lc "blue",\
		"$<" using 1:3 with lines title "f2" lc "green",\
		"$<" using 1:4 with lines title "f3" lc "purple",\
		"$<" using 1:5 with lines title "analytic" lc "red",\
		'| gnuplot

exC.png : out.times.data Makefile
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "N";\
		set ylabel "Time";\
		set key top left;\
		f(x)=a*x**b;\
		a=1; b=3;\
		fit f(x) "$<" using 1:2 via a,b;\
		plot "$<" using 1:2 with points title "Time calculated", \
		     f(x) with lines title "f(x) = x³"\
	' | gnuplot

clean :
	$(RM) *.dll *.exe *.png Out.txt out.*.txt
