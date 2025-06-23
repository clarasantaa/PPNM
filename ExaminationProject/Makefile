fft_times.exe : fft_times.cs fft.dll
	mcs -target:exe -out:$@ -reference:fft.dll fft_times.cs -r:System.Numerics.dll

fft.dll : fft.cs
	mcs -target:library -out:$@ fft.cs -r:System.Numerics.dll

out_fft_times.dat : fft_times.exe Makefile
	>$@
	>log.err
	for N in 128 256 512 1024 2048 4096 8192 16384 32768 65536 131072 262144 524288; do\
		mono fft_times.exe -size $$N 1 >> out_fft_times.dat 2>>log.err;\
	done

times.png : out_fft_times.dat
	sed -i 's/,/./g' $<
	echo '\
		set terminal png size 800,600;\
		set output "$@";\
		set title "FFT execution time vs input size (Cooley-Turkey)";\
		set xlabel "N";\
		set ylabel "Average Time (s)";\
		set grid;\
		set key left top;\
		set format y "10^{%L}";\
		\
		f(x)=a*x*log(x);\
		a=1e-10;\
		fit f(x) "$<" via a;\
		\
		plot "$<" using 1:2 with points pointtype 7 pointsize 1.2 lc rgb "blue" title "Average data",\
		f(x) with lines lw 2 lc rgb "red" title "Expected: O(N log N)";\
		'|gnuplot
