FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["User.Service.Presentation/User.Service.Presentation.csproj", "User.Service/User.Service.Presentation/"]
COPY ["User.Service.Application/User.Service.Application.csproj", "User.Service/User.Service.Application/"]
COPY ["User.Service.Domain/User.Service.Domain.csproj", "User.Service/User.Service.Domain/"]
COPY ["User.Service.Infrastructure/User.Service.Infrastructure.csproj", "User.Service/User.Service.Infrastructure/"]
COPY ["User.Service.Shared/User.Service.Shared.csproj", "User.Service/User.Service.Shared/"]
RUN dotnet restore "User.Service/User.Service.Presentation/User.Service.Presentation.csproj"
COPY . .
WORKDIR "/src/User.Service/User.Service.Presentation"
RUN dotnet build "User.Service.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "User.Service.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Service.Presentation.dll"]
