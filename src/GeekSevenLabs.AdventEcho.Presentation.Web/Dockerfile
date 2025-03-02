FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GeekSevenLabs.AdventEcho.Presentation.Web/GeekSevenLabs.AdventEcho.Presentation.Web.csproj", "src/GeekSevenLabs.AdventEcho.Presentation.Web/GeekSevenLabs.AdventEcho.Presentation.Web/"]
COPY ["src/GeekSevenLabs.AdventEcho.Presentation.Web.Client/GeekSevenLabs.AdventEcho.Presentation.Web.Client.csproj", "src/GeekSevenLabs.AdventEcho.Presentation.Web/GeekSevenLabs.AdventEcho.Presentation.Web.Client/"]
RUN dotnet restore "src/GeekSevenLabs.AdventEcho.Presentation.Web/GeekSevenLabs.AdventEcho.Presentation.Web.csproj"
COPY . .
WORKDIR "/src/src/GeekSevenLabs.AdventEcho.Presentation.Web"
RUN dotnet build "GeekSevenLabs.AdventEcho.Presentation.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GeekSevenLabs.AdventEcho.Presentation.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekSevenLabs.AdventEcho.Presentation.Web.dll"]
