## SA-CA2-Backend
# App Name: "Peamount 6 v 6 League"

## BY:
# Callum Glasgow X00201142
# Colm Callan X00195992

# Work distrubtion: 
# evenly split between frontend, backend, DB and deploy work



## how to set up
clone down the repo (https://github.com/Colm-Callan/SA-CA2-Backend.git)

Cd SA-CA2-Backend\SACA2\SAC2

run these install commands

dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0

dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.0

dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0

dotnet tool install --global dotnet-ef


To set up the database

dotnet ef migrations add InitialCreate

dotnet ef database update

dotnet build
dotnet run

To set up the tests
Cd SA-CA2-Backend\SACA2\SAC2-Test




x
x
x
x
x
x

## Deployed via azure using GitHub Actions
# Deployed backed url: https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/swagger/index.html
# Deployed DB url: saca2dbv2.postgres.database.azure.com
<img width="1905" height="978" alt="Screenshot 2026-04-23 191954" src="https://github.com/user-attachments/assets/9d03eb78-2f9d-4ce1-b68f-9a09586754ee" />

## Azure config for Backend and PostgreSQL db

<img width="1915" height="987" alt="Screenshot 2026-04-23 192110" src="https://github.com/user-attachments/assets/34101f96-a90d-4d2d-b1c3-445e5a8e6e81" />

<img width="1908" height="979" alt="Screenshot 2026-04-23 192215" src="https://github.com/user-attachments/assets/e0d01bd0-9181-4075-bc57-1be2f31ed512" />

## Testing report:
# here 

## URI Scheme:
# Auth Endpoints
POST   https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Auth/signup
POST   https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Auth/login

# Team Endpoints
GET    https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Team
POST   https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Team
GET    https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Team/{id}
PUT    https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Team/{id}
DELETE https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Team/{id}

# Player Endpoints
GET    https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Player
POST   https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Player
GET    https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Player/{id}
PUT    https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Player/{id}
DELETE https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Player/{id}
GET    https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Player/team/{teamId}

# Fixture Endpoints
GET    https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Fixture
POST   https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Fixture
POST   https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Fixture/generate/{date}
POST   https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/api/Fixture/generate-multiple/{days}
