FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "GigaHurtz Frontend/GigaHurtz Frontend.csproj"
COPY . .
WORKDIR "/src/GigaHurtz Frontend"
RUN dotnet build "GigaHurtz Frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GigaHurtz Frontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "GigaHurtzApi.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet "GigaHurtz Frontend.dll"
