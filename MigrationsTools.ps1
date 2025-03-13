# Ask which context to use
Write-Host "Choose the context:"
Write-Host "1 - AdventEchoIdentityDbContext"
Write-Host "2 - AdventEchoDbContext"
$contextOption = Read-Host "Enter the number of the desired option"

# Set variables based on choice
if ($contextOption -eq "1") {
    $context = "AdventEchoIdentityDbContext"
    $project = "src/GeekSevenLabs.AdventEcho.Infrastructure.Identity/GeekSevenLabs.AdventEcho.Infrastructure.Identity.csproj"
} elseif ($contextOption -eq "2") {
    $context = "AdventEchoDbContext"
    $project = "src/GeekSevenLabs.AdventEcho.Infrastructure.DataAccess/GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.csproj"
} else {
    Write-Host "Invalid option. Exiting..."
    exit
}

$startupProject = "src/GeekSevenLabs.AdventEcho.Presentation.Web/GeekSevenLabs.AdventEcho.Presentation.Web.csproj"

# Ask for the migration name
$migrationName = Read-Host "Enter the migration name"

# Create the migration
dotnet ef migrations add $migrationName `
  --project $project `
  --startup-project $startupProject `
  --context $context

# Ask if the user wants to run the update
$confirmUpdate = Read-Host "Do you want to run 'dotnet ef database update'? (y/n)"

# Run the update if the answer is "y" or "Y"
if ($confirmUpdate -match "^[Yy]$") {
    dotnet ef database update `
      --project $project `
      --startup-project $startupProject `
      --context $context
} else {
    Write-Host "Database update canceled."
}
