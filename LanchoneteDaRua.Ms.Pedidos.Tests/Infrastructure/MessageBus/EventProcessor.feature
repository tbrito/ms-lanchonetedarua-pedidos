Funcionalidade: Processamento de Eventos
    Para processar eventos de domínio de forma eficiente
    Como um sistema de processamento de eventos
    Eu quero enviar eventos serializados para uma fila de mensagens

Cenário: Processar uma lista de eventos de domínio
    Dado que eu tenho uma lista de eventos de domínio
    Quando eu processar esses eventos
    Então uma fila deve ser criada
    E cada evento deve ser serializado e enviado para a fila
