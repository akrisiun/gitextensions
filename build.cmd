cd "%~dp0"

:modules
@REM "%ProgramFiles%\Git\bin\git.exe" submodule update --init --recursive
@REM "%ProgramFiles%\Git\bin\git.exe" submodule sync

:restore
if exist "%~dp0packages" goto build
@REM          GitExtensionsMono.sln
nuget restore GitExtensions.VS2013.sln

:build
@set msbuild="%ProgramFiles(x86)%\msbuild\14.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles%\MSBuild\14.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles(x86)%\MSBuild\12.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles%\MSBuild\12.0\Bin\MSBuild.exe"

@REM %msbuild% /m /nr:false /p:COnfiguration=Release /p:Platform="Any CPU" /v:M /fl GitExtensionsMono.sln
%msbuild% /m /nr:false /p:Configuration=Release /p:Platform="Any CPU" /v:M /fl GitExtensions.VS2013.sln

@PAUSE