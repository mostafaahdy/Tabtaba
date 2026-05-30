# 1. المرحلة الأساسية لتشغيل التطبيق باستخدام .NET 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. مرحلة البناء والنشر باستخدام SDK 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# نسخ كل الملفات دفعة واحدة
COPY . .

# عمل Restore وبناء للفولدر بالكامل
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