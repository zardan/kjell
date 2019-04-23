#!/bin/bash

# Set error flags
set -o nounset
set -o errexit
set -o pipefail

PROJECT=${1?Project (input) folder required.}
upm=${2?UPM (output) folder required.}

# Remove old files in upm/se.zifro.ui
echo ">>> Remove old files"
rm -rfv $upm/se.zifro.kjell/*
echo

# Copy over files
echo ">>> Move Playground UI assets"
mkdir -pv $upm/se.zifro.kjell/
cp -rv $PROJECT/Assets/Kjell/* $upm/se.zifro.kjell/
echo
