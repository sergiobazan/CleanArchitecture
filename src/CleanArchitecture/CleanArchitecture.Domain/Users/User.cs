using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users.Events;

namespace CleanArchitecture.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, Nombre nombre, Apellido apellido, Email email) : base(id)
    {
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
    }

    public Nombre Nombre { get; private set; }
    public Apellido Apellido { get; private set; }
    public Email Email { get; private set; }

    public static User Create(Nombre nombre, Apellido apellido, Email email)
    {
        User user = new(new Guid(), nombre, apellido, email);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }
}