FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine-arm64v8 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CyberSecurity.csproj", "./"]
RUN dotnet restore "CyberSecurity.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "CyberSecurity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CyberSecurity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CyberSecurity.dll"]
