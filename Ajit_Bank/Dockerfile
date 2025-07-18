# STEP 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the code and publish it
COPY . ./
RUN dotnet publish -c Release -o out

# STEP 2: Create the runtime container
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published output
COPY --from=build /app/out .

# Use environment variable from Render for port
ENV ASPNETCORE_URLS=http://+:$PORT

# Expose the port (not mandatory, but good practice)
EXPOSE 10000

# Start your app
ENTRYPOINT ["dotnet", "Ajit_Bank.dll"]
