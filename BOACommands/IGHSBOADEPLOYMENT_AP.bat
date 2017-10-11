..\ExecutionRelay.cmd  -a "InternationalGrocery" -e "UK DEV66AP" -f "Component Backoffice Website Deployment" -p "%~3" -u "%~2" -s {UKDEV66APBWS01V,UKTAS01NOLAP01v} -r "{Application Parameters/Release Version,"%~1"}"

exit /b 0