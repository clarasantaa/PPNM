Our objective with this Examination Project is to clean a noisy signal using the FFT (Fast Fourier transform). For this, we create the noisy signal and then clean it two ways: Amplitude filtering and Frequency filtering.

	Amplitude filtering: we discard all parts of the signal whose amplitude is not high enough. In our case, we have decided to discard components with an amplitude that is 10% or less from the maximum amplitude found.
	
	Frequency filtering: we discard all parts of the signal with higher frequencies, since it is where noise usually appears. Because the data has been through the FFT program, we know that the low frequencies are located at both ends due to FFT symetry. We send a cutoff that is a bit higher than all frequencies that compose the signal, and keep all components from 0, ..., cutoff, N-cutoff, ..., N-1

By ploting our two examples (one simpler which consist of a single frequency and one more complex which combines three) we observe that the amplitude gives better cleaning results, but not by much.

Lastly, we have eliminated the array coppying from the FFT program to optimized it. We do this by using strides and an initial index to separate even and odd elementes directly.
