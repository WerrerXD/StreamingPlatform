FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Notification.Service.Presentation/Notification.Service.Presentation.csproj", "Notification.Service/Notification.Service.Presentation/"]
COPY ["Notification.Service.Shared/Notification.Service.Shared.csproj", "Notification.Service/Notification.Service.Shared/"]
COPY ["Notification.Service.Application/Notification.Service.Application.csproj", "Notification.Service/Notification.Service.Application/"]
COPY ["Notification.Service.Infrastructure/Notification.Service.Infrastructure.csproj", "Notification.Service/Notification.Service.Infrastructure/"]
COPY ["Notification.Service.Domain/Notification.Service.Domain.csproj", "Notification.Service/Notification.Service.Domain/"]
RUN dotnet restore "Notification.Service/Notification.Service.Presentation/Notification.Service.Presentation.csproj"
COPY . .
WORKDIR "/src/Notification.Service/Notification.Service.Presentation"
RUN dotnet build "Notification.Service.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Notification.Service.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.Service.Presentation.dll"]
