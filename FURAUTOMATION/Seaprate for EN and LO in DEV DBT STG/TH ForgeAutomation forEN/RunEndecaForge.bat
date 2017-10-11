ECHO OFF

IF [%1]==[] GOTO ECHOCOMMANDSYNTAX
IF [%2]==[] GOTO ECHOCOMMANDSYNTAX


SET BATCHFILEDIR=%~dp0 
CD /d %BATCHFILEDIR%

SET FILENAME="EndecaForgelog.txt"

ECHO [%date% %time%] Forge Started > CON
ECHO [%date% %time%] Forge Started 2>&1


SET PARTIALFORGEFILENAME=%1%\PartialForge.flag
SET FULLFORGEFILENAME=%1%\FullForge.flag

SET TOFOLDER=%2
SET REGIONLANG=%3
SET REGIONNAME="TH"

IF EXIST "%PARTIALFORGEFILENAME%" SET FORGEMODE="PARTIAL"
IF EXIST "%FULLFORGEFILENAME%" SET FORGEMODE="FULL"

IF [%FORGEMODE%]==[] GOTO NOFORGEFILEEXISTS

REM ECHO %FORGEMODE%

IF %FORGEMODE%=="FULL"  SET "FROMFOLDER=%1%\FullForge\"
IF %FORGEMODE%=="PARTIAL" SET "FROMFOLDER=%1%\Partial\"

FOR /F "delims=|" %%I IN ('DIR "%FROMFOLDER%*.zip" /B /O:D') DO SET NEWESTFILE="%%I"

SET FROMFILE=%FROMFOLDER%%NEWESTFILE%

IF [%FROMFILE%]==[] GOTO ERROR
IF [%NEWESTFILE%]==[] GOTO ERROR
IF [%TOFOLDER%]==[] GOTO ERROR

SET FROMFILE="%FROMFOLDER%%NEWESTFILE%"

ECHO [%date% %time%] Copy delta file from PI directory >> CON
ECHO [%date% %time%] Copy delta file from PI directory 2>&1
XCOPY "%FROMFILE%" "%TOFOLDER%" /S /V /Y 

SET LOCALLANGUAGEINDEXFOLDER=%TOFOLDER%\%REGIONNAME%%REGIONLANG%
SET ENGLISHINDEXFOLDER=%TOFOLDER%\%REGIONNAME%EN

IF %FORGEMODE%=="FULL" (
	SET LOCALLANGUAGEDATAFOLDER="%LOCALLANGUAGEINDEXFOLDER%\data\incoming\"
	SET ENGLISHDATAFOLDER="%ENGLISHINDEXFOLDER%\data\incoming\"
)

IF %FORGEMODE%=="PARTIAL" (
	SET LOCALLANGUAGEDATAFOLDER="%LOCALLANGUAGEINDEXFOLDER%\data\partials\incoming\"
	SET ENGLISHDATAFOLDER="%ENGLISHINDEXFOLDER%\data\partials\incoming\"
)


ECHO [%date% %time%] Extracting files for forge > CON
ECHO [%date% %time%] Extracting files for forge 2>&1

REM Delete any existing files
DEL %LOCALLANGUAGEDATAFOLDER%\*.xml 2> NUL
DEL %ENGLISHDATAFOLDER%\*.xml 2> NUL

CALL "C:\Program Files (x86)\7-Zip\7z.exe" x -y %TOFOLDER%\%NEWESTFILE% -o%ENGLISHDATAFOLDER% *.xml -r > NUL

ECHO [%date% %time%] File extraction complete > CON
ECHO [%date% %time%] File extraction complete 2>&1

IF %FORGEMODE%=="FULL" (
	ECHO [%date% %time%] Call Scripts for English language for cluster 1 - Baseline Update  >> CON
	ECHO [%date% %time%] Call Scripts for English language for cluster 1 - Baseline Update  2>&1
	CD %ENGLISHINDEXFOLDER%\CONTROL\
	set_baseline_data_ready_flag.bat
	IF %ERRORLEVEL% GEQ 1 GOTO FORGEFAIL

	baseline_generate_index.bat
	IF %ERRORLEVEL% GEQ 1 GOTO FORGEFAIL

	baseline_apply_index_cluster1.bat
	IF %ERRORLEVEL% GEQ 1 GOTO ENGCLUSTER1ERROR


	REM ECHO [%date% %time%] Extracting files for forge for Cluster 2 >> CON
	REM ECHO [%date% %time%] Extracting files for forge for Cluster 2 2>&1
	REM "C:\Program Files (x86)\7-Zip\7z.exe" x %TOFOLDER%\%NEWESTFILE% -o%ENGLISHDATAFOLDER% *.xml -r >ForgeFileList.txt
	
	REM REM Delete dummy file 
	REM DEL ForgeFileList.txt
	
	REM ECHO [%date% %time%] Call Scripts for English language for cluster 2 - Baseline Update >> CON
	REM ECHO [%date% %time%] Call Scripts for English language for cluster 2 - Baseline Update 2>&1
	REM CD %ENGLISHINDEXFOLDER%\CONTROL\
	REM set_baseline_data_ready_flag.bat
	REM IF %ERRORLEVEL% GEQ 1 GOTO FORGEFAIL

	REM baseline_generate_index.bat
	REM IF %ERRORLEVEL% GEQ 1 GOTO FORGEFAIL

	REM baseline_apply_index_cluster2.bat
REM	IF %ERRORLEVEL% GEQ 1 GOTO ENGCLUSTER2ERROR

	ECHO [%date% %time%] Forge operation completed successfully > CON
	ECHO [%date% %time%] Forge operation completed successfully  2>&1
	CD /d %BATCHFILEDIR%

	REM FORGE FLAG FILE
	DEL "%FULLFORGEFILENAME%"
	DEL %TOFOLDER%\%NEWESTFILE%

)
ECHO "Redirect output" > NUL
IF %FORGEMODE%=="PARTIAL" (
	ECHO [%date% %time%] Call Scripts for English language - Partial Update > CON
	ECHO [%date% %time%] Call Scripts for English language - Partial Update 2>&1
	CD %ENGLISHINDEXFOLDER%\CONTROL\
	set_partial_data_ready_flag.bat products
	IF %ERRORLEVEL% GEQ 1 GOTO FORGEFAIL

	partial_update.bat
	IF %ERRORLEVEL% GEQ 1 GOTO FORGEFAIL

	ECHO [%date% %time%] Forge operation completed successfully > CON
	ECHO [%date% %time%] Forge operation completed successfully  2>&1
	CD /d %BATCHFILEDIR%
	
	REM FORGE FLAG FILE
	DEL "%PARTIALFORGEFILENAME%"
	DEL %TOFOLDER%\%NEWESTFILE%
)

EXIT /B

:ERROR 
ECHO [%date% %time%] Could not find Delta file on Source folder > CON
ECHO [%date% %time%] Could not find Delta file on Source folder 2>&1
GOTO FORGEFAIL

:ECHOCOMMANDSYNTAX
ECHO [%date% %time%] "Invalid parameters supplied for the command. The syntax for the command is BatchCopy <SourceFolder> <EndecaFolder> <Region>" > CON
ECHO [%date% %time%] E.g. BatchCopy \\UKDEV66CESQL01V\DataIntegration\International\CZ\Product\Reports d:\TESCO CS > CON
ECHO [%date% %time%] "Invalid parameters supplied for the command. The syntax for the command is BatchCopy <SourceFolder> <EndecaFolder> <Region>" 2>&1
ECHO [%date% %time%] E.g. BatchCopy \\UKDEV66CESQL01V\DataIntegration\International\CZ\Product\Reports d:\TESCO CS 2>&1
GOTO LOGFILE

:ENGCLUSTER1ERROR
ECHO [%date% %time%] Stopping Cluster 1 for English > CON
ECHO [%date% %time%] Stopping Cluster 1 for English 2>&1
CALL %ENGLISHINDEXFOLDER%\CONTROL\cluster1_stop.bat
GOTO FORGEFAIL

:ENGCLUSTER2ERROR
ECHO [%date% %time%] Stopping Cluster 1 for %REGIONLANG% > CON
ECHO [%date% %time%] Stopping Cluster 1 for %REGIONLANG% 2>&1
CALL %ENGLISHINDEXFOLDER%\CONTROL\cluster2_stop.bat

:LOCALLANGCLUSTER1ERROR
ECHO [%date% %time%] Stopping Cluster 2 for English >>%FILENAME% > CON
ECHO [%date% %time%] Stopping Cluster 2 for English 2>&1
CALL %LOCALLANGUAGEINDEXFOLDER%\CONTROL\cluster1_stop.bat
GOTO FORGEFAIL

:LOCALLANGCLUSTER2ERROR
ECHO [%date% %time%] Stopping Cluster 2 for %REGIONLANG% > CON
ECHO [%date% %time%] Stopping Cluster 2 for %REGIONLANG% 2>&1
CALL %LOCALLANGUAGEINDEXFOLDER%\CONTROL\cluster2_stop.bat
GOTO FORGEFAIL

:FORGEFAIL
ECHO [%date% %time%] Error running forge! > CON
ECHO [%date% %time%] Error running forge! 2>&1
GOTO LOGFILE

:NOFORGEFILEEXISTS
ECHO [%date% %time%] Forge file does not exist. Exiting process! > CON
ECHO [%date% %time%] Forge file does not exist. Exiting process! 2>&1
GOTO LOGFILE

:LOGFILE
REM rename batch file
CD /d %BATCHFILEDIR%
EXIT /B
