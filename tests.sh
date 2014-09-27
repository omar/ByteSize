#!/bin/sh
xbuild src/ByteSize.Tests/ByteSize.Tests.csproj
mono tools/xunit/xunit.console.clr4.exe src/ByteSize.Tests/bin/Debug/ByteSize.Tests.dll