using Application;

var startup = new Startup();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.InitSwagger();
builder.Services.InitMediator();
builder.Services.InitDatabase(builder.Configuration);
builder.Services.InitRepositories(builder.Configuration);
builder.Services.InitMapper();
builder.Services.InitToken(builder.Configuration);
startup.ConfigureLogger(builder);

var app = builder.Build();
startup.ConfigureMiddleware(app, builder.Environment);

app.Run();