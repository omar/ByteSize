.PHONY: build test pack

# Check to see if Mono exists. If it does, use that path to build against .NET 4.5
ifneq ("$(wildcard /usr/local/lib/mono/)","")
	# MONO_REFERENCE_ASSEMBLIES is automatically pulled in ByteSizeLib.csproj
	# to allow building against .NET Framework on a Mac where I do most of my
	# development.
    export MONO_REFERENCE_ASSEMBLIES=/usr/local/lib/mono
endif

build:
	dotnet build src

test:
	dotnet test src

pack:
	dotnet pack src/ByteSizeLib -c Release -o pack

test-in-docker:
	# Stop and delete conatiner if already exists
	docker stop bytesize || true && docker rm bytesize || true
	# Use an image with both Mono and .NET Core SDK installed so we can build
	# against .NET Framework 4.5
	docker build -t bytesize-build .
	docker run -td --name bytesize -v $(CURDIR):/bytesize bytesize-build
	docker exec bytesize bash -c "cd /bytesize/src && msbuild -t:restore ByteSizeLib.sln"
	docker exec bytesize bash -c "cd /bytesize/src && dotnet test ByteSizeLib.Tests"
