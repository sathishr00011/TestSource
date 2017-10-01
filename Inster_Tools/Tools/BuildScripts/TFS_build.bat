set PATH=%PATH%;C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC;

set builddefinition=%1
set TPCName=%2


call vcvarsall.bat > Logfiles\tfsmanifest.txt
TFSBuild start /collection:"http://172.25.56.27:80/tfs/Grocery" /builddefinition:"%TPCName%\%builddefinition%"

