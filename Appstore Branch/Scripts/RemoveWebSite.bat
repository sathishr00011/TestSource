c:

cd %WinDir%\System32\Inetsrv 

appcmd delete site "Grocery"

appcmd delete site "Reporting.ITesco"

appcmd delete apppool "Grocery"

IISRESET