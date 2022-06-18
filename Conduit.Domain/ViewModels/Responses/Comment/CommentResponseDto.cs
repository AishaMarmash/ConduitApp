namespace Conduit.Domain.ViewModels
{
    public class CommentResponseDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Body { get; set; }
        public ProfileResponseDto Author { get; set; } = new ProfileResponseDto();
    }
}