REM 
REM this file needs to 'save as' US-ASCII so it will function, default of UTF-8 will make it un-runnnable.
REM
REM NOTE: this is part of the Test project for two reasons:
REM       1) this guarantees that the nuget package has been created
REM       2) this guarantees that tests have paassed
REM 
REM NOTE: Add to PostBuild Events as:
REM       (ProjectName)DeployNugetPackage.bat $(Version) $(ProjectKey)
REM
echo     Trying to deploy nuget package to repository.

set VERSION=%1
set PROJECT=%2
REM echo VERSION=[%VERSION%]
REM echo PROJECT=[%PROJECT%]

REM DEVELOPER SPECIFIC VALUE
set REPO=C:\Users\Fred\NugetRepository

@echo off
For /f "tokens=2-4 delims=/ " %%a in ('echo %DATE%') do (set mydate=%%c%%a%%b)
For /f "tokens=1-4 delims=:." %%a in ('echo %TIME%') do (set mytime=%%a%%b%%c%%d)
set TIMESTAMP=%mydate%%mytime%
REM echo TIMESTAMP=[%TIMESTAMP%]

REM only use timestamp if this is an 'alpha' version ( if contains 'dash' )
IF x%VERSION:-=%==x%VERSION% (set TIMESTAMP=)
rem IF /i [%VERSION:~-6%]!=[-alpha] (set TIMESTAMP=)

REM copy current nupkg file into timestamped version in repository.
set FILE=..\%PROJECT%\bin\Release\%PROJECT%.%VERSION%.nupkg
set FILESTAMPED=..\%PROJECT%\bin\Release\%PROJECT%.%VERSION%%TIMESTAMP%.nupkg
copy %FILE% %FILESTAMPED%

echo CMD [nuget add %FILESTAMPED% -source %REPO%]
nuget add %FILESTAMPED% -source %REPO%

