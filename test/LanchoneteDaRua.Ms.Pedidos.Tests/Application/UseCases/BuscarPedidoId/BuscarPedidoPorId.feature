Funcionalidade: Buscar Pedido Por ID
Como um sistema de gerenciamento de pedidos
Eu quero buscar um pedido usando seu ID
Para obter detalhes específicos do pedido

    Cenário: Buscar um pedido existente pelo ID
        Dado que já existe um pedido com ID 'e4c32c9f-8f3e-4e81-8433-0b8cd1df9e77'
        Quando eu buscar o pedido com ID 'e4c32c9f-8f3e-4e81-8433-0b8cd1df9e77'
        Então o pedido com ID 'e4c32c9f-8f3e-4e81-8433-0b8cd1df9e77' deve ser retornado

    Cenário: Buscar um pedido que não existe
        Dado que não existe um pedido com ID '24dba73b-c5f9-4042-99dc-25ea602cb61c'
        Quando eu buscar o pedido com ID '24dba73b-c5f9-4042-99dc-25ea602cb61c'
        Então nenhum pedido deve ser retornado