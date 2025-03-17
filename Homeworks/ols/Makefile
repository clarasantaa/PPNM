PARAMS := $(shell head -n 1 Out.txt | sed 's/,/./g')
lnA := $(word 1, $(PARAMS))
lambda := $(word 2, $(PARAMS))

Out.txt : Makefile main.exe
	mono main.exe > $@

main.exe : main.cs vm.dll
	mcs -target:exe -out:$@ -reference:vm.dll main.cs

vm.dll : vm.cs
	mcs -target:library -out:$@ vm.cs

Ra224.png: Out.txt Makefile
	echo '\
		set terminal png;\
		set output "$@";\
		set xlabel "Time (days)";\
		set ylabel "Activity";\
		f(x)=exp(lnA)*exp(-lambda*x);\
		lnA=$(lnA);\
		lambda=$(lambda);\
		T_half=log(2)/lambda; \
		set arrow from T_half, graph 0 to T_half, graph 1 nohead dt 2 lc rgb "red"; \
		set label sprintf("T_{1/2} = %.2f", T_half) at T_half, graph 1.02 center; \
		fit f(x) "< tail -n +2 Out.txt" via lnA,lambda ;\
		plot "< tail -n +2 Out.txt" using 1:2:3 with errorbars title "Experimental Data",\
		f(x) with lines title "Exponential adjust"\
		' | gnuplot
