# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /AnagramWS

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore AnagramWS
# Build and publish a release
RUN dotnet publish AnagramWS  -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /AnagramWS
COPY --from=build-env /AnagramWS/out .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
CMD ["dotnet", "AnagramWS.dll"]