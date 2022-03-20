Migrations:

inside Persons.API directory:

dotnet ef migrations add "Initial" --context PersonsDbContext -o Infrastructure/Migrations/


dotnet ef migrations add "initial" -p .\src\Services\Persons\Persons.API\Persons.API\ -o .\src\Services\Persons\Persons.API\Persons.API\Infrastructure\IntegrationEventMigrations -s .\src\Services\Persons\Persons.API\Persons.API\ -c IntegrationEventLogContext
