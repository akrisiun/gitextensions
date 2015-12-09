cd Setup
call BuildInstallers.VS2013.cmd
cd ..

msbuild "GitExtensions.VS2013.sln"

@PAUSE