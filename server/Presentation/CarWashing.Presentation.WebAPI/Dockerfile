#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/CarWashing.Presentation.WebAPI/CarWashing.Presentation.WebAPI.csproj", "Presentation/CarWashing.Presentation.WebAPI/"]
COPY ["Application/CarWashing.Application/CarWashing.Application.csproj", "Application/CarWashing.Application/"]
COPY ["Application/CarWashing.Application.Abstractions/CarWashing.Application.Abstractions.csproj", "Application/CarWashing.Application.Abstractions/"]
COPY ["Application/CarWashing.Application.Dto/CarWashing.Application.Dto.csproj", "Application/CarWashing.Application.Dto/"]
COPY ["Domain/CarWashing.Domain.Core/CarWashing.Domain.Core.csproj", "Domain/CarWashing.Domain.Core/"]
COPY ["Domain/CarWashing.Domain.Common/CarWashing.Domain.Common.csproj", "Domain/CarWashing.Domain.Common/"]
COPY ["Application/CarWashing.Application.Common/CarWashing.Application.Common.csproj", "Application/CarWashing.Application.Common/"]
COPY ["Application/CarWashing.Application.Handlers/CarWashing.Application.Handlers.csproj", "Application/CarWashing.Application.Handlers/"]
COPY ["Infrastructure/CarWashing.Infrastructure.Mapping/CarWashing.Infrastructure.Mapping.csproj", "Infrastructure/CarWashing.Infrastructure.Mapping/"]
COPY ["Application/CarWashing.Application.Contracts/CarWashing.Application.Contracts.csproj", "Application/CarWashing.Application.Contracts/"]
COPY ["Application/CarWashing.DataAccess.Abstractions/CarWashing.DataAccess.Abstractions.csproj", "Application/CarWashing.DataAccess.Abstractions/"]
COPY ["Infrastructure/CarWashing.Infrastructure.DataAccess/CarWashing.Infrastructure.DataAccess.csproj", "Infrastructure/CarWashing.Infrastructure.DataAccess/"]
COPY ["Infrastructure/CarWashing.Infrastructure.Identity/CarWashing.Infrastructure.Identity.csproj", "Infrastructure/CarWashing.Infrastructure.Identity/"]
COPY ["Presentation/CarWashing.Presentation.Controllers/CarWashing.Presentation.Controllers.csproj", "Presentation/CarWashing.Presentation.Controllers/"]
COPY ["Presentation/CarWashing.Presentation.Contracts/CarWashing.Presentation.Contracts.csproj", "Presentation/CarWashing.Presentation.Contracts/"]
RUN dotnet restore "Presentation/CarWashing.Presentation.WebAPI/CarWashing.Presentation.WebAPI.csproj"
COPY . .
WORKDIR "/src/Presentation/CarWashing.Presentation.WebAPI"
RUN dotnet build "CarWashing.Presentation.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarWashing.Presentation.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["Assets/*", "/app/Assets/"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarWashing.Presentation.WebAPI.dll"]
