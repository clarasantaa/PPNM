Out.txt out.circle.dat out.ellipse.dat out.relativistic.dat : main.exe Makefile
	mono main.exe > $@

main.exe : solve.dll vector.dll main.cs
	mcs -target:exe -out:$@ -reference:vector.dll -reference:solve.dll main.cs

solve.dll : vector.dll solve.cs
	mcs -target:library -out:$@ -reference:vector.dll solve.cs

vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs

exA.png : Out.txt
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "x";\
		set ylabel "y";\
		plot "$<" using 2:3 with lines title "XY",\
		"$<" using 1:2 with lines title "x(t)",\
		"$<" using 1:3 with lines title "y(t)",\
		'| gnuplot

exB.png : out.circle.dat out.ellipse.dat out.relativistic.dat
	for f in $^; do \
  		sed -i 's/,/./g' "$$f"; \
	done
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "x";\
		set ylabel "y";\
		plot "out.circle.dat" using (cos($$1)/$$2):(sin($$1)/$$2) with lines title "Circular orbit",\
		"out.ellipse.dat" using (cos($$1)/$$2):(sin($$1)/$$2) with lines title "Elliptical orbit",\
		"out.relativistic.dat" using (cos($$1)/$$2):(sin($$1)/$$2) with lines title "Relativistic orbit",\
		'| gnuplot

clean :
	$(RM) *.dll *.exe *.png [Oo]ut* log.*
