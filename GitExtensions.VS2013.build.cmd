@REM Debug build

@set msbuild="%ProgramFiles(x86)%\msbuild\12.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles%\MSBuild\12.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles%\MSBuild\14.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles(x86)%\MSBuild\15.0\Bin\MSBuild.exe"

%msbuild% /m /nr:false /p:Configuration=Debug /p:Platform="Any CPU" /v:M GitExtensions.VS2013.sln

@PAUSE