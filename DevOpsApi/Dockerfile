FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["DevOpsApi.csproj", "."]
RUN dotnet restore "DevOpsApi.csproj"

COPY . .

RUN dotnet publish "DevOpsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DevOpsApi.dll"]