Funcionalidade: Gerenciamento de Pedidos
Como um usuário do sistema
Quero poder adicionar, buscar e atualizar pedidos
Para gerenciar pedidos no sistema

Cenário: Buscar um pedido por ID
    Dado que existe um pedido com o ID 'cd2dd635-125e-4a0a-be7d-713b775212bc'
    Quando eu buscar o pedido com o ID 'cd2dd635-125e-4a0a-be7d-713b775212bc'
    Então o pedido correspondente deve ser retornado

Cenário: Adicionar um novo pedido
    Dado que eu tenho um novo pedido
    Quando eu adicionar este pedido
    Então o pedido deve ser salvo no sistema

Cenário: Atualizar um pedido existente
    Dado que existe um pedido com o ID 'cd2dd635-125e-4a0a-be7d-713b775212bc'
    E eu tenho uma atualização para esse pedido
    Quando eu atualizar o pedido com o ID 'cd2dd635-125e-4a0a-be7d-713b775212bc'
    Então o pedido atualizado deve ser salvo no sistema