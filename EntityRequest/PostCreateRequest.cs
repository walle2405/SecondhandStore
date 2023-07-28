using System.ComponentModel.DataAnnotations;

namespace SecondhandStore.EntityRequest;

public class PostCreateRequest
{
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int PostStatusId = 3;
    public int CategoryId { get; set; }
    public bool isDonated { get; set; }
    public double Price { get; set; }
    [Required(ErrorMessage = "Need to insert at least 1 image")]
    public IFormFileCollection ImageUploadRequest { get; set; }
}