FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/CourseMarket.Shared/*.csproj Shared/CourseMarket.Shared/
COPY Services/Catalog/CourseMarket.Services.Catalog/*.csproj Services/Catalog/CourseMarket.Services.Catalog/
RUN dotnet restore Services/Catalog/CourseMarket.Services.Catalog/*.csproj
COPY . .
RUN dotnet publish Services/Catalog/CourseMarket.Services.Catalog/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "CourseMarket.Services.Catalog.dll" ]