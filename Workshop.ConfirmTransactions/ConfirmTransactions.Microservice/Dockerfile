#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Workshop.ConfirmTransactions/ConfirmTransactions.Microservice/ConfirmTransactions.Microservice.csproj", "Workshop.ConfirmTransactions/ConfirmTransactions.Microservice/"]
RUN dotnet restore "Workshop.ConfirmTransactions/ConfirmTransactions.Microservice/ConfirmTransactions.Microservice.csproj"
COPY . .
WORKDIR "/src/Workshop.ConfirmTransactions/ConfirmTransactions.Microservice"
RUN dotnet build "ConfirmTransactions.Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConfirmTransactions.Microservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConfirmTransactions.Microservice.dll"]
