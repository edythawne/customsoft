using Application.Adapter.File;
using Domain.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class StorageController : BaseController {
    
    private string? Directory;
    
    public StorageController(IMediator mediator, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(mediator, httpContextAccessor) {
        Directory =  configuration!.GetValue<string>("FileSettings:UploadDirectory");
    }
    
    [HttpPost("store")]
    public async Task<IActionResult> Store([FromForm] FilesUploadAdapter validator) {
        validator.ToModels(Directory!);
        return await GetResponse(await new CreateStorage().Of(validator));
    }
    
    [HttpGet("download")]
    public async Task<IActionResult> Download([FromQuery] string path) {
        return await GetResponseStorage(await new GetStorage().Of(path));
    }
    
}