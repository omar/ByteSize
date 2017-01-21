build:
	dotnet restore
	dotnet build src/ByteSizeLib

test:
	dotnet build src/ByteSizeLib
	dotnet restore
	dotnet test src/ByteSizeLib.Tests

package:
	dotnet restore
	dotnet pack src/ByteSizeLib -c Release  -o pack