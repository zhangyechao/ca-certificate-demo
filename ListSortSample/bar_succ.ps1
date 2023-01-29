docker build -t sort:v2 -f .\Dockerfile .
docker run --rm -e LC_ALL=zh_CN sort:v2