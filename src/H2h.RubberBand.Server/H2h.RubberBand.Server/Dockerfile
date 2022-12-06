#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["H2h.RubberBand.Server/H2h.RubberBand.Server.csproj", "H2h.RubberBand.Server/"]
COPY ["H2h.ElasticSearch.Model/H2h.ElasticSearch.Model.csproj", "H2h.ElasticSearch.Model/"]
COPY ["H2h.RubberBand.Database.Postgres/H2h.RubberBand.Database.Postgres.csproj", "H2h.RubberBand.Database.Postgres/"]
COPY ["H2h.RubberBand.Database/H2h.RubberBand.Database.csproj", "H2h.RubberBand.Database/"]
COPY ["H2h.RubberBand.Database.SqlServer/H2h.RubberBand.Database.SqlServer.csproj", "H2h.RubberBand.Database.SqlServer/"]
RUN dotnet restore "H2h.RubberBand.Server/H2h.RubberBand.Server.csproj"
COPY . .
WORKDIR "/src/H2h.RubberBand.Server"
RUN dotnet build "H2h.RubberBand.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "H2h.RubberBand.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "H2h.RubberBand.Server.dll"]