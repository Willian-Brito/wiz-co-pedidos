FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/WizCo.Presentation.WebAPI/WizCo.Presentation.WebAPI.csproj", "src/Presentation/WizCo.Presentation.WebAPI/"]
COPY ["src/Infrastructure/WizCo.Infra.IoC/WizCo.Infra.IoC.csproj", "src/Infrastructure/WizCo.Infra.IoC/"]
COPY ["src/Core/WizCo.Core.Domain/WizCo.Core.Domain.csproj", "src/Core/WizCo.Core.Domain/"]
COPY ["src/Core/WizCo.Core.Application/WizCo.Core.Application.csproj", "src/Core/WizCo.Core.Application/"]
COPY ["src/Infrastructure/WizCo.Infra.Data/WizCo.Infra.Data.csproj", "src/Infrastructure/WizCo.Infra.Data/"]
RUN dotnet restore "src/Presentation/WizCo.Presentation.WebAPI/WizCo.Presentation.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Presentation/WizCo.Presentation.WebAPI"
RUN dotnet build "WizCo.Presentation.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WizCo.Presentation.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/data && chmod -R 777 /app/data
ENTRYPOINT ["dotnet", "WizCo.Presentation.WebAPI.dll"]
