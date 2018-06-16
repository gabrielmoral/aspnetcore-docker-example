FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY ./Api/*.csproj ./Api/
COPY ./Api.Domain/*.csproj ./Api.Domain/
COPY ./Api.MemoryStore/*.csproj ./Api.MemoryStore/
COPY ./Api.PermanentStore/*.csproj ./Api.PermanentStore/
COPY ./Api.Test/*.csproj ./Api.Test/
COPY ./Api.EndToEndTest/*.csproj ./Api.EndToEndTest/
COPY . ./

RUN dotnet publish -c Release -o out

# # Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/Api/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "Api.dll"]