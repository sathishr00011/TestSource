set PATH=%PATH%;D:\Softwares\Nant\nant-0.85\bin;

nant -buildfile:iFormdefault_CCNet.build -D:arg.sourcepath=%1 -l:Logfiles/iForms_Manual_build.log
 
Exit


