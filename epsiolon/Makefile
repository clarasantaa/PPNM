Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : funct.dll main.cs
	mcs -target:exe -out:main.exe -reference:funct.dll main.cs

funct.dll : funct.cs
	mcs -target:library -out:funct.dll funct.cs

