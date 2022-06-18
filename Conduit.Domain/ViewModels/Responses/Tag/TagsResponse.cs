namespace Conduit.Domain.ViewModels
{
    public class TagsResponse
    {
        public List<string> Tags { get; set; }
        public TagsResponse(List<string> tags)
        {
            Tags = tags;
        }
    }
}