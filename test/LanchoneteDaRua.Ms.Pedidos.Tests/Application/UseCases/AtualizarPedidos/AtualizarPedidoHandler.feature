Funcionalidade: AtualizarPedido
Para atualizar pedidos
Como um usuário
Eu quero ser capaz de atualizar detalhes do pedido

    Cenário: Atualizando um pedido existente com sucesso
        Dado Um pedido existente com ID 'e4c32c9f-8f3e-4e81-8433-0b8cd1df9e77'
        E O pedido precisa ser atualizado
        Quando Eu atualizo o pedido
        Então O pedido deve ser atualizado com sucesso

    Cenário: Falha ao atualizar um pedido devido a erro no banco de dados
        Dado Um pedido existente com ID 'e4c32c9f-8f3e-4e81-8433-0b8cd1df9e77'
        E O pedido precisa ser atualizado
        E Ocorre um erro no banco de dados
        Quando Eu atualizo o pedido
        Então A atualização deve falhar com um erro de banco de dados