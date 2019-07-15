# msbuild /v:m /t:build /p:GenerateFullPaths=true

dotnet msbuild -v:m "/p:GenerateFullPaths=true;Configuration=Debug"

# dotnet msbuild -p:Configuration=Release
# -consoleloggerparameters:PerformanceSummary;NoSummary -verbosity:minimal /p:GenerateFullPaths=true