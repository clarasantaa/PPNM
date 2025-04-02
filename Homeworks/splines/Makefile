Out.txt : Makefile main.exe
	mono main.exe > $@
	sed -i 's/,/./g' $@

main.exe : main.cs vector.dll interpole.dll
	mcs -target:exe -out:$@ -reference:vector.dll -reference:interpole.dll main.cs

vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs

interpole.dll : interpole.cs vector.dll
	mcs -target:library -reference:vector.dll -out:$@ interpole.cs

exA.png : Out.txt
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "x";\
		set ylabel "y";\
		plot "$<" index 0 using 1:2 with lines title "interpolation",\
		cos(x) with lines title "cos(x)",\
		"$<" using 1:3 with lines title "anti-derivative",\
		sin(x) with lines title "sin(x)"\
		'| gnuplot

exB.png : Out.txt
	echo '\
                set terminal png;\
                set output "$@";\
                set xlabel "x";\
                set ylabel "y";\
                plot "$<" index 1 using 1:2 with lines title "interpolation",\
                "$<" index 1 using 1:3 with lines title "derivative",\
		"$<" index 1 using 1:4 with lines title "anti-derivative",\
		'| gnuplot

exC.png : Out.txt
	 echo '\
                set terminal png;\
                set output "$@";\
                set xlabel "x";\
                set ylabel "y";\
                plot "$<" index 2 using 1:2 with lines title "interpolation",\
                "$<" index 2 using 1:3 with lines title "derivative",\
                "$<" index 2 using 1:4 with lines title "anti-derivative",\
                '| gnuplot

