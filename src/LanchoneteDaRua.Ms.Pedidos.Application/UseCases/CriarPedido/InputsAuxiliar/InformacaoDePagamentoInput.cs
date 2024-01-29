using LanchoneteDaRua.Ms.Pedidos.Domain.ValueObjects;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido.InputsAuxiliar;

public record InformacaoDePagamentoInput
{
    public string NumeroDoCartao { get; init; }
    public string NomeCompleto { get; init; }
    public string DataExpiracao { get; init; }
    public string Cvv { get; init; }
    public InformacaoDePagamento ToEntity()
    {
        return new InformacaoDePagamento(NumeroDoCartao, NomeCompleto, DataExpiracao, Cvv);
    }
}