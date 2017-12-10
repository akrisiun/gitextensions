
dotnet restore conemu-inside/ConEmuWinForms/ConEmuWinFormsLib.csproj
# dotnet build   conemu-inside/ConEmuWinForms/ConEmuWinFormsLib.csproj -o ../../bin
dotnet msbuild   conemu-inside/ConEmuWinForms/ConEmuWinFormsLib.csproj /p:Configuration=Debug

nuget  pack    conemu-inside/ConEmuWinForms\Package.nuspec