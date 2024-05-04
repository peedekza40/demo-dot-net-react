#dotnet tool install --global dotnet-ef --version 8.0.4

dotnet ef migrations add InitialDB --context PracticeDotnetReactContext --project PracticeReactApp.Migrations --startup-project PracticeReactApp.Server