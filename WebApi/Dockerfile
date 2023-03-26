FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY ./ /code

RUN cd /code && \ 
    dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
COPY --from=build /app /app
WORKDIR /app
ENTRYPOINT [ "dotnet", "WebApi.dll", "--urls", "http://0.0.0.0:5000" ]