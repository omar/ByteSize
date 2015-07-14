build:
	mono tools/nuget.exe restore src/ByteSize.sln
	xbuild src/ByteSize.sln

test:
	mono tools/nuget.exe restore src/ByteSize.sln
	xbuild src/ByteSizeLib.Tests/ByteSizeLib.Tests.csproj
	mono tools/xunit/xunit.console.clr4.exe src/ByteSizeLib.Tests/bin/Debug/ByteSizeLib.Tests.dll

package:
	xbuild src/ByteSizeLib/ByteSizeLib.csproj /p:Configuration=Release
	mono tools/NuGet.exe Pack ByteSize.nuspec

