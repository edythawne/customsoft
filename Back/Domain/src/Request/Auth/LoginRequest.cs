using System.ComponentModel.DataAnnotations;

namespace Domain.Request.Auth;

public class LoginRequest {
    
    [Required(ErrorMessage = "El email es requerido.")]
    public string Email { set; get; } = null!;
    
    [Required(ErrorMessage = "La contraseña es requerida.")] 
    public string Password { set; get; } = null!;
    
}