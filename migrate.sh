#!/bin/sh
export PATH="$PATH:/root/.dotnet"
export PATH="$PATH:/root/.dotnet/tools"

export PATH=$PATH:$HOME/.dotnet/tools
export DOTNET_ROOT=$HOME/.dotnet
export PATH=$PATH:$DOTNET_ROOT

echo "[*] Applying migrations..."

if [ "$#" -ne 1 ]; then
    echo "Usage: $0 <migration_name>"
    exit 1
fi

dotnet ef migrations add $1 --project Atlas.Infrastructure --startup-project Atlas.Presentation/Atlas.WebApi
dotnet ef database update --project Atlas.Infrastructure --startup-project Atlas.Presentation/Atlas.WebApi
