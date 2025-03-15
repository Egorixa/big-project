FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.sln ./
COPY frontend/frontend.csproj ./frontend/
RUN dotnet restore frontend/frontend.csproj
COPY frontend/ ./frontend/
WORKDIR /app/frontend
RUN dotnet publish -c Release -o /publish
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /publish/wwwroot .
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]