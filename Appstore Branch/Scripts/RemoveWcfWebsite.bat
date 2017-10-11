c:

cd %WinDir%\System32\Inetsrv 

appcmd delete site "AppStore"

appcmd delete apppool "AppStore"

IISRESET