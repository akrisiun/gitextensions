
dotnet restore GitBase.sln

# %msbuild% /m /nr:false /p:Configuration=Release /p:Platform="Any CPU" /v:M /fl  GitExtensions.VS2017.sln
dotnet msbuild /m /nr:false /p:Configuration=Debug /p:Platform="Any CPU" /v:M /fl GitBase.sln
