N ?= 5
M ?= 3
Out.txt : Makefile main.exe
	mono main.exe $(N) $(M) > $@

main.exe : main.cs vm.dll
	mcs -target:exe -out:$@ -reference:vm.dll main.cs

vm.dll : vm.cs
	mcs -target:library -out:$@ vm.cs
