FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CoreCRUD/CoreCRUD.Api/CoreCRUD.Api.csproj", "CoreCRUD/CoreCRUD.Api/"]
COPY ["CoreCRUD/CoreCRUD.Repository/CoreCRUD.Repository.csproj", "CoreCRUD/CoreCRUD.Repository/"]
COPY ["CoreCRUD/CoreCRUD.Infrastructure/CoreCRUD.Infrastructure.csproj", "CoreCRUD/CoreCRUD.Infrastructure/"]
COPY ["CoreCRUD/CoreCRUD.Domain/CoreCRUD.Domain.csproj", "CoreCRUD/CoreCRUD.Domain/"]
COPY ["CoreCRUD/CoreCRUD.Application/CoreCRUD.Application.csproj", "CoreCRUD/CoreCRUD.Application/"]
RUN dotnet restore "CoreCRUD/CoreCRUD.Api/CoreCRUD.Api.csproj"
COPY . .
WORKDIR "/src/CoreCRUD/CoreCRUD.Api"
RUN dotnet build "CoreCRUD.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoreCRUD.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoreCRUD.Api.dll"]
