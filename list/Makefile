.PHONY: Out.txt
Out.txt : main.exe
	mono main.exe > $@

genlist.dll: genlist.cs
	mcs -target:library -out:$@ genlist.cs

main.exe: main.cs genlist.dll
	mcs -target:exe -out:$@ -reference:genlist.dll main.cs

