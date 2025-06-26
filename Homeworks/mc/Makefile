Out.txt : main.exe Makefile
	mono main.exe > $@

out.circle.dat : main.exe Makefile
	mono main.exe >$@

main.exe : solve.dll vector.dll main.cs
	mcs -target:exe -out:$@ -reference:vector.dll -reference:solve.dll main.cs

solve.dll : vector.dll solve.cs
	mcs -target:library -out:$@ -reference:vector.dll solve.cs

vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs

circle.png : out.circle.dat
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set title "Monte Carlo error for unit circle area";\
		set xlabel "1/√N";\
		set ylabel "Error";\
		plot "$<" using 1:2 with lines title "Estimated Error",\
		"$<" using 1:3 with lines title "Real Error",\
		'| gnuplot

clean :
	$(RM) *.dll *.exe *.png Out.txt out.*.txt
