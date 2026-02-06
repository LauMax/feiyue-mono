#!/bin/bash

# Feiyue Backend Build and Test Script

echo "🔨 Building Feiyue Backend..."
cd "$(dirname "$0")"

# Build all projects
echo "📦 Building projects..."
dotnet build src/Feiyue.InternalContracts/Feiyue.InternalContracts.csproj
dotnet build src/Feiyue.User.Storage/Feiyue.User.Storage.csproj
dotnet build src/Feiyue.Match.Storage/Feiyue.Match.Storage.csproj
dotnet build src/Feiyue.Chat.Storage/Feiyue.Chat.Storage.csproj
dotnet build src/Feiyue.User/Feiyue.User.csproj
dotnet build src/Feiyue.Match/Feiyue.Match.csproj
dotnet build src/Feiyue.Chat/Feiyue.Chat.csproj
dotnet build src/Feiyue.Api/Feiyue.Api.csproj

if [ $? -eq 0 ]; then
    echo "✅ Build successful!"
    echo ""
    echo "📝 Next steps:"
    echo "1. Setup Supabase database (see database/README.md)"
    echo "2. Update appsettings.Development.json with your Supabase connection string"
    echo "3. Start Redis: docker run -d -p 6379:6379 redis:7-alpine"
    echo "4. Run API: cd src/Feiyue.Api && dotnet run"
else
    echo "❌ Build failed!"
    exit 1
fi
