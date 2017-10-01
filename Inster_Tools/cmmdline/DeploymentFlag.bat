set PATH=%PATH%;D:\Software\Nant\nant-0.85\bin;C:\Program Files (x86)\Nolio\NolioCLI;C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC;D:\cmmdline;

for /f %%i in ('findstr BOA Logfiles\DeploymentKey.txt') do set revision=%%i
set str=%revision%
echo.%str%

for /f %%j in ('findstr LOG Logfiles\DeploymentKey.txt') do set vision=%%j
set st=%vision%
echo.%st%



IF BOA==%str% goto label1
IF LOG==%st% goto label2
goto end

 :label1
 echo label1
 call BOA.bat
 
 :label2
 echo label2
 call Login.bat

:end
 end 


