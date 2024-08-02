using Domain.Response;
using HeyRed.Mime;
using Infrastructure.Dto;
using Infrastructure.Exception;

namespace Domain.UseCases;

public class GetStorage {
    
    public async Task<BaseResponse> Of(string path) {
        if (!File.Exists(path)) {
            throw new NullResponseException(MessageConstant.ResponseEmpty);
        }

        var mimeType = MimeTypesMap.GetMimeType(path);
        return new BaseResponse(MessageConstant.ResponseOk, CreateStorage(path, UriToStream(path), mimeType));
    }

    public static FileResponse CreateStorage(string path, Task<Stream> stream, string mimeType) {
        return new FileResponse(stream, path, mimeType);
    }
    
    private static async Task<Stream> UriToStream(string filePath) {
        var memoryStream = new MemoryStream();
        await using (var stream = new FileStream(filePath, FileMode.Open)) {
            await stream.CopyToAsync(memoryStream);
        }
        
        memoryStream.Position = 0;
        return memoryStream;
    }
    
}