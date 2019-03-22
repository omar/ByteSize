build:
	dotnet build src

test:
	dotnet test src

pack:
	dotnet pack src/ByteSizeLib -c Release -o pack