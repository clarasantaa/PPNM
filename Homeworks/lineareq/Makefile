all: Out.txt exC.png
N ?= 5
M ?= 3
Out.txt : Makefile main.exe
	mono main.exe $(N) $(M) > $@

main.exe : main.cs vm.dll
	mcs -target:exe -out:$@ -reference:vm.dll main.cs

vm.dll : vm.cs
	mcs -target:library -out:$@ vm.cs

exC.exe : exC.cs vm.dll
	mcs -target:exe -out:$@ -reference:vm.dll exC.cs

out.times.data : exC.exe
	>$@
	>log.err
	for N in $$(seq 400 20 600); do \
		time --format "$$N %e" --output $@ --append \
		mono $< -size $$N 1>>out 2>>log.err ;\
	done

exC.png: out.times.data Makefile
	echo '\
		set terminal png;\
		set output "exC.png";\
		set xlabel "N";\
		set ylabel "Time";\
		a=1; b=3 ;\
		f(x)=a*x**b ;\
		fit f(x) "$<" via a,b ;\
		plot "$<" with points \
		,f(x) with lines \
		' | gnuplot

#			,f(x) \
