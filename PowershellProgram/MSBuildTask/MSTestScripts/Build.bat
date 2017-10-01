set PATH=%PATH%;D:\MSBuildTask\Nant\nant-0.85\bin;

nant -buildfile:Unittest.build -l:Logfiles/Unittest.log
 
Exit