build:
	dotnet build src

test:
	dotnet test src

pack:
	dotnet pack src/ByteSizeLib -c Release -o pack

build-in-docker:
	# Stop and delete conatiner if already exists
	docker stop bytesize || true && docker rm bytesize || true
	# Use an image with both Mono and .NET Core SDK installed so we can build
	# against .NET Framework 4.5
	docker build -t bytesize-build .
	docker run -td --name bytesize -v $(CURDIR):/bytesize bytesize-build
	docker exec bytesize bash -c "cd /bytesize/src && msbuild -t:restore ByteSizeLib.sln"
	docker exec bytesize bash -c "cd /bytesize/src && dotnet test ByteSizeLib.Tests"
