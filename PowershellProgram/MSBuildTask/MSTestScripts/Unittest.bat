@echo off

set resultPath=%1
set solutionPath=%2

IF %1==[] goto exitLabel
IF %2==[] goto exitLabel

IF exist %resultPath%\TestResults\ (
  del %resultPath%\TestResults\*.* /s /q /f 
  rd %resultPath%\TestResults /s /q  
)
mkdir %resultPath%\TestResults

powershell -file "D:\MSBuildTask\MSTestScripts\Unittest.ps1" %solutionPath%

ping 127.0.0.1 -n 2 > nul

REM powershell -file "D:\CodeCoverage\FxCopSonarArgs.ps1" %solutionPath%

ping 127.0.0.1 -n 2 > nul

:exitLabel
exit /b