using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido.InputsAuxiliar;

public record ClienteInput
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public Cliente ToEntity()
    {
        return new Cliente(Id, NomeCompleto, Email);
    }
};