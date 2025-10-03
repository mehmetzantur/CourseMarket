FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
EXPOSE 80

COPY Gateways/CourseMarket.Gateways.Web/*.csproj Gateways/CourseMarket.Gateways.Web/
RUN dotnet restore Gateways/CourseMarket.Gateways.Web/*.csproj
COPY . .
RUN dotnet publish Gateways/CourseMarket.Gateways.Web/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "CourseMarket.Gateways.Web.dll" ]