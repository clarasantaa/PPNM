Out.txt out.circle.dat out.ellipse.dat out.relativistic.dat out.rk23.dat : main.exe Makefile
	mono main.exe > $@

out.threebody.dat : main.exe
	mono main.exe > $@
	sed -i 's/,/./g' $@

out.oscillator.dat : main.exe
	mono main.exe > $@
	sed -i 's/,/./g' $@

main.exe : solve.dll vector.dll main.cs
	mcs -target:exe -out:$@ -reference:vector.dll -reference:solve.dll main.cs

solve.dll : vector.dll solve.cs
	mcs -target:library -out:$@ -reference:vector.dll solve.cs

vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs

exA.png : out.oscillator.dat
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set title "Simple Harmonic Oscillator";\
		set xlabel "Time (x)";\
		set ylabel "Displacement / Velocity";\
		plot "$<" using 2:3 with lines title "XY",\
		"$<" using 1:2 with lines title "Displacement u(x)",\
		"$<" using 1:3 with lines title "Velocity up(x)";\
		'| gnuplot

exB.png : out.circle.dat out.ellipse.dat out.relativistic.dat
	for f in $^; do \
  		sed -i 's/,/./g' "$$f"; \
	done
	echo '\
		set terminal png;\
		set output "$@";\
		set title "Orbital trajectories: Circular, Elliptical and Relativitic";\
		set xlabel "x";\
		set ylabel "y";\
		plot "out.circle.dat" using (cos($$1)/$$2):(sin($$1)/$$2) with lines title "Circular orbit",\
		"out.ellipse.dat" using (cos($$1)/$$2):(sin($$1)/$$2) with lines title "Elliptical orbit",\
		"out.relativistic.dat" using (cos($$1)/$$2):(sin($$1)/$$2) with lines title "Relativistic orbit",\
		'| gnuplot

exC.png : out.threebody.dat Makefile
	echo '\
                set terminal png;\
                set output "$@";\
		set title "Three-body Problem: Figure-8 Orbit";\
		set xlabel "x";\
                set ylabel "y";\
                plot "$<" using 1:2 with lines lw 2 title "Body 1",\
                "$<" using 3:4 with lines lw 2 title "Body 2",\
                "$<" using 5:6 with lines lw 2 title "Body 3";\
                '| gnuplot


clean :
	$(RM) *.dll *.exe *.png [Oo]ut* log.*
