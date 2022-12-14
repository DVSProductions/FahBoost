# Folding at Home Boost
This little tool will ensure that folding at home runs on it's own core.

## The issue
When working with GPU cores, folding at home will spawn a process with low CPU Priority and affinity to all cores.
This means that the GPU will be used, but the CPU will be used for other things. This is not ideal, as the GPU will be underutilized.
In order to stop the cpu from bottlenecking the GPU, we need to ensure that the CPU is not used for other things. 
## The Fix
To accomplish this, FAH Boost will pin the FAH process to a single core, and set the priority to high. 
All other processes will have their process affinity changed to unitize any other core.
To account for Hyperthreaded cores, we block two logical Cores to ensure that folding at home gets its own physical core. 
This is done because a workload on a logical core can affect the Performance of another logical core on the same physical core.
## Performance impact
This won't have much of an impact on normal workloads, as a matter of fact it can improve your general performance, because folding at home will stay out of cores you use for your browser or other normal tasks.
Full CPU workloads will suffer a bit, since the available core count will be lower.
If the tool detects that you aren't currently folding, it will automatically disable itself and allow other software access to the reserved core.

# Disclaimer
Use at your own risk. I am not responsible for any damage this tool may cause.
Some software may not respond kindly to having its CPU affinity changed.
