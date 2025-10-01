FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/CourseMarket.Shared/*.csproj Shared/CourseMarket.Shared/
COPY Services/Basket/CourseMarket.Services.Basket/*.csproj Services/Basket/CourseMarket.Services.Basket/
RUN dotnet restore Services/Basket/CourseMarket.Services.Basket/*.csproj
COPY . .
RUN dotnet publish Services/Basket/CourseMarket.Services.Basket/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "CourseMarket.Services.Basket.dll" ]