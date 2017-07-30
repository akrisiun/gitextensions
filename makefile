XBUILD      = /usr/bin/xbuild /v:m
XBUILD_OPT  = /verbosity:quiet  /nologo
MSBUILD     = msbuild /v:m
MONO        = mono
MONO_OPT    = --debug
NUGET       = .nuget/nuget.exe

help:
	@echo	"make run | restore | msbuild | xbuild | build | build-test"

run:
    mono GitExtensions/bin/Debug/GitExtensions.exe & 
restore:
	nuget restore GitExtensionsMono.sln
msbuild:
	msbuild  /v:m  GitExtensionsMono.sln
xbuild:
	xbuild  /v:m  GitExtensionsMono.sln
xbuild2:
	$(XBUILD)	GitExtensionsMono.sln
build:
	$(MSBUILD)	GitExtensionsMono.sln
xbuild-test:
	$(XBUILD)	GitExtensionsTest/GitExtensionsTest.csproj
