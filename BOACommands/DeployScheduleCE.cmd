echo off

IF "%~1"=="BackOffice" GOTO No1


GOTO No4
 

:No1

  ECHO "BackOffice Deployment"

  Call IGHSBOADEPLOYMENT_CE.bat "%~2" "%~3" "%~4" >Logfiles\CE.txt

GOTO End1



:No4

  ECHO "No match found"

  set errorlevel=2            

 

:End1


echo %errorlevel%
