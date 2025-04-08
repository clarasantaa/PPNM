Out.txt : Makefile main.exe
	mono main.exe > $@

main.exe : main.cs integration.dll
	mcs -target:exe -out:$@ -reference:integration.dll main.cs

integration.dll : integration.cs
	mcs -target:library -out:$@ integration.cs

exA.png : Out.txt
	sed -i 's/,/./g' $<
	echo '\
		set terminal png;\
		set output "$@";\
		set logscale xy;\
		set xlabel "acc";\
		set ylabel "err";\
		set grid;\
		plot "$<" index 1 with linespoint title "Error in erf(1),\
		'| gnuplot
