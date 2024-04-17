using PracticeReactApp.Server.Models;

namespace PracticeReactApp.Server.ViewModels.Account
{
    public class RegisterViewModel: User
    {
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}