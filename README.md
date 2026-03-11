# analyzer

Web Application for analyzing performance for athletes and their friends based on Strava API.

# Requirements

This API need the `ClientId` and `ClientSecret`.
You can use ENVIRONMENT VARIABLES or User Secrets
```bash
dotnet user-secrets init
dotnet user-secrets set "Strava:ClientId" "..."
dotnet user-secrets set "Strava:ClientSecret" "..."
```
