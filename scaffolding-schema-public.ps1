dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=practice_dotnet_react;User Id=postgres;Password=pass@word1;" Npgsql.EntityFrameworkCore.PostgreSQL --project PracticeReactApp.Core --context-dir Data --output-dir Data/Entities --force --table public.Menu --table public.RoleMenu --table public.APIEndpoint --table public.RoleAPIEndpoint --table public.Role