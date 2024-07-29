namespace Domain.Request.Storage.Request;

public class FilesRequest {
    
    private string Storage;
    private string Folder;
    private List<FileRequest> Files = new();

    protected void SetStorage(string storage) {
        Storage = storage;
    }
    
    public string GetStorage() {
        return Storage;
    }
    
    protected void SetFolder(string parent) {
        Folder = parent;
    }
    
    public string GetFolder() {
        return Folder;
    }

    public List<FileRequest> GetFiles() {
        return Files;
    }

    protected void setFile(FileRequest model) {
        Files.Add(model);
    }
    
}