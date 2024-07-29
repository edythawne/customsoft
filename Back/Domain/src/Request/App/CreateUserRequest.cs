using System.ComponentModel.DataAnnotations;

namespace Domain.Request.App;

public class CreateUserRequest {

    [Required(ErrorMessage = "El nombre es requerido.")]
    public string Name { set; get; } = null!;

    [Required(ErrorMessage = "El apellidos es requerido.")]
    public string FirstName { set; get; } = null!;
    
    [Required(ErrorMessage = "El email es requerido.")]
    public string Email { set; get; } = null!;
    
    [Required(ErrorMessage = "El departamento es requerido.")] 
    public int FkDepartment { set; get; } = 0;

    [Required(ErrorMessage = "El rol es requerido.")]
    public int FkRol { set; get; } = 0;

}