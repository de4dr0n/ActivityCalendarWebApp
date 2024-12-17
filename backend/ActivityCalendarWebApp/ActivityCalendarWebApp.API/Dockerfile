FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ActivityCalendarWebApp.API/ActivityCalendarWebApp.API.csproj", "ActivityCalendarWebApp.API/"]
COPY ["ActivityCalendarWebApp.Application/ActivityCalendarWebApp.Application.csproj", "ActivityCalendarWebApp.Application/"]
COPY ["ActivityCalendarWebApp.Domain/ActivityCalendarWebApp.Domain.csproj", "ActivityCalendarWebApp.Domain/"]
COPY ["ActivityCalendarWebApp.Persistence/ActivityCalendarWebApp.Persistence.csproj", "ActivityCalendarWebApp.Persistence/"]
RUN dotnet restore "ActivityCalendarWebApp.API/ActivityCalendarWebApp.API.csproj"
COPY . .
WORKDIR "/src/ActivityCalendarWebApp.API"
RUN dotnet build "ActivityCalendarWebApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ActivityCalendarWebApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ActivityCalendarWebApp.API.dll"]
