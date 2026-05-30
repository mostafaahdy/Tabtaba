FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Tabtaba.sln"
RUN dotnet build "Tabtaba.sln" -c Release

FROM build AS publish
WORKDIR "/src/Tbtba.API"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["sh", "-c", "dotnet $(ls Tbtba.API.dll Tabtaba.Web.dll 2>/dev/null | head -n 1)"]