FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SuperHeroAPI.csproj", "./"]
RUN dotnet restore "SuperHeroAPI.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "SuperHeroAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuperHeroAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperHeroAPI.dll"]
