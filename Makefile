build:
	dotnet restore
	dotnet build src

test:
	dotnet restore
	dotnet build src
	dotnet test src

package:
	dotnet restore
	dotnet pack src/ByteSizeLib -c Release -o pack