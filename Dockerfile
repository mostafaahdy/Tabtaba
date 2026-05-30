# 1. المرحلة الأساسية لتشغيل التطبيق
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. مرحلة البناء (Build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ ملفات الـ csproj لكل المشاريع بالأسماء الحقيقية
COPY ["Tbtba.API/Tbtba.API.csproj", "Tbtba.API/"]
COPY ["Tabtaba.Services/Tabtaba.Services.csproj", "Tabtaba.Services/"]
COPY ["Tabtaba.ServicesAbstraction/Tabtaba.ServicesAbstraction.csproj", "Tabtaba.ServicesAbstraction/"]
COPY ["Tabtba.Shared/Tabtba.Shared.csproj", "Tabtba.Shared/"]
COPY ["Tbtba.Infrastructure/Tbtba.Infrastructure.csproj", "Tbtba.Infrastructure/"]

RUN dotnet restore "Tbtba.API/Tbtba.API.csproj"

# نسخ باقي الملفات وبناء المشروع
COPY . .
WORKDIR "/src/Tbtba.API"
RUN dotnet build "Tbtba.API.csproj" -c Release -o /app/build

# 3. مرحلة النشر (Publish)
FROM build AS publish
RUN dotnet publish "Tbtba.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. المرحلة النهائية لتشغيل السيرفر
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tbtba.API.dll"]