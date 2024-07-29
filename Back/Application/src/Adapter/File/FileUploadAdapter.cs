using Domain.Request.Storage.Request;

namespace Application.Adapter.File;

public class FileUploadAdapter  : FileRequest{
    
    public IFormFile File { set; get; }
    public bool ForceRename { set; get; } 

    public void ToModel() {
        GetFileName();
        SetExtension(Path.GetExtension(File.FileName));
        SetFileName($"{GetBaseName()}{GetExtension()}");
        SetStream(ToStream(File));
    }

    public bool IsValidExtension(string[] allowedExtensions) {
        var fileExtension = Path.GetExtension(File.FileName);
        return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
    }

    private async Task<Stream> ToStream(IFormFile file) {
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;
        return stream;
    }

    private void GetFileName() {
        if (ForceRename) {
            SetBaseName(Guid.NewGuid().ToString());
        }

        if (!ForceRename) {
            SetBaseName(Path.GetFileNameWithoutExtension(File.FileName));
        }
    }
    
}