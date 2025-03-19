#!/bin/bash

echo "Welcome to the AdventEchoMigrationsTools"

# Ask which context to use
echo "Choose the context:"
echo "1 - AdventEchoIdentityDbContext"
echo "2 - AdventEchoDbContext"
read -p "Enter the number of the desired option: " CONTEXT_OPTION

# Set variables based on choice
if [[ "$CONTEXT_OPTION" == "1" ]]; then
  CONTEXT="AdventEchoIdentityDbContext"
  PROJECT="src/Services/Identity/Infrastructure/AdventEcho.Identity.Infrastructure.csproj"
  STARTUP_PROJECT="src/Presentation/Identity/AdventEcho.Presentation.Identity.csproj"
elif [[ "$CONTEXT_OPTION" == "2" ]]; then
  CONTEXT="AdventEchoDbContext"
  PROJECT="src/AdventEcho.Infrastructure.DataAccess/GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.csproj"
  STARTUP_PROJECT="src/Presentation/Identity/AdventEcho.Presentation.Identity.csproj"
else
  echo "Invalid option. Exiting..."
  exit 1
fi

# Ask for the migration name
read -p "Enter the migration name: " MIGRATION_NAME

# Create the migration
dotnet ef migrations add "$MIGRATION_NAME" \
  --project "$PROJECT" \
  --startup-project "$STARTUP_PROJECT" \
  --context "$CONTEXT"

# Ask if the user wants to run the update
read -p "Do you want to run 'dotnet ef database update'? (y/n): " CONFIRM_UPDATE

# Run the update if the answer is "y" or "Y"
if [[ "$CONFIRM_UPDATE" =~ ^[Yy]$ ]]; then
  dotnet ef database update \
    --project "$PROJECT" \
    --startup-project "$STARTUP_PROJECT" \
    --context "$CONTEXT"
else
  echo "Database update canceled."
fi
