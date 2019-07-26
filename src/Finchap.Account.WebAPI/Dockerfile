FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Finchap.Account.WebAPI/Finchap.Account.WebAPI.csproj", "Finchap.Account.WebAPI/"]
RUN dotnet restore "Finchap.Account.WebAPI/Finchap.Account.WebAPI.csproj"
COPY . .
WORKDIR "/src/Finchap.Account.WebAPI"
RUN dotnet build "Finchap.Account.WebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Finchap.Account.WebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Finchap.Account.WebAPI.dll"]