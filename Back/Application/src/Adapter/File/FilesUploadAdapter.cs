using System.ComponentModel.DataAnnotations;
using Domain.Request.Storage.Request;

namespace Application.Adapter.File;

public class FilesUploadAdapter : FilesRequest {
    [Required]
    public List<IFormFile> Files { get; set; }
    
    public string? Folder { get; set; }

    public bool IsRename { get; set; } = false;

    public void ToModels(string directory) {
        SetStorage(directory);
        SetFolder(Folder);
        
        foreach (var ifile in Files) {
            var file = new FileUploadAdapter {
                File = ifile,
                ForceRename = IsRename
            };
            
            file.ToModel();
            setFile(file);
        }
    }
    
}