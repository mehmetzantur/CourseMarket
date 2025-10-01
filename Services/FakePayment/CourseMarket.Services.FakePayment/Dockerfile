FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/CourseMarket.Shared/*.csproj Shared/CourseMarket.Shared/
COPY Services/FakePayment/CourseMarket.Services.FakePayment/*.csproj Services/FakePayment/CourseMarket.Services.FakePayment/
RUN dotnet restore Services/FakePayment/CourseMarket.Services.FakePayment/*.csproj
COPY . .
RUN dotnet publish Services/FakePayment/CourseMarket.Services.FakePayment/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "CourseMarket.Services.FakePayment.dll" ]