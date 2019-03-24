build:
	dotnet build src

test:
	dotnet test src

pack:
	dotnet pack src/ByteSizeLib -c Release -o pack

build-in-docker:
	# Use an image with both Mono and .NET Core SDK installed
	docker run -td --name bytesize -v $(CURDIR):/bytesize andrewlock/dotnet-mono
	docker exec bytesize bash -c "cd /bytesize/src && msbuild ByteSizeLib.sln"
	docker exec bytesize bash -c "cd /bytesize && dotnet test src"
	docker container stop bytesize
	docker container rm bytesize
