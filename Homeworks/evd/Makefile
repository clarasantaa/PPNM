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
		set title "Convergence of ground state energy ε₀ vs. Δr";\
		set xlabel "Δr (Bohr)";\
		set ylabel "ε0 (Hartree)";\
		plot "$<" using 1:2 with lines title "energy convergence" lw 2;\
		'| gnuplot

dr.png : out.rmax.txt
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set title "Convergence of ground state energy ε₀ vs. rmax";\
		set xlabel "rmax (Bohr)";\
		set ylabel "ε0 (Hartree)";\
		plot "$<" using 1:2 with lines title "energy convergence" lw 2;\
		'| gnuplot

wavefunct.png : out.wavefunct.txt
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set title "Radial wave functions f(r) for hydrogen atmo";\
		set xlabel "r (Bohr)";\
		set ylabel "f(r)";\
		set key bottom right;\
		plot "$<" using 1:2 with lines lw 2 lc rgb "blue" title "Ground state (numerical)",\
		"$<" using 1:3 with lines lw 2 lc rgb "green" title "First excited state (numerical)",\
		"$<" using 1:4 with lines lw 2 lc rgb "purple" title "Second excited state (numerical)",\
		"$<" using 1:5 with lines lw 2 lc rgb "red" title "Ground state (analytical)";\
		'| gnuplot

exC.png : out.times.data Makefile
	echo '\
		set terminal png;\
		set output "$@";\
		set title "Checking diagonalization is O(N³)";\
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
