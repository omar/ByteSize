build:
	mono tools/nuget.exe restore src/ByteSizeLib.Tests/package.config
	xbuild src/ByteSize.sln

test:
	mono tools/nuget.exe restore src/ByteSizeLib.Tests/package.config
	xbuild src/ByteSizeLib.Tests/ByteSizeLib.Tests.csproj
	mono tools/xunit/xunit.console.x86.exe src/ByteSizeLib.Tests/bin/Debug/ByteSizeLib.Tests.dll

package:
	xbuild src/ByteSizeLib/ByteSizeLib.csproj /p:Configuration=Release
	xbuild src/ByteSizeLib/ByteSizeLib.Dotnet.csproj /p:Configuration=Release
	mono tools/NuGet.exe Pack ByteSize.nuspec
