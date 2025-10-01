FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/CourseMarket.Shared/*.csproj Shared/CourseMarket.Shared/
COPY Services/Order/CourseMarket.Services.Order.Application/*.csproj Services/Order/CourseMarket.Services.Order.Application/
COPY Services/Order/CourseMarket.Services.Order.Domain/*.csproj Services/Order/CourseMarket.Services.Order.Domain/
COPY Services/Order/CourseMarket.Services.Order.Domain.Core/*.csproj Services/Order/CourseMarket.Services.Order.Domain.Core/
COPY Services/Order/CourseMarket.Services.Order.Infrastructure/*.csproj Services/Order/CourseMarket.Services.Order.Infrastructure/
COPY Services/Order/CourseMarket.Services.Order.API/*.csproj Services/Order/CourseMarket.Services.Order.API/
RUN dotnet restore Services/Order/CourseMarket.Services.Order.API/*.csproj
COPY . .
RUN dotnet publish Services/Order/CourseMarket.Services.Order.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "CourseMarket.Services.Order.API.dll" ]