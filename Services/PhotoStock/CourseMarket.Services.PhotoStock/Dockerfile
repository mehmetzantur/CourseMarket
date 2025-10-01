FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/CourseMarket.Shared/*.csproj Shared/CourseMarket.Shared/
COPY Services/PhotoStock/CourseMarket.Services.PhotoStock/*.csproj Services/PhotoStock/CourseMarket.Services.PhotoStock/
RUN dotnet restore Services/PhotoStock/CourseMarket.Services.PhotoStock/*.csproj
COPY . .
RUN dotnet publish Services/PhotoStock/CourseMarket.Services.PhotoStock/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "CourseMarket.Services.PhotoStock.dll" ]