build:
	dotnet build src

test:
	dotnet test src

package:
	dotnet pack src/ByteSizeLib -c Release -o pack