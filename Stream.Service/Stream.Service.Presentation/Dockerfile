FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Stream.Service/Stream.Service.Presentation/Stream.Service.Presentation.csproj", "Stream.Service/Stream.Service.Presentation/"]
COPY ["Stream.Service/Stream.Service.BusinessLogic/Stream.Service.BusinessLogic.csproj", "Stream.Service/Stream.Service.BusinessLogic/"]
COPY ["Stream.Service/Stream.Service.DataAccess/Stream.Service.DataAccess.csproj", "Stream.Service/Stream.Service.DataAccess/"]
COPY ["Stream.Service/Stream.Service.Shared/Stream.Service.Shared.csproj", "Stream.Service/Stream.Service.Shared/"]
RUN dotnet restore "Stream.Service/Stream.Service.Presentation/Stream.Service.Presentation.csproj"
COPY . .
WORKDIR "/src/Stream.Service/Stream.Service.Presentation"
RUN dotnet build "Stream.Service.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Stream.Service.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stream.Service.Presentation.dll"]
