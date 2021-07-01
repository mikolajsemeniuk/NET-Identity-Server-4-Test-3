# NET-Identity-Server-4
in `terminal`
```sh
dotnet new -l

dotnet new sln -n NET-Identity-Server-4

dotnet new webapi -o source/app/api
dotnet sln add source/app/services/api.service

dotnet new web -o source/app/IdentityServer
dotnet sln add source/app/IdentityServer

dotnet new xunit -o source/tests
dotnet sln add source/tests

dotnet new webapi -o source/app/services/client.service
dotnet sln add source/app/services/client.service

# http://localhost:5000/.well-known/openid-configuration
# https://localhost:5001/connect/token
```