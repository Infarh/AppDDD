#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UI/AppDDD.MVC/AppDDD.MVC.csproj", "UI/AppDDD.MVC/"]
COPY ["Services/AppDDD.WebAPI.Clients/AppDDD.WebAPI.Clients.csproj", "Services/AppDDD.WebAPI.Clients/"]
COPY ["Services/AppDDD.Interfaces/AppDDD.Interfaces.csproj", "Services/AppDDD.Interfaces/"]
COPY ["Services/AppDDD.Interfaces.Base/AppDDD.Interfaces.Base.csproj", "Services/AppDDD.Interfaces.Base/"]
COPY ["Common/AppDDD.Domain/AppDDD.Domain.csproj", "Common/AppDDD.Domain/"]
COPY ["Common/AppDDD.Domain.Base/AppDDD.Domain.Base.csproj", "Common/AppDDD.Domain.Base/"]
RUN dotnet restore "UI/AppDDD.MVC/AppDDD.MVC.csproj"
COPY . .
WORKDIR "/src/UI/AppDDD.MVC"
RUN dotnet build "AppDDD.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppDDD.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppDDD.MVC.dll"]