#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#基本镜像
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#工作目录
WORKDIR /app
#端口号
EXPOSE 8030

COPY . .

ENTRYPOINT ["dotnet", "AbpDemo.Web.dll"]