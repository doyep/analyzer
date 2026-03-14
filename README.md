# Analyzer

Web Application for analyzing performance for athletes and their friends based on Strava API.

At this moment, this repository only contains WebApi. The Ui will be add someday.

# Requirements

This API need the `ClientId` and `ClientSecret`.
You can use ENVIRONMENT VARIABLES or User Secrets
```bash
dotnet user-secrets init
dotnet user-secrets set "Strava:ClientId" "..."
dotnet user-secrets set "Strava:ClientSecret" "..."
```
