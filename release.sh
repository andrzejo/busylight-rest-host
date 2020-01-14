#!/usr/bin/env bash

set -eo pipefail

if [[ "$1" == "" ]]; then
    echo "Specify release version (tag)";
    exit 1
fi

echo "Creating release '$1' "
git push
git tag "$1"
git push origin --tags
