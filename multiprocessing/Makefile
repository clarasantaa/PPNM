test: out1 out2 out3 out4 # make -j4 test
out1: main.exe
	$(TIME) mono $< -nthreads 2 -terms 5e8 >$@

out2: main.exe
	$(TIME) mono $< -nthreads 4 -terms 6e8 >$@ 

out3: main.exe
	$(TIME) mono $< -nthreads 2 -terms 6e8 >$@ 

out4: main.exe
	$(TIME) mono $< -nthreads 4 -terms 5e8 >$@
