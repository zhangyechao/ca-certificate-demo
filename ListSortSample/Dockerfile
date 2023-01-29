FROM mcr.microsoft.com/dotnet/runtime:6.0-bullseye-slim AS base
# ENV LC_ALL zh_CN
# ENV LANG zh_CN
# ENV LANGUAGE zh_CN

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY . .
RUN dotnet restore /src/ListSortSample.sln

WORKDIR /src/ListSortSample
RUN dotnet build "ListSortSample.csproj" -c Release -o /app/build

FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "ListSortSample.dll"]
