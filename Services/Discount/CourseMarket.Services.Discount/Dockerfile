FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/CourseMarket.Shared/*.csproj Shared/CourseMarket.Shared/
COPY Services/Discount/CourseMarket.Services.Discount/*.csproj Services/Discount/CourseMarket.Services.Discount/
RUN dotnet restore Services/Discount/CourseMarket.Services.Discount/*.csproj
COPY . .
RUN dotnet publish Services/Discount/CourseMarket.Services.Discount/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "CourseMarket.Services.Discount.dll" ]