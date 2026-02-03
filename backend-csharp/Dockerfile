# C# 后端 Dockerfile

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# 复制项目文件
COPY src/ ./

# 还原依赖
RUN dotnet restore Feiyue.Api/Feiyue.Api.csproj

# 构建
RUN dotnet build Feiyue.Api/Feiyue.Api.csproj -c Release -o /app/build

# 发布
RUN dotnet publish Feiyue.Api/Feiyue.Api.csproj -c Release -o /app/publish

# 运行时镜像
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Feiyue.Api.dll"]
