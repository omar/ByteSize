build:
	mono tools/nuget.exe restore src/ByteSizeLib.Tests/packages.config -PackagesDirectory src/packages
	xbuild src/ByteSize.sln
	dotnet restore
	dotnet build src/ByteSizeLib/project.json

test:
	mono tools/nuget.exe restore src/ByteSizeLib.Tests/packages.config -PackagesDirectory src/packages
	xbuild src/ByteSizeLib.Tests/ByteSizeLib.Tests.csproj
	mono tools/xunit/xunit.console.x86.exe src/ByteSizeLib.Tests/bin/Debug/ByteSizeLib.Tests.dll
	dotnet restore
	dotnet test src/ByteSizeLib.Tests/

package:
	xbuild src/ByteSizeLib/ByteSizeLib.csproj /p:Configuration=Release
	dotnet build src/ByteSizeLib/project.json --configuration Release
	mono tools/NuGet.exe Pack ByteSize.nuspec
