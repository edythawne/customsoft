using System.ComponentModel.DataAnnotations;

namespace Domain.Request.App;

public class CreateUserDetailRequest {
    
    [Required(ErrorMessage = "El user id es requerido.")]
    public long UserId { get; set; }

    [Required(ErrorMessage = "El curp es requerido.")]
    [MaxLength(18)]
    public string Curp { get; set; }

    [Required(ErrorMessage = "La fecha de nacimiento es requerido.")]
    public DateTimeOffset DateOfBirth { get; set; }

    [Required(ErrorMessage = "El lugar de nacimiento es requerido.")]
    public string PlaceOfBirth { get; set; }

    [Required(ErrorMessage = "El archivo de nacimiento es requerido.")]
    public string BirthCertificate { get; set; }

    [Required(ErrorMessage = "El sexo es requerido.")]
    [MaxLength(1)]
    public string Gender { get; set; }
    
}