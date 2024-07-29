using Application.Middleware;
using Serilog;

namespace Application;

public class Startup {
    
    /**
     * Configuracion de Middleware
     */
    public void ConfigureMiddleware(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(builder => { builder.MapControllers(); });
    }
    
    public void ConfigureLogger(WebApplicationBuilder builder) {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() 
            .WriteTo.Console()    
            .WriteTo.File("Logs/netcore.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        builder.Host.UseSerilog();
    }
    
}