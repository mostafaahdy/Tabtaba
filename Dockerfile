# 1. المرحلة الأساسية لتشغيل التطبيق
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. مرحلة البناء والنشر (Build & Publish)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ كل الملفات دفعة واحدة عشان الـ Solution يقرا الفولدرات صح
COPY . .

# عمل Restore وبناء باستخدام الحروف الصحيحة للمشروع
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