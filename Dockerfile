# FROM dotnet/asp:3.1 AS base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS http://+:5000
ENV TZ=Asia/Shanghai

# FROM dotnet/sdk:3.1 AS publish
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS publish
WORKDIR /src
COPY ./ ./
RUN  dotnet publish Api -c Release -o /app
COPY ./Api/Data.db /app/Data.db


FROM base AS dev
WORKDIR /app
COPY --from=publish /app .
ENV FileService:ServeMapPath=''
ENTRYPOINT ["dotnet", "Api.dll"]
