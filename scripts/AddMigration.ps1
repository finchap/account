Param(
  [string]$comment
)

dotnet ef migrations add $comment -p .\src\Finchap.Account.Infrastructure\ -s .\src\Finchap.Account.AdminCLI\