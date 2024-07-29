using Domain.Request.Storage.Request;
using Domain.Response;
using Microsoft.Extensions.Logging;

namespace Domain.UseCases;

public class CreateStorage {
    
    //private readonly ILogger<CreateStorage> Logger;

    //public CreateStorage(ILogger<CreateStorage> logger) {
    //    Logger = logger;
    //}

    public async Task<BaseResponse> Of(FilesRequest validators) {
        try {
            var paths = new List<string>();
        
            foreach (var file in validators.GetFiles()) {
                var filePath = CreatePathOnStorage(validators.GetStorage(), validators.GetFolder(), file.GetFileName());
                paths.Add(filePath);

                await using var stream = await file.GetStream();
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await stream.CopyToAsync(fileStream);
            }

            //Logger.LogInformation("Se han subido los archivos proporcionados");
            return new BaseResponse("Ok", paths);
        } catch (Exception exception) {
            //Logger.LogInformation("Se ha producido un error al subir archivos proporcionados");
            return new BaseResponse("Bad", null);
        }
    }
    
    private string CreatePathOnStorage(string context, string parent, string filename) {
        var folder = Path.Combine(context, parent);
        
        if (!Directory.Exists(folder)) {
            Directory.CreateDirectory(folder);
        }
        
        return Path.Combine(folder, filename);
    }

    
}