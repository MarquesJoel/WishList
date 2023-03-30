FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WishList.API/WishList.API.csproj", "WishList.API/"]
RUN dotnet restore "WishList.API/WishList.API.csproj"
COPY ./src .
WORKDIR "/src/WishList.API"
RUN dotnet build "WishList.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WishList.API.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WishList.API.dll"]