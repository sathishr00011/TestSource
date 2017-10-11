set PATH=%PATH%;D:\Software\Nant\nant-0.85\bin;

REM nant -buildfile:iFormdefault_CCNet.build -l:Logfiles/iForms_Manual_build.log
 nant -buildfile:Emailpublish.build -l:Logfiles/Emailpublish.log

Exit


