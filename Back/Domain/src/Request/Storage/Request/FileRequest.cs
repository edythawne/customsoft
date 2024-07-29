namespace Domain.Request.Storage.Request;

public class FileRequest {
    
    private string FileName;
    private string Extension;
    private string BaseName;
    private Task<Stream> FileStream;

    protected void SetFileName(string filename) {
        FileName = filename;
    }

    public string GetFileName() {
        return FileName;
    }

    protected void SetBaseName(string basename) {
        BaseName = basename;
    }

    public string GetExtension() {
        return Extension;
    }
    
    protected void SetExtension(string extension) {
        Extension = extension;
    }

    public string GetBaseName() {
        return BaseName;
    }
    
    protected void SetStream(Task<Stream> stream) {
        FileStream = stream;
    }

    public Task<Stream> GetStream() {
        return FileStream;
    }
    
    
}