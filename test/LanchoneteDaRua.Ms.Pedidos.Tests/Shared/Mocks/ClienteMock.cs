using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido.InputsAuxiliar;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;

public static class ClienteMock
{
    private static readonly Guid Id = new Guid("cd2dd635-125e-4a0a-be7d-713b775212bc");
    private static readonly string NomeCompleto = "Joao testador";
    private static readonly string Email ="joaotestador@lanchonetedarua.com";

    public static Cliente ClienteFake()
    {
        return new Cliente(Id, NomeCompleto, Email);
    }
    
    public static ClienteInput ClienteInputFaker()
    {
        return new ClienteInput
        {
            Id = Id,
            NomeCompleto = NomeCompleto,
            Email = Email
        };
    }
}