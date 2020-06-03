
# Also run:
# dotnet tool restore

if (test-path .\Trl.IntegerMapper.Tests\TestResults) {
    Remove-Item -r -force .\Trl.IntegerMapper.Tests\TestResults
}

if (test-path .\UnitTestCoverageReport) {
    Remove-Item -r -force .\UnitTestCoverageReport
}

dotnet test --collect:"XPlat Code Coverage"
dotnet tool run reportgenerator -reports:.\Trl.IntegerMapper.Tests\TestResults\*\*.xml -targetdir:.\UnitTestCoverageReport -reporttypes:Html
