# Email setup (local dev)

We keep EmailSettings:Password empty in appsettings.json to avoid leaking secrets.

## Using user-secrets
```bash
dotnet user-secrets init
dotnet user-secrets set "EmailSettings:From" "yourcinema@gmail.com"
dotnet user-secrets set "EmailSettings:Password" "YOUR_APP_PASSWORD"
dotnet user-secrets set "EmailSettings:Enabled" "true"
```

## Gmail app password
Create App Password in Google Account → Security → App Passwords.
Use that password for EmailSettings:Password.