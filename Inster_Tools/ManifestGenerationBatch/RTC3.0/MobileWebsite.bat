@echo off

set searchdirectory=%1
set destinationdirectory=%2
set name1=%3
set template=%4

for /f "tokens=*" %%A in ('dir "%searchdirectory%" /AD /O-D /B') do (set recent=%%A& goto exit)
:exit


setLocal DisableDelayedExpansion

set name=%recent%


for /f "tokens=* delims= " %%G in (%template%\ManifestGenerationBatch\RTC3.0\ComponentManifest_IGHS_UK_MobileWebsite.xml) do (
set str=%%G
setLocal EnableDelayedExpansion
set str=!str:number=%name%!
set str=!str:manifest_version=%name1%!


>> %destinationdirectory%\ComponentManifest_IGHS_UK_MobileWebsite_%name1%.xml echo(!str!
    endlocal
)



