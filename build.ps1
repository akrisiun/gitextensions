# cd "%~dp0"

#:modules
#@REM "%ProgramFiles%\Git\bin\git.exe" submodule update --init --recursive
#@REM "%ProgramFiles%\Git\bin\git.exe" submodule sync

#:restore
#if exist "%~dp0packages" goto build
dotnet restore GitExtensions.VS2017.sln

#:build
$msbuild="$env:ProgramFiles (x86)\msbuild\15.0\Bin\MSBuild.exe"

& $msbuild /m /nr:false /p:Configuration=Debug /p:Platform="Any CPU" /v:M /fl GitExtensions.VS2017.sln

# @PAUSE