FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["TaxCalculator.Web/TaxCalculator.Web.csproj","TaxCalculator.Web/TaxCalculator.Web.csproj"]
COPY ["TaxCalculator.Business/TaxCalculator.Business.csproj","TaxCalculator.Business/TaxCalculator.Business.csproj"]
COPY ["TaxCalculator.Data/TaxCalculator.Data.csproj","TaxCalculator.Data/TaxCalculator.Data.csproj"]
COPY ["TaxCalculator.Domain/TaxCalculator.Domain.csproj","TaxCalculator.Domain/TaxCalculator.Domain.csproj"]
COPY ["TaxCalculator.Validation/TaxCalculator.Validation.csproj","TaxCalculator.Validation/TaxCalculator.Validation.csproj"]
RUN dotnet restore "TaxCalculator.Web/TaxCalculator.Web.csproj"
COPY . .
WORKDIR "/src/TaxCalculator.Web"
# Build and publish a release
RUN dotnet build "TaxCalculator.Web.csproj" -c Release -o /app

FROM build as publish
RUN dotnet publish "TaxCalculator.Web.csproj" -c Release -o /app

# Build runtime image
FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TaxCalculator.Web.dll"]