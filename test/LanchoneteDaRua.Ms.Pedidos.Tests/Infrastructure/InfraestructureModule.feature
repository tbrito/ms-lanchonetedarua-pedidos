Funcionalidade: Configuração da Infraestrutura
Como um desenvolvedor
Quero garantir que os serviços de infraestrutura sejam corretamente configurados
Para que possam ser injetados e utilizados no aplicativo

    Cenário: Adicionar configuração do MongoDB ao IServiceCollection
        Dado que o IServiceCollection está configurado
        Quando adiciono o módulo de infraestrutura
        Então o cliente e o banco de dados do MongoDB devem estar disponíveis para injeção

    Cenário: Adicionar repositórios ao IServiceCollection
        Dado que o IServiceCollection está configurado
        Quando adiciono o módulo de infraestrutura
        Então os repositórios devem estar disponíveis para injeção

    Cenário: Adicionar configuração do Message Bus ao IServiceCollection
        Dado que o IServiceCollection está configurado
        Quando adiciono o módulo de infraestrutura
        Então os serviços do Message Bus devem estar disponíveis para injeção