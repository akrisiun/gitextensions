"%ProgramFiles%\Git\bin\git.exe" submodule update --init --recursive
"%ProgramFiles%\Git\bin\git.exe" submodule sync
nuget restore GitExtensions.VS2015.sln

@set msbuild="%ProgramFiles(x86)%\msbuild\14.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles%\MSBuild\14.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles(x86)%\MSBuild\12.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles%\MSBuild\12.0\Bin\MSBuild.exe"

%msbuild% /m /nr:false /p:COnfiguration=Release /p:Platform="Any CPU" /v:M /fl GitExtensions.VS2015.sln