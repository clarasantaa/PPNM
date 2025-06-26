out.linear.txt : Makefile main.exe
	mono main.exe > $@
	sed -i 's/,/./g' $@

out.qspline.txt : Makefile main.exe
	mono main.exe > $@
	sed -i 's/,/./g' $@

out.cspline.txt : Makefile main.exe
	mono main.exe > $@
	sed -i 's/,/./g' $@

main.exe : main.cs vector.dll interpole.dll
	mcs -target:exe -out:$@ -reference:vector.dll -reference:interpole.dll main.cs

vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs

interpole.dll : interpole.cs vector.dll
	mcs -target:library -reference:vector.dll -out:$@ interpole.cs

exA.png : out.linear.txt Makefile
	echo '\
		set terminal png;\
		set output "$@";\
		set title "Linear Interpolation and its integral";\
		set xlabel "x";\
		set ylabel "y";\
		set key bottom right;\
		plot "$<" using 1:2 with lines title "Linear interpolation of cos(x)",\
		cos(x) with lines title "Exact cos(x)",\
		"$<" using 1:3 with lines title "Integral of linear interpolatione",\
		sin(x) with lines title "Exact sin(x)"\
		'| gnuplot

exB.png : out.qspline.txt Makefile
	echo '\
                set terminal png;\
                set output "$@";\
		set title "Quadratic Spline Interpolation, derivative and integral";\
                set xlabel "x";\
                set ylabel "y";\
		set key bottom right;\
                plot "$<" using 1:2 with lines title "Quadratic spline interpolation",\
                "$<" using 1:3 with lines title "Spline derivative",\
		"$<" using 1:4 with lines title "Spline anti-derivative",\
		'| gnuplot

exC.png : out.cspline.txt Makefile
	 echo '\
                set terminal png;\
                set output "$@";\
		set title "Cubic Spline Interpolation, derivative and integral";\
                set xlabel "x";\
                set ylabel "y";\
		set key bottom right;\
                plot "$<" using 1:2 with lines title "Cubic spline interpolation",\
                "$<" using 1:3 with lines title "Spline derivative",\
                "$<" using 1:4 with lines title "Spline anti-derivative",\
                '| gnuplot

Compare.png : out.cspline.txt out.qspline.txt out.linear.txt Makefile
	sed -i 's/,/./g' out.cspline.txt
	sed -i 's/,/./g' out.qspline.txt
	sed -i 's/,/./g' out.linear.txt
	echo '\
		set terminal png;\
                set output "$@";\
                set title "Comparison of Linear, Quadratic and Cubic Spline Interpolation";\
                set xlabel "x";\
                set ylabel "y";\
                set key bottom right;\
                plot "out.linear.txt" using 1:2 with lines lw 2 title "Linear",\
                "out.qspline.txt" using 1:2 with lines lw 2 title "Quadratic",\
                "out.cspline.txt" using 1:2 with lines lw 2 title "Cubic",\
		sin(x) with lines lc rgb "black" lw 2 title "Exact sin(x)"\
                '| gnuplot
clean :
	$(RM) *.dll *.exe *.png [Oo]ut* log.*

