export Debug="Debug"
msbuild ../Gravatar/Gravatar.csproj -v:m /p:GenerateFullPaths=true;Configuration=Debug
cp ../Gravatar/bin/$Debug/Gravatar.dll bin/$Debug/
msbuild -v:m /p:GenerateFullPaths=true;Configuration=Debug

cp ../packages/System.IO.Abstractions.2.0.0.144/lib/net40/System.IO.Abstractions.dll bin/Debug
cp ../packages/System.Collections.Immutable.1.3.1/lib/netstandard1.0/System.Collections.Immutable.dll bin/Debug
cp ../packages/Newtonsoft.Json.10.0.3/lib/net45/Newtonsoft.Json.dll bin/Debug
cp ../Bin/*.dll  bin/Debug

# System.Reactive.3.1.1                           System.Reactive.Core.3.1.1
# System.Reactive.Interfaces.3.1.1          System.Reactive.Linq.3.1.1
# System.Reactive.PlatformServices.3.1.1
# RestSharp.105.2.3
