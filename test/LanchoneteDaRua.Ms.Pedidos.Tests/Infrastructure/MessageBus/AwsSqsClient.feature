Funcionalidade: Interação com o AWS SQS
    Para interagir eficientemente com o serviço de fila da AWS SQS
    Como um cliente AWS SQS
    Eu quero criar filas, enviar mensagens e receber mensagens

Cenário: Criar uma nova fila
    Dado que eu desejo criar uma nova fila com o nome "minha-fila-teste"
    Quando eu solicitar a criação da fila
    Então uma nova fila chamada "minha-fila-teste" deve ser criada
    E o URL da fila recém-criada chamada "minha-fila-teste" deve ser retornado

Cenário: Enviar uma mensagem para a fila
    Dado que eu tenho uma fila com o URL "minha-fila-teste"
    E eu tenho uma mensagem para enviar
    Quando eu enviar a mensagem para a fila
    Então a mensagem deve ser adicionada à fila "minha-fila-teste"

Cenário: Receber mensagens da fila
    Dado que eu tenho uma fila com mensagens com o URL "url-da-minha-fila"
    Quando eu solicitar para receber mensagens da fila
    Então as mensagens da fila "url-da-minha-fila" devem ser retornadas