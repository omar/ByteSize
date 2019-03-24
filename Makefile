build:
	dotnet build src

test:
	dotnet test src

pack:
	dotnet pack src/ByteSizeLib -c Release -o pack

build-in-docker:
	# Use an image with both Mono and .NET Core SDK installed
	docker run -td --name bytesize -v $(CURDIR):/bytesize andrewlock/dotnet-mono
	docker exec bytesize bash -c "cd /bytesize/src && msbuild -t:restore ByteSizeLib.sln"
	docker exec bytesize bash -c "cd /bytesize/src && dotnet test ByteSizeLib.Tests"
