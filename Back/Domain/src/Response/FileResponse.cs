namespace Domain.Response;

public class FileResponse {
    
    public Task<Stream> Stream { private set; get; }
    public string? Path { private set; get; }
    public string MimeType { private set; get; }

    public FileResponse(Task<Stream> stream, string path, string mimeType) {
        Stream = stream;
        Path = path;
        MimeType = mimeType;
    }
    
    public FileResponse(Task<Stream> stream, string mimeType) {
        Stream = stream;
        MimeType = mimeType;
    }
    
}