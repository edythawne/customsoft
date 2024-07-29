namespace Domain;

public class HttpContextInformation {
    
    public string? Path { get; set; }
    public string? Query { get; set; }
    public string? Token { get; set; }
    
    public HttpContextInformation(string? path, string? query, string? token) {
        Path = path;
        Query = query;
        Token = token;
    }
    
}