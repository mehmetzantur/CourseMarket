FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/CourseMarket.Shared/*.csproj Shared/CourseMarket.Shared/
COPY IdentityServer/CourseMarket.IdentityServer/*.csproj IdentityServer/CourseMarket.IdentityServer/
RUN dotnet restore IdentityServer/CourseMarket.IdentityServer/*.csproj
COPY . .
RUN dotnet publish IdentityServer/CourseMarket.IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "CourseMarket.IdentityServer.dll" ]