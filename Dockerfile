# 1. المرحلة الأساسية لتشغيل التطبيق
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. مرحلة البناء والنشر (Build & Publish)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ كل ملفات الـ Solution والمشاريع دفعة واحدة لتجنب أخطاء المسارات
COPY . .

# عمل Restore وبناء من ملف الـ API الأساسي علطول
RUN dotnet restore "Tbtba.API/Tbtba.API.csproj"
RUN dotnet build "Tbtba.API/Tbtba.API.csproj" -c Release -o /app/build

# 3. مرحلة النشر (Publish)
FROM build AS publish
RUN dotnet publish "Tbtba.API/Tbtba.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. المرحلة النهائية لتشغيل السيرفر
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tbtba.API.dll"]