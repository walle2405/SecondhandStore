namespace SecondhandStore.Custom;

public class BlobResponse
{
    public BlobResponse()
    {
        Blob = new Blob();
    }

    public string? Status { get; set; }
    public bool Error { get; set; }
    public Blob Blob { get; set; }
}