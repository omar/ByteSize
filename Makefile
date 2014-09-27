build:
	xbuild src/ByteSize.sln

test:
	xbuild src/ByteSize.Tests/ByteSize.Tests.csproj
	mono tools/xunit/xunit.console.clr4.exe src/ByteSize.Tests/bin/Debug/ByteSize.Tests.dll

package:
	xbuild src/ByteSize/ByteSize.csproj /p:Configuration=Release
	mono tools/NuGet.exe Pack ByteSIze.nuspec

