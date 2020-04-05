
# Also run:
# dotnet tool restore

if (test-path .\Trs.IntegerMapper.Tests\TestResults) {
    Remove-Item -r -force .\Trs.IntegerMapper.Tests\TestResults
}

if (test-path .\UnitTestCoverageReport) {
    Remove-Item -r -force .\UnitTestCoverageReport
}

dotnet test --collect:"XPlat Code Coverage"
dotnet tool run reportgenerator -reports:.\Trs.IntegerMapper.Tests\TestResults\*\*.xml -targetdir:.\UnitTestCoverageReport -reporttypes:Html
