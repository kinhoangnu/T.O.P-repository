set ACPTOP_PCT_HOME=%~dp0
set MSBUILD_HOME=C:\Program Files (x86)\MSBuild\14.0
set PATH=%MSBUILD_HOME%\Bin;%NUGET_HOME%;%PATH%

call %ACPTOP_PCT_HOME%\build_package.cmd -update -restore
msbuild %ACPTOP_PCT_HOME%\Your.sln /p:Configuration=Release /p:Platform="Any CPU"

pause
