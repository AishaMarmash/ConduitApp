namespace Conduit.Domain.ViewModels
{
    public class ProfileResponseDto
    {
        public string Username { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public bool Following { get; set; }
    }
}