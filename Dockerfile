# =========================
# 1. BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо solution і csproj файли
COPY LangApp.sln .
COPY LangApp.API/LangApp.API.csproj LangApp.API/
COPY LangApp.BLL/LangApp.BLL.csproj LangApp.BLL/
COPY LangApp.Core/LangApp.Core.csproj LangApp.Core/
COPY LangApp.DAL/LangApp.DAL.csproj LangApp.DAL/

# Відновлюємо залежності
RUN dotnet restore

# Копіюємо весь код
COPY . .

# Збираємо і публікуємо Web API
RUN dotnet publish LangApp.API/LangApp.API.csproj -c Release -o /app/publish


# =========================
# 2. RUNTIME STAGE
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Копіюємо опублікований застосунок
COPY --from=build /app/publish .

# Відкриваємо порт (HTTP)
EXPOSE 8080

# Запуск застосунку
ENTRYPOINT ["dotnet", "LangApp.API.dll"]
