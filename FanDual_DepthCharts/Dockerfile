FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FanDual_DepthCharts/FanDual_DepthCharts.csproj", "FanDual_DepthCharts/"]
RUN dotnet restore "FanDual_DepthCharts/FanDual_DepthCharts.csproj"
COPY . .
WORKDIR "/src/FanDual_DepthCharts"
RUN dotnet build "FanDual_DepthCharts.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FanDual_DepthCharts.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FanDual_DepthCharts.dll"]
