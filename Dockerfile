# 1. المرحلة الأساسية لتشغيل التطبيق
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. مرحلة البناء (Build)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# نسخ كل ملفات المشروع بالكامل
COPY . .

# الانتقال لفولدر الـ API وبنائه
WORKDIR "/src/Tbtba.API"
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

# 3. مرحلة النشر (Publish)
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# 4. المرحلة النهائية لتشغيل السيرفر بالـ DLL الصح
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tabtaba.Web.dll"]