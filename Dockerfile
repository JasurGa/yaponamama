FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5056
#
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
#
COPY . .
RUN dotnet restore "./Atlas.Presentation/Atlas.Identity/Atlas.Identity.csproj"
#
WORKDIR "/src/."
RUN dotnet build "Atlas.Presentation/Atlas.Identity/Atlas.Identity.csproj" -c Release -o /app/build
#
FROM build AS publish
RUN dotnet publish "Atlas.Presentation/Atlas.Identity/Atlas.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
ENTRYPOINT ["dotnet", "Atlas.Identity.dll"]
