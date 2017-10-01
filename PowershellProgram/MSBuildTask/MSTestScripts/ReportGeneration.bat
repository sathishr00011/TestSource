"D:\OpenCover\OpenCover.Console.exe" ^
-register:user ^
-target:"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\mstest.exe" ^
-targetargs:"/testcontainer:\"D:\JenkinsSlave\workspace\Rewards\Product\UnitTests\EntityFrameworkImpl.Tests\bin\Debug\EntityFrameworkImpl.Tests.dll\" /resultsfile:\"D:\Buildoutput\Product\A-JV37_DVGEOBUILD013UK 2016-07-18 13_51_12.trx\"" ^
-filter:"+[BowlingSPAService*]* -[BowlingSPAService.Tests]* -[*]BowlingSPAService.RouteConfig" ^
-mergebyhash ^
-skipautoprops ^
-output:"D:\Buildoutput\Product\TestResults\GeneratedReports\CoverageReport.xml"


