FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# build runtime image
FROM microsoft/dotnet:2.1-runtime 
WORKDIR /opt/cavitos/registry-client
COPY --from=build-env /app/RegistryClient/out ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "RegistryClient.dll"]