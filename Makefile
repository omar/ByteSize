build:
	dotnet build src

test:
	dotnet test src/ByteSizeLib.Tests

pack:
	dotnet pack src/ByteSizeLib -c Release -o pack
