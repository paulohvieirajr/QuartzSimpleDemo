FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["QuartzSimpleDemo/QuartzSimpleDemo.csproj", "QuartzSimpleDemo/"]
RUN dotnet restore "QuartzSimpleDemo/QuartzSimpleDemo.csproj"
COPY . .
WORKDIR "/src/QuartzSimpleDemo"
RUN dotnet build "QuartzSimpleDemo.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "QuartzSimpleDemo.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QuartzSimpleDemo.dll"]