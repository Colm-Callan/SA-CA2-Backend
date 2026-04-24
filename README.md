## SA-CA2-Backend
# App Name: "Peamount 6 v 6 League"

## BY: Callum Glasgow X00201142 and Colm Callan X00195992

# Work distrubtion: 
Evenly split between frontend, backend, DB and deploy work

## Project Aim
Create a application for a mini football league where groups of friends can register a team to the league. The project can then generate fixtures for those teams at designated pitches and times to play. The league can store current standings to create a leaderboard and show current league winners. 

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

## Deployed via azure using GitHub Actions
# Deployed backed url: 
https://saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net/swagger/index.html
# Deployed DB url: 
saca2dbv2.postgres.database.azure.com
#
<img width="1905" height="978" alt="Screenshot 2026-04-23 191954" src="https://github.com/user-attachments/assets/9d03eb78-2f9d-4ce1-b68f-9a09586754ee" />

## Azure config for Backend and PostgreSQL db

<img width="1915" height="987" alt="Screenshot 2026-04-23 192110" src="https://github.com/user-attachments/assets/34101f96-a90d-4d2d-b1c3-445e5a8e6e81" />

<img width="1908" height="979" alt="Screenshot 2026-04-23 192215" src="https://github.com/user-attachments/assets/e0d01bd0-9181-4075-bc57-1be2f31ed512" />

## Testing report:
### Running Tests and Generating Coverage Reports

Navigate to Tests folder
```bash
cd SA-CA2-Backend/SACA2/SACA2.Tests
```

---
Run tests with coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

If missing report generator install
```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

---

Generate coverage report with
```bash
reportgenerator -reports:TestResults/**/*.xml -targetdir:coverage-report -reporttypes:Html -classfilters:"-SACA2.Migrations.*;-SACA2.Controllers.PlayerController;-SACA2.Models.Player"
```

---

Open report file
```
SACA2.Tests/coverage-report/index.html
```

<img width="1678" height="900" alt="image" src="https://github.com/user-attachments/assets/109a3e93-af98-4f85-81cf-54da32b6edb8" />

### Testing Summary
I used XUnit to write the tests for backend making use of the built in HttpClient to test endpoints.

Tests cover all core features such as 
- Teams
- Fixtures / Fixutre generation
- Authorisation

With these tests we have 93% line coverage and 81% branch coverage giving us a strongly tested backend API.

We done some user acceptable testing by giving the app to some family members with androids to test as well and they said it worked well

## Internationalisation:
We added Spanish as a second language available for accessibility for any spanish users to use, we had some complications with the emulator and had to find 
a way to change the language for testing but it works perfectly fine when tested on a physical android device when the language is changed in settings

## URI Scheme:
saca2deploy-a0gsbuavepa7asd6.polandcentral-01.azurewebsites.net

/api/Auth/signup

/api/Auth/login

/api/Team

/api/Team/{id}

/api/Player

/api/Player/{id}

/api/Player/team/{teamId}

/api/Fixture

/api/Fixture/generate/{date}

/api/Fixture/generate-multiple/{days}

## App Screenshots

View All in /Screenshots folder
--

<img width="1080" height="2400" alt="Home" src="https://github.com/user-attachments/assets/604513c0-427c-4c53-aa3b-fb41032db629" />

--

<img width="1080" height="2400" alt="Fixture" src="https://github.com/user-attachments/assets/8d81df58-e605-4078-9c67-0cdffbb046d6" />

-- 

<img width="1080" height="2400" alt="RegisterEs" src="https://github.com/user-attachments/assets/9ca5903b-418b-4d1c-be01-3f212af222ff" />

