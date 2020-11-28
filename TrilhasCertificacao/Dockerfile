#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TrilhasCertificacao/TrilhasCertificacao.csproj", "TrilhasCertificacao/"]
RUN dotnet restore "TrilhasCertificacao/TrilhasCertificacao.csproj"
COPY . .
WORKDIR "/src/TrilhasCertificacao"
RUN dotnet build "TrilhasCertificacao.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrilhasCertificacao.csproj" -c Release -o /app/publish

#Angular build
FROM node:12.16.1-alpine3.9 as nodebuilder

# set working directory
#RUN mkdir /usr/src/app
#WORKDIR /usr/src/app
WORKDIR /app

# add `/usr/src/app/node_modules/.bin` to $PATH
#ENV PATH /usr/src/app/node_modules/.bin:$PATH
ENV PATH /app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY TrilhasCertificacao/ClientApp/package.json /app/package.json
RUN npm install
RUN npm install -g @angular/cli --unsafe

# add app

COPY TrilhasCertificacao/ClientApp/. /app

RUN npm run build --prod

#End Angular build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/ClientApp/dist
COPY --from=nodebuilder /app/dist/. /app/ClientApp/dist/
ENTRYPOINT ["dotnet", "TrilhasCertificacao.dll"]