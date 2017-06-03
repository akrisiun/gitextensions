XBUILD      = /usr/bin/xbuild /v:m
XBUILD_OPT  = /verbosity:quiet  /nologo
MSBUILD     = msbuild /v:m
MONO        = mono
MONO_OPT    = --debug
NUGET       = .nuget/nuget.exe

help:
	@echo	"make xbuild | build | build-test"
    
xbuild:
	$(XBUILD)	GitExtensionsMono.sln
build:
	$(MSBUILD)	GitExtensionsMono.sln
xbuild-test:
	$(XBUILD)	GitExtensionsTest/GitExtensionsTest.csproj
