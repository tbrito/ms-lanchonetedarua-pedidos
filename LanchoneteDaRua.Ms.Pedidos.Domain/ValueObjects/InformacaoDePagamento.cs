namespace LanchoneteDaRua.Ms.Pedidos.Domain.ValueObjects;

public record InformacaoDePagamento(string NumeroDoCartao, string NomeCompleto, string DataExpiracao, string Cvv);