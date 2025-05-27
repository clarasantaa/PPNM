Out.txt : main.exe Makefile
	mono main.exe > $@

main.exe : solve.dll vector.dll matrix.dll minimization.dll main.cs
	mcs -target:exe -out:$@ -reference:matrix.dll -reference:vector.dll -reference:minimization.dll -reference:solve.dll main.cs

solve.dll : vector.dll minimization.dll matrix.dll solve.cs
	mcs -target:library -out:$@ -reference:minimization.dll -reference:vector.dll -reference:matrix.dll solve.cs

minimization.dll : vector.dll matrix.dll minimization.cs
	mcs -target:library -out:$@ -reference:vector.dll -reference:matrix.dll minimization.cs

matrix.dll : vector.dll matrix.cs
	mcs -target:library -out:$@ -reference:vector.dll matrix.cs

vector.dll : vector.cs
	mcs -target:library -out:$@ vector.cs
