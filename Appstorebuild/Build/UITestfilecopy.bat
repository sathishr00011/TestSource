
if %1=="UITest" GOTO No1
if %1=="ServiceTest" GOTO No2
if %1=="APITest" GOTO No3

:No1
Set Source_PATH=%2
REM set Destination_PATH=\\UKDEV66TSTEX01V\UIAutomationTest

REM xcopy "%Source_PATH%\Cell46Main/Tesco/Tests/GOIAutomatedRegression" "%Destination_PATH%\Tests\GOIAutomatedRegression" /y /S /r /e

set Destination_PATH_List=(%3 %4)
for %%i in %Destination_PATH_List% do xcopy "%Source_PATH%\Cell46Main/Tesco/Tests/GOIAutomatedRegression" "%%i\Tests\GOIAutomatedRegression" /y /S /r /e 

GOTO End1
:No2

Set Source_PATH=%2
set Destination_PATH=%3
xcopy "%Source_PATH%\Cell46Main/Tesco/Tests/ServiceTests" "%Destination_PATH%\Tesco\ServiceTests" /y /S /r /e
GOTO End2

:No3
Set Source_PATH=%2
Set Destination_PATH=%3

xcopy "%Source_PATH%\Cell46Main/Tesco/Tests/ighs_test_api-master" "%Destination_PATH%\ighs_test_api-master" /y /S /r /e
GOTO End3

:End1
:End2
:End3
