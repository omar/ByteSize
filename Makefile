build:
	dotnet build src

test:
	dotnet test src/ByteSize.Tests

pack:
	dotnet pack src/ByteSize -c Release  -o pack
