FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/CourseMarket.Shared/*.csproj Shared/CourseMarket.Shared/
COPY Frontends/CourseMarket.Web/*.csproj Frontends/CourseMarket.Web/
RUN dotnet restore Frontends/CourseMarket.Web/*.csproj
COPY . .
RUN dotnet publish Frontends/CourseMarket.Web/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "CourseMarket.Web.dll" ]