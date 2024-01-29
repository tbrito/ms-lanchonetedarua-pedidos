Funcionalidade: Criar Pedido
Como um sistema de gerenciamento de pedidos
Eu quero criar novos pedidos
Para adicionar pedidos ao sistema e processar eventos associados

    Cenário: Criar um novo pedido com sucesso
        Dado que eu tenho os detalhes de um novo pedido
        Quando eu criar o pedido
        Então um novo pedido deve ser criado

    Cenário: Erro ao criar um pedido devido a falha no banco de dados
        Dado que eu tenho os detalhes de um novo pedido
        Mas ocorre um problema no banco de dados
        Quando eu tentar criar o pedido
        Então um erro com o código 'InternalServerError' deve ser retornado