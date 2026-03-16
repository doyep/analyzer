# Analyzer

Web Application for analyzing performance for athletes and their friends based on Strava API.

At this moment, this repository only contains WebApi. The Ui will be added someday.

# Requirements

This API need the following informations :
- `AllowedHosts`
- `ClientId` 
- `ClientSecret`
- `RedirectUri`

You can use ENVIRONMENT VARIABLES or User Secrets (Doyep.Anlyzer.Api layer)

```bash
dotnet user-secrets init
dotnet user-secrets set "AllowedHosts" "..."
dotnet user-secrets set "StravaApplication:ClientId" "..."
dotnet user-secrets set "StravaApplication:ClientSecret" "..."
dotnet user-secrets set "StravaApplication:RedirectUri" "..."
```
