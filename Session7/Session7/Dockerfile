#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Session7/Session7.csproj", "Session7/"]
RUN dotnet restore "Session7/Session7.csproj"
COPY . .
WORKDIR "/src/Session7"
RUN dotnet build "Session7.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Session7.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Session7.dll"]