#!/bin/sh
xbuild src/ByteSize/ByteSize.csproj /p:Configuration=Release
mono tools/NuGet.exe Pack ByteSize.nuspec