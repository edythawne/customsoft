namespace Infrastructure.Dto;

public class MessageConstant {
    
    public const string ResponseOk = "Se ha realizado la accion correctamente";
    public const string ResponseEmpty = "Se ha realizado la acción sin resultado existoso";
    public const string ResponseBadAuthentication = "Acceso no autorizado o token invalido";
    
    public const string TokenCreateException = "Se ha producido un error al crear el token";
    public const string TokenReadException = "Se ha producido un error al leer el token";
    
    public const string UserNotHasPermission = "Usuario no permitido para realizar la acción";
    
}