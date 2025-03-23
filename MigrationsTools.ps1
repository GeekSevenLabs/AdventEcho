# Ask which context to use
Write-Host "Choose the context:"
Write-Host "1 - AdventEchoIdentityDbContext"
Write-Host "2 - AdventEchoDbContext"
$contextOption = Read-Host "Enter the number of the desired option"

# Set variables based on choice
if ($contextOption -eq "1") {
    $context = "AdventEchoIdentityDbContext"
    $project = "src/Services/Identity/Infrastructure/AdventEcho.Identity.Infrastructure.csproj"
    $startupProject = "src/Presentation/Identity/AdventEcho.Presentation.Identity.csproj"
} elseif ($contextOption -eq "2") {
    $context = "AdventEchoDbContext"
    $project = "src/GeekSevenLabs.AdventEcho.Infrastructure.DataAccess/GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.csproj"
    $startupProject = "src/GeekSevenLabs.AdventEcho.Presentation.Web/GeekSevenLabs.AdventEcho.Presentation.Web.csproj"
} else {
    Write-Host "Invalid option. Exiting..."
    exit
}

Write-Host "Actions:"
Write-Host "1 - Create migration and update database"
Write-Host "2 - Create migration only"
Write-Host "3 - Update database only"
$contextOption = Read-Host "Enter the number of the desired option"


if ($contextOption -match "^[12]$")
{
    # Ask for the migration name
    $migrationName = Read-Host "Enter the migration name"

    # Create the migration
    dotnet ef migrations add $migrationName `
    --project $project `
    --startup-project $startupProject `
    --context $context
}

if ($contextOption -match "^[13]$") {
    dotnet ef database update `
        --project $project `
        --startup-project $startupProject `
        --context $context
} else {
    Write-Host "Database update canceled."
}

