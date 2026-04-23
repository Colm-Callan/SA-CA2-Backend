# SA-CA2-Backend
# App Name:

## BY
## Callum Glasgow X00201142
## Colm Callan X00195992

# Work distrubtion : evenly split between frontend, backend, DB and deploy work


## how to set up
go to SA-CA2-Backend\SACA2\SAC2

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
