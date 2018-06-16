#!/bin/bash
set -eu -o pipefail

dotnet run -p api/Api & 
dotnet test api/Api.EndToEndTest