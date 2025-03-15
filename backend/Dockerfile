FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.sln ./
COPY StudyService/*.csproj ./StudyService/
RUN dotnet restore
COPY StudyService/. ./StudyService/
WORKDIR /app/StudyService
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/StudyService/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "StudyService.dll"]