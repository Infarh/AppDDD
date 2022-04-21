#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/AppDDD.WebAPI/AppDDD.WebAPI.csproj", "Services/AppDDD.WebAPI/"]
COPY ["Data/AppDDD.DAL/AppDDD.DAL.csproj", "Data/AppDDD.DAL/"]
COPY ["Services/AppDDD.Interfaces/AppDDD.Interfaces.csproj", "Services/AppDDD.Interfaces/"]
COPY ["Services/AppDDD.Interfaces.Base/AppDDD.Interfaces.Base.csproj", "Services/AppDDD.Interfaces.Base/"]
COPY ["Common/AppDDD.Domain/AppDDD.Domain.csproj", "Common/AppDDD.Domain/"]
COPY ["Common/AppDDD.Domain.Base/AppDDD.Domain.Base.csproj", "Common/AppDDD.Domain.Base/"]
COPY ["Data/AppDDD.DAL.MSSQLServer/AppDDD.DAL.MSSQLServer.csproj", "Data/AppDDD.DAL.MSSQLServer/"]
COPY ["Services/AppDDD.Services/AppDDD.Services.csproj", "Services/AppDDD.Services/"]
COPY ["Data/AppDDD.DAL.Sqlite/AppDDD.DAL.Sqlite.csproj", "Data/AppDDD.DAL.Sqlite/"]
RUN dotnet restore "Services/AppDDD.WebAPI/AppDDD.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/AppDDD.WebAPI"
RUN dotnet build "AppDDD.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppDDD.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppDDD.WebAPI.dll"]