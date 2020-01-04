nuget restore GitExtensions.VS2017.sln

@set msbuild="%ProgramFiles(x86)%\msbuild\15.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles%\MSBuild\14.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles(x86)%\MSBuild\12.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles%\MSBuild\12.0\Bin\MSBuild.exe"

%msbuild% /m /nr:false /p:Configuration=Release /p:Platform="Any CPU" /v:M /fl GitExtensions.VS2017.sln

@PAUSE