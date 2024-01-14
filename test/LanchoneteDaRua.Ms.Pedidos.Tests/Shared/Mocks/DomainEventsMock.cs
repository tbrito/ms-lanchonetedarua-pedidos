using AutoBogus;
using Bogus;
using LanchoneteDaRua.Ms.Pedidos.Domain.Events;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;

public static class DomainEventsMock
{
  public static Faker<PedidoCriado> PedidoCriadoFake => new AutoFaker<PedidoCriado>();
}