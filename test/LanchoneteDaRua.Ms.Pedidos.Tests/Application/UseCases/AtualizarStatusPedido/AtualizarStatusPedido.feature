Funcionalidade: Atualizar Status do Pedido
Como um sistema de gerenciamento de pedidos
Eu quero atualizar o status de um pedido
Para refletir as mudanças no estado do pedido

    Cenário: Atualizar o status de um pedido para 'Pagamento Rejeitado'
        Dado que existe um pedido com ID 'e4c32c9f-8f3e-4e81-8433-0b8cd1df9e77'
        E o status atual do pedido é 'Recebido'
        Quando eu atualizar o status do pedido para 'PagamentoRejeitado'
        Então a resposta não deve retornar erro

    Cenário: Avançar para o próximo estado do pedido
        Dado que existe um pedido com ID 'e4c32c9f-8f3e-4e81-8433-0b8cd1df9e77'
        E o status atual do pedido é 'Recebido'
        Quando eu atualizar o status do pedido para 'EmPreparacao'
        Então a resposta não deve retornar erro
        
    Cenário: Erro ao salvar no banco de dados
        Dado que existe um pedido com ID 'e4c32c9f-8f3e-4e81-8433-0b8cd1df9e77'
        E o status atual do pedido é 'Recebido'
        Mas ocorre um erro ao salvar no banco
        Quando eu atualizar o status do pedido para 'EmPreparacao'
        Então a resposta deve retornar que houve erro