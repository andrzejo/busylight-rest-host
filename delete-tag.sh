#!/usr/bin/env bash

set -eo pipefail

if [[ "$1" == "" ]]; then
    echo "Specify release version (tag)";
    exit 1
fi

echo "Delete tag '$1' "

git push --delete origin "$1"
git tag -d "$1"
