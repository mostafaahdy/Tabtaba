# 1. المرحلة الأساسية لتشغيل التطبيق
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. مرحلة البناء والنشر (Build & Publish)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ كل الملفات دفعة واحدة
COPY . .

# عمل Restore وبناء للفولدر بالكامل بدون تحديد اسم ملف الـ csproj
RUN dotnet restore "Tbtba.API"
RUN dotnet build "Tbtba.API" -c Release -o /app/build

# 3. مرحلة النشر (Publish)
FROM build AS publish
RUN dotnet publish "Tbtba.API" -c Release -o /app/publish /p:UseAppHost=false

# 4. المرحلة النهائية لتشغيل السيرفر
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tbtba.API.dll"]