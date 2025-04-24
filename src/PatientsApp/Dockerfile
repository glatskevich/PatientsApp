#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PatientsApp/PatientsApp.csproj", "PatientsApp/"]
COPY ["PatientsApp.BusinessLogic/PatientsApp.BusinessLogic.csproj", "PatientsApp.BusinessLogic/"]
COPY ["PatientsApp.Common/PatientsApp.Common.csproj", "PatientsApp.Common/"]
COPY ["PatientsApp.DataAccess/PatientsApp.DataAccess.csproj", "PatientsApp.DataAccess/"]
RUN dotnet restore "./PatientsApp/PatientsApp.csproj"
COPY . .
WORKDIR "/src/PatientsApp"
RUN dotnet build "./PatientsApp.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PatientsApp.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientsApp.dll"]