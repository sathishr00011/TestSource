for /f "tokens=4" %%i in ('findstr R8.0_ManifestGeneration_AppStore_8.0.0.%1.%2.%3 Logfiles\tfsmanifest.txt') do set revision=%%i
echo %revision%
set str=%revision:~-9%
echo.%str%
asap.cmd -u jv37 -p password -a "InternationalGrocery" -e "UK DEV66CE" -f "R8.0 IGHS Login Website Deployment with AppPoolRecycle" -s  {UKDEV66MILWS01V,uktas01nolap01v} -r "{Application Parameters/Release Version,%str%}" -n