build:
	dotnet restore
	dotnet build src/ByteSizeLib

test:
	dotnet restore
	dotnet build src/ByteSizeLib
	dotnet test src/ByteSizeLib.Tests

package:
	dotnet restore
	dotnet pack src/ByteSizeLib -c Release  -o pack