using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Domain;

public class TokenService {
    private readonly string _issuer;
    private readonly string _key;

    public TokenService(string issuer, string key) {
        _issuer = issuer;
        _key = key;
    }

    /**
     * Generar token
     */
    public string GenerateToken(int idUser) {
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, idUser.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    /**
     * Obtener id guardada en el token
     */
    public static int? GetUserIdFromToken(string token) {
        var handler = new JwtSecurityTokenHandler();

        try {
            // Leer el token
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // Verificar si el token es un JwtSecurityToken
            if (jsonToken != null) {
                
                // Obtener el claim del usuario
                var userIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null) {
                    if (int.TryParse(userIdClaim.Value, out int userId)) {
                        return userId;
                    }
                }
            }
        }
        catch (Exception ex) {
            Console.WriteLine($"Error al extraer el ID del usuario: {ex.Message}");
        }

        return null;
    }

}