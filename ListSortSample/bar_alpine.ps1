docker build -t sort:v3 -f .\Dockerfile.alpine .
docker run --rm -e LC_ALL=zh_CN -e DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false sort:v3