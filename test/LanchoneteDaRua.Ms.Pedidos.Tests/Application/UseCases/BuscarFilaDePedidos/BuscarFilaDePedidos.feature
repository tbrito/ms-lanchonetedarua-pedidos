Funcionalidade: Buscar Fila de Pedidos
Como um sistema de gerenciamento de pedidos
Eu quero buscar uma fila de pedidos em preparação
Para gerenciar a sequência de pedidos a serem processados

    Cenário: Buscar pedidos com status 'Em preparação'
        Dado que existem pedidos com status '3'
        Quando eu buscar pela fila de pedidos
        Então a lista de pedidos em preparação deve ser retornada