:: ****************** Prompt for elevated permissions *************************
:: This makes the batch file prompt for elevated permissions on Windows Vista
:: or Windows 7, then re-run itself
VER | FINDSTR /IL "5.1." > NUL
IF %ERRORLEVEL% EQU 0 SET Version=XP
VER | FINDSTR /IL "5.2." > NUL
IF %ERRORLEVEL% EQU 0 SET Version=2003
VER | FINDSTR /IL "6.0." > NUL
IF %ERRORLEVEL% EQU 0 SET Version=Vista
VER | FINDSTR /IL "6.1." > NUL
IF %ERRORLEVEL% EQU 0 SET Version=7
If "%Version%"=="XP" GoTo SkipElevation
If "%Version%"=="2003" GoTo SkipElevation
PushD "%~dp0"
If Exist "%~0.ELEVATED" GoTo SkipElevation
:: Have to escape double quotes because they are passed to Cmd.exe via ShellExecute
Set CMD_Args=%0 %*
Set CMD_Args=%CMD_Args:"="%
Set ELEVATED_CMD=PowerShell -noexit -Command (New-Object -com 'Shell.Application').ShellExecute('Cmd.exe', '/C %CMD_Args%', '', 'runas')
Echo %ELEVATED_CMD% >> "%~0.ELEVATED"
:: If there are single quotes in the arguments, this will fail
Call %ELEVATED_CMD%
Exit
:SkipElevation
If Exist "%~0.ELEVATED" Del "%~0.ELEVATED"
:: ****************************************************************************
PowerShell.exe Set-ExecutionPolicy Unrestricted
PowerShell.exe -noexit .\IGHSLatestBuild.ps1
