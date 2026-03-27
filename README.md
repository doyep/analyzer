# Analyzer

Web Application for analyzing performance for athletes and their friends based on Strava API.

At this moment, this repository only contains WebApi. The Ui will be added someday.

# Requirements

This API need the following informations :
- `AllowedHosts`
- `ConectionStrings.DefaultConnection`
- `Application.BaseUrl`
- `Strava.ClientId` 
- `Strava.ClientSecret`
- `Strava.RedirectUri`
- `Jwt.SecretKey`

You can use ENVIRONMENT VARIABLES or User Secrets (Doyep.Anlyzer.Api layer)

```bash
dotnet user-secrets init
dotnet user-secrets set "AllowedHosts" "..."
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "..."
dotnet user-secrets set "Application:BaseUrl" "..."
dotnet user-secrets set "Strava:ClientId" "..."
dotnet user-secrets set "Strava:ClientSecret" "..."
dotnet user-secrets set "Strava:RedirectUri" "..."
dotnet user-secrets set "Jwt:SecretKey" "..."
```

# Migration 

```bash
dotnet ef migrations add InitialCreate -p Doyep.Analyzer.Infrastructure -s Doyep.Analyzer.Api
```

```bash
dotnet ef database update -p Doyep.Analyzer.Infrastructure -s Doyep.Analyzer.Api
```
