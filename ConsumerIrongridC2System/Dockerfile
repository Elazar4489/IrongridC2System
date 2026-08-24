FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /src
COPY  --from=build /publish .
ENTRYPOINT ["dotnet", "ConsumerIrongridC2System.dll"]

