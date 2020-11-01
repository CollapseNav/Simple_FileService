FROM dotnet/asp:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS http://+:5000
ENV TZ=Asia/Shanghai

FROM dotnet/sdk:3.1 AS publish
WORKDIR /src
COPY ./ ./
RUN  dotnet publish Api -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY ./Api/Data.db /app/Data.db
ENTRYPOINT ["dotnet", "Api.dll"]
