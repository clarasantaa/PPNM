Out.txt : Makefile main.exe 
	>$@
	echo "" | mono main.exe -numbers:1,2,3,4,5 >> $@
	echo "-numbers:1,2,3,4,5" > inputfile
	echo "" | mono main.exe $$(cat inputfile) >> $@
	echo "1 2 3 4 5" | mono main.exe 2>> $@

main.exe : main.cs
	mcs -target:exe -out:main.exe main.cs


