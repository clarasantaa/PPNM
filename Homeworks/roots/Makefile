Out.txt : main.exe Makefile
	mono main.exe > $@

main.exe : solve.dll vector.dll matrix.dll main.cs
	mcs -target:exe -out:$@ -reference:vector.dll -reference:matrix.dll -reference:solve.dll main.cs

solve.dll : vector.dll matrix.dll solve.cs
	mcs -target:library -out:$@ -reference:vector.dll -reference:matrix.dll solve.cs

matrix.dll : vector.dll matrix.cs
	mcs -target:library -out:$@ -reference:vector.dll matrix.cs
vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs
