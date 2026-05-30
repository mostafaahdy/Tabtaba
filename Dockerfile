# 1. مرحلة التشغيل الأساسية
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. مرحلة البناء (Build)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# نسخ ملف السولوشين وكل ملفات المشاريع أولاً كاش للـ Restore
COPY *.sln ./
COPY Tbtba.API/*.csproj ./Tbtba.API/
COPY Tabtaba.Services/*.csproj ./Tabtaba.Services/
COPY Tabtaba.ServicesAbstraction/*.csproj ./Tabtaba.ServicesAbstraction/
COPY Tabtba.Shared/*.csproj ./Tabtba.Shared/
COPY Tbtba.Infrastructure/*.csproj ./Tbtba.Infrastructure/

# عمل Restore للاعتماديات
RUN dotnet restore

# نسخ باقي الملفات بالكامل وبناء المشروع
COPY . .
WORKDIR "/src/Tbtba.API"
RUN dotnet build -c Release -o /app/build

# 3. مرحلة النشر (Publish)
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# 4. تشغيل التطبيق النهائي
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tbtba.API.dll"]