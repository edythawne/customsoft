using Domain;
using Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application;

public class BaseController : ControllerBase {

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediator _mediator;

    public BaseController(IMediator mediator, IHttpContextAccessor httpContextAccessor) {
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }

    protected async Task<IActionResult> Execute(object command) {
        if (!ModelState.IsValid) {
            return Conflict(ModelState.ToString());
        }

        var response = await _mediator.Send(command);
        return await GetResponse(response);
    }

    protected async Task<IActionResult> GetResponse(object? response) {
        return Ok(response);
    }
    
    protected async Task<IActionResult> GetResponseStorage(BaseResponse response) {
        if (response.Data is FileResponse binary) {
            return binary.Path != null ? File(await binary.Stream, binary.MimeType, Path.GetFileName(binary.Path)) : File(await binary.Stream, binary.MimeType);
        }

        return await GetResponse(response);
    }

    protected HttpContextInformation? GetHttpContext() {
        var request = _httpContextAccessor.HttpContext?.Request;

        if (request == null) {
            return null;
        }

        var token = "";
        string value = request?.HttpContext.Request.Headers["Authorization"]!;
        
        var path = request?.Path;
        var query = request?.QueryString.Value;

        if (value != null) {
            token = value.Replace("Bearer ", "");
        }
        
        return new HttpContextInformation(path, query, token);
    }

}