using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido.InputsAuxiliar;
using LanchoneteDaRua.Ms.Pedidos.Domain.ValueObjects;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;

public class InformacaoDePagamentoMock
{
    private static readonly string NomeCompleto = "Joao Testador";
    private static readonly string NumeroDoCartao = "4565152625132541";
    private static readonly string DataExpiracao = "07/2023";
    private static readonly string Cvv = "450";

    public static InformacaoDePagamento InformacaoDePagamentoFake()
    {
        return new InformacaoDePagamento(NumeroDoCartao, NomeCompleto, DataExpiracao, Cvv);
    }

    public static InformacaoDePagamentoInput InformacaoDePagamentoInputFaker()
    {
        return new InformacaoDePagamentoInput
        {
            NomeCompleto = NomeCompleto,
            NumeroDoCartao = NumeroDoCartao,
            DataExpiracao = DataExpiracao,
            Cvv = Cvv
        };
    }
}