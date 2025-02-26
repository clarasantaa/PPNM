Out.txt : main.exe Makefile
	mono main.exe > $@
	sed -i 's/,/./g' $@
	echo "\n" >> $@
	echo "0 0\n0.04 0.045111106\n-0.04 -0.045111106	" >> $@
	echo "\
		set terminal png;\
		set output 'error.png';\
		set xlabel 'x';\
		set ylabel 'Error Function';\
		set key bottom right;\
		plot '$@' index 0 with lines title 'error', '$@' index 2 with points title 'points';\
		" | gnuplot

	echo "\
                set terminal png;\
                set output 'gamma.png';\
                set xlabel 'x';\
                set ylabel 'Gamma Function';\
                set key bottom right;\
                plot '$@' index 1 with lines title 'gamma;\
                " | gnuplot


main.exe : main.cs error.dll
	mcs -target:exe -out:main.exe -reference:error.dll main.cs

error.dll : error.cs
	mcs -target:library -out:error.dll error.cs

