@echo off
setlocal EnableDelayedExpansion

for /f "tokens=2 delims==" %%I in ('"wmic os get localdatetime /value"') do set datetime=%%I
set migrationName=Migration_!datetime:~0,8!_!datetime:~8,6!

dotnet ef migrations add !migrationName!

endlocal