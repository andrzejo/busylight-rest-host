#!/usr/bin/env bash

set -eo pipefail

echo "Installing NuGet packages..."
nuget restore

echo "Building application..."
msbuild /t:restore
msbuild /p:Configuration=Release BusylightRestHost.sln /p:TargetFrameworkVersion=v4.7 /target:BusylightRestHost
echo "Build completed."
