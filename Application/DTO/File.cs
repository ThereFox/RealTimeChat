namespace Application.DTO;

public class SavingFile
{
    public string FileName { get; init; }
    public string ContentType { get; init; }
    public long Size { get; init; }
    public Stream Content { get; set; }
}