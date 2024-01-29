# Microserviço de Pedidos
Este é o microserviço de pedidos para o sistema de gerenciamento de uma lanchonete. Ele é responsável por gerenciar e processar os pedidos dos clientes.

### Tecnologias Utilizadas
•	.NET Core
•	Docker
•	AWS SQS
• MongoDB

### Como Executar o Projeto

1.	Clone o repositório para a sua máquina local usando git clone.
   
2.	Navegue até a pasta do projeto e execute dotnet restore para restaurar as dependências do projeto.
   
3.	Execute dotnet run para iniciar o microserviço.
   
### Como Executar os Testes
Execute dotnet test na raiz do projeto para rodar os testes unitários.


### Estrutura do Projeto

•	Controllers/: Contém os controladores que gerenciam as requisições HTTP.

•	Services/: Contém os serviços que contêm a lógica de negócios.

•	Repositories/: Contém os repositórios que gerenciam a persistência de dados.

•	Models/: Contém as classes de modelo que representam os objetos de negócios.

•	Tests/: Contém os testes unitários para o microserviço.

### API Endpoints

•	POST /pedidos: Cria um novo pedido.

•	GET /pedidos: Retorna todos os pedidos.

•	GET /pedidos/{status}: Retorna uma lista de pedidos de um status específico.

•	GET /pedidos/{id}: Retorna um pedido específico pelo ID.

•	PUT /pedidos/{id}: Atualiza um pedido específico.

•	PATCH /pedidos/{id}: Atualiza o status de um pedido específico.


### Contribuindo
Por favor, leia o CONTRIBUTING.md para detalhes sobre o nosso código de conduta e o processo para enviar pedidos pull para nós.

### Licença
Este projeto está licenciado sob a licença MIT - veja o arquivo LICENSE.md para mais detalhes.
