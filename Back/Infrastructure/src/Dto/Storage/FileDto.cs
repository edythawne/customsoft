namespace Infrastructure.Dto.Storage;

public class FileDto {

    private string FileName { get; set; }
    private string Extension { get; set; }
    private string BaseName { get; set; }
    private Task<Stream> FileStream { get; set; }
    
}