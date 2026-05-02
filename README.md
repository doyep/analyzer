# Analyzer

Web Application for analyzing performance for athletes and their friends based on Strava API.

At this moment, this repository only contains WebApi. The Ui will be added someday.

# Requirements

This API need the following informations :
- `AllowedHosts`
- `Auth.Jwt.Secret`
- `Auth.RefreshToken.ExpirationInDays`
- `Frontend.BaseUrl`
- `ConnectionStrings.DoyepAnalyzerDb`
- `Strava.ClientId` 
- `Strava.ClientSecret`
- `Strava.RedirectUri`

You can use ENVIRONMENT VARIABLES or User Secrets (Doyep.Analyzer.Api layer)

```bash
dotnet user-secrets init
dotnet user-secrets set "AllowedHosts" "..."
dotnet user-secrets set "Auth:Jwt:Secret" "..."
dotnet user-secrets set "Auth:RefreshToken:ExpirationInDays" "..."
dotnet user-secrets set "Frontend:BaseUrl" "..."
dotnet user-secrets set "ConnectionStrings:DoyepAnalyzerDb" "..."
dotnet user-secrets set "Strava:ClientId" "..."
dotnet user-secrets set "Strava:ClientSecret" "..."
dotnet user-secrets set "Strava:RedirectUri" "..."
```

# Migration 

```bash
dotnet ef migrations add InitialCreate -p src/infrastructure -s apps/api
```

```bash
dotnet ef database update -p src/infrastructure -s apps/api
```

# Aspire 

Clean cause /obj /bin already existe after migration to Aspire Monorepo

Try to clean first 

```bash
dotnet clean
rm -rf **/bin **/obj
```

The reinstall

```bash
dotnet restore
dotnet build
```
