using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users;

public static class UserErrors
{
   public static Error NotFound = new(
    "User.Found",
    "No existe el usuario buscado por este id"
   ); 

   public static Error InvalidCredencials = new(
    "User.InvalidCredencials",
    "Las credenciales son incorrectas"
   ); 
}