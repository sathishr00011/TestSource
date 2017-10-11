set PATH=%PATH%;D:\Software\Nant\nant-0.85\bin;C:\Program Files (x86)\Nolio\NolioCLI;C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC;D:\cmmdline;

REM call Manifestgenerate.bat > Logfiles\tfsmanifest.txt
call DeploymentFlag.bat > Logfiles\DeploymentFlag.txt
exit
pause


