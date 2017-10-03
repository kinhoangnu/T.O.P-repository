@echo off
setlocal
:: Version 5
pushd %~dp0
SET CACHED_NUGET=%LocalAppData%\NuGet\NuGet.exe
SET NUGET_PATH=.nuget
SET NUGET=%NUGET_PATH%\NuGet.exe
SET REPOSITORY_PATH=packages
SET NUGET_UTIL_PACKAGE=nuget-util
SET ARGS=%*
SET SCRIPTNAME="%REPOSITORY_PATH%\%NUGET_UTIL_PACKAGE%\build_package.ps1"
SET UPDATEFLAG=F
SET CLEANFLAG=F

IF "%~1"=="-update" (
	SET UPDATEFLAG=T
)
IF "%~1"=="-Update" (
	SET UPDATEFLAG=T
)
IF "%~1"=="-clean" (
	SET CLEANFLAG=T
)
IF "%~1"=="-Clean" (
	SET CLEANFLAG=T
)

IF "%UPDATEFLAG%"=="T" (

	IF EXIST "%NUGET%" (
		del "%NUGET%"
	)
	IF EXIST "%CACHED_NUGET%" (
		del "%CACHED_NUGET%"
	)	
	IF EXIST "%REPOSITORY_PATH%\%NUGET_UTIL_PACKAGE%" (
		rd /Q /S "%REPOSITORY_PATH%\%NUGET_UTIL_PACKAGE%"
	)
)

IF EXIST "%CACHED_NUGET%" goto copynuget
echo Downloading latest version of NuGet.exe...
IF NOT EXIST %LocalAppData%\NuGet md %LocalAppData%\NuGet
powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe' -OutFile '%CACHED_NUGET%'"

:copynuget
IF EXIST %NUGET% goto restore
IF NOT EXIST %NUGET_PATH% md %NUGET_PATH%
copy %CACHED_NUGET% %NUGET% > nul

:restore
IF EXIST "%REPOSITORY_PATH%\%NUGET_UTIL_PACKAGE%" goto build-package
%NUGET% install %NUGET_UTIL_PACKAGE% -ExcludeVersion -o %REPOSITORY_PATH% -nocache -NonInteractive

:build-package
:: Delegate to the package script from the create-packages package to create packages and optionally push them.
powershell -Version 3.0 -NoLogo -ExecutionPolicy Unrestricted -Command "& { $ErrorActionPreference = 'Stop'; & '%SCRIPTNAME%' @args; [Environment]::Exit($LASTEXITCODE) }" %ARGS%

IF "%CLEANFLAG%"=="T" (
	echo Cleaning resources...
	:: remove .nuget\nuget.exe
	IF EXIST "%NUGET%" (
		echo Cleaning resource "%NUGET%"
		del "%NUGET%"
	)
	:: remove .nuget when empty
	IF EXIST "%NUGET_PATH%" (
		echo Cleaning resources "%NUGET_PATH%"
		rd "%NUGET_PATH%
	)
	 
	:: remove packages folder
	IF EXIST "%REPOSITORY_PATH%" (
		echo Cleaning resources "%REPOSITORY_PATH%"
		rd /Q /S "%REPOSITORY_PATH%"
	)
)

popd
EXIT /B %ERRORLEVEL%