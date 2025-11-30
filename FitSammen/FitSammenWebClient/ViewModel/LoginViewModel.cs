namespace FitSammenWebClient.ViewModel
{
    public class LoginViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Redirect URL after successful login
        public string? ReturnUrl { get; set; }
    }
}
