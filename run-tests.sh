#!/usr/bin/env bash

set -eo pipefail

echo "Installing NuGet packages..."

nuget restore

echo "Compiling sources..."

msbuild /t:clean
msbuild /t:restore
msbuild BusylightRestHost.Tests/BusylightRestHost.Tests.csproj

echo "Running tests..."

mono packages/NUnit.ConsoleRunner.3.10.0/tools/nunit3-console.exe BusylightRestHost.Tests/BusylightRestHost.Tests.csproj