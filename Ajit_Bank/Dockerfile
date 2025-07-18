# STEP 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy .csproj and restore as distinct layers
COPY Ajit_Bank/*.csproj ./Ajit_Bank/
RUN dotnet restore ./Ajit_Bank/Ajit_Bank.csproj

# Copy everything else and build
COPY . .
WORKDIR /app/Ajit_Bank
RUN dotnet publish -c Release -o out

# STEP 2: Create the runtime container
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published output
COPY --from=build /app/Ajit_Bank/out ./

# Use environment variable from Render for port
ENV ASPNETCORE_URLS=http://+:$PORT

# Expose the port (not mandatory, but good practice)
EXPOSE 10000

# Start your app
ENTRYPOINT ["dotnet", "Ajit_Bank.dll"]
