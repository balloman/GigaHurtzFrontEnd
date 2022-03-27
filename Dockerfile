FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "GigaHurtzApi/GigaHurtzApi.csproj"
COPY . .
WORKDIR "/src/GigaHurtzApi"
RUN dotnet build "GigaHurtzApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GigaHurtzApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "GigaHurtzApi.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet GigaHurtzApi.dll
