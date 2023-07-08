FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["cs-learn/cs-learn.csproj", "cs-learn/"]
RUN dotnet restore "cs-learn/cs-learn.csproj"
COPY . .
WORKDIR "/src/cs-learn"
RUN dotnet build "cs-learn.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cs-learn.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cs-learn.dll"]
