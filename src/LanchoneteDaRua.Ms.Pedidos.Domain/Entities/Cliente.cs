namespace LanchoneteDaRua.Ms.Pedidos.Domain.Entities;

public class Cliente
{
    public Cliente(Guid id, string nomeCompleto, string email)
    {
        Id = id;
        NomeCompleto = nomeCompleto;
        Email = email;
    }

    public Guid Id { get; private set; }
    public string NomeCompleto { get; private set; }
    public string Email { get; private set; }

}