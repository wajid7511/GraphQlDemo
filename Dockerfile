# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and project files
COPY ["GraphQlDemo.sln", "."]
COPY ["GraphQlDemo/GraphQlDemo.csproj", "GraphQlDemo/"]
COPY ["Core/GraphQl.Core/GraphQl.Core.csproj", "Core/GraphQl.Core/"]
COPY ["Abstractions/GraphQl.Abstractions/GraphQl.Abstractions.csproj", "Abstractions/GraphQl.Abstractions/"]
COPY ["GraphQlDemo.API.Models/GraphQlDemo.API.Models.csproj", "GraphQlDemo.API.Models/"]
COPY ["Shared/GraphQlDemo.Shared/GraphQlDemo.Shared.csproj", "Shared/GraphQlDemo.Shared/"]
COPY ["Common/GraphQl.Common/GraphQl.Common.csproj", "Common/GraphQl.Common/"]
COPY ["Messaging/Producer.RabbitMq/Producer.RabbitMq.csproj", "Messaging/Producer.RabbitMq/"]
COPY ["Data/Database/GraphQl.Database.csproj", "Data/Database/"]
COPY ["Data/GraphQl.Mongo.Database/GraphQl.Mongo.Database.csproj", "Data/GraphQl.Mongo.Database/"]
COPY ["Tests/GraphQl.Core.Test/GraphQl.Core.Test.csproj", "Tests/GraphQl.Core.Test/"]
COPY ["Tests/GraphQl.Database.Test/GraphQl.Database.Test.csproj", "Tests/GraphQl.Database.Test/"]
COPY ["Tests/GraphQlDemo.API.Models.Test/GraphQlDemo.API.Models.Test.csproj", "Tests/GraphQlDemo.API.Models.Test/"]
COPY ["Tests/GraphQlDemo.Test/GraphQlDemo.Test.csproj", "Tests/GraphQlDemo.Test/"]
COPY ["Tests/GraphQl.Common.Test/GraphQl.Common.Test.csproj", "Tests/GraphQl.Common.Test/"]

# Restore dependencies
RUN dotnet restore

# Copy the remaining source files
COPY . .

# Build the application
WORKDIR "/src/GraphQlDemo"
RUN dotnet build -c Release -o /app/build

# Stage 2: Run tests
FROM build AS testrunner
WORKDIR /src

# Execute tests
RUN dotnet test --no-build "Tests/GraphQl.Core.Test/GraphQl.Core.Test.csproj" --verbosity normal
RUN dotnet test --no-build "Tests/GraphQl.Database.Test/GraphQl.Database.Test.csproj" --verbosity normal
RUN dotnet test --no-build "Tests/GraphQlDemo.API.Models.Test/GraphQlDemo.API.Models.Test.csproj" --verbosity normal
RUN dotnet test --no-build "Tests/GraphQlDemo.Test/GraphQlDemo.Test.csproj" --verbosity normal

# Stage 3: Publish the application
FROM build AS publish
WORKDIR "/src/GraphQlDemo"
RUN dotnet publish -c Release -o /app/publish

# Stage 4: Final stage - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published output from the publish stage
COPY --from=publish /app/publish . 

EXPOSE 8080

# Set the entry point for the application
ENTRYPOINT ["dotnet", "GraphQlDemo.dll"]
