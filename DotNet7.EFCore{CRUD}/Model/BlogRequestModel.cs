namespace DotNet7.EFCore_CRUD_.Model;

public class BlogRequestModel
{
    //public long BlogId { get; set; }
    public string BlogTitle { get; set; } = null!;
    public string BlogAuthor { get; set; } = null!;
    public string BlogContent { get; set; } = null!;
    public bool IsActive { get; set; }
}
