# BlazorFaturas


BlazorFatura
Este projeto é um aplicativo web desenvolvido utilizando Blazor, C#, e SQL Server, criado para importar faturas a partir de arquivos XML em um diretório local e armazená-las em um banco de dados. O projeto também inclui funcionalidades para visualizar, editar e gerenciar essas faturas através de uma interface de usuário simples e intuitiva.

Funcionalidades Principais
Importação de Faturas: Importa faturas automaticamente a partir de arquivos XML localizados em um diretório específico.
Visualização e Edição: Permite visualizar e editar as faturas importadas.
Integração com Banco de Dados: Utiliza Entity Framework Core para persistir os dados das faturas em um banco de dados SQL Server.
Interface de Usuário Amigável: Interface desenvolvida com Blazor, proporcionando uma experiência interativa diretamente no navegador web.
Tecnologias Utilizadas
Blazor: Framework da Microsoft para construção de aplicações web utilizando C# e .NET.
Entity Framework Core: ORM utilizado para mapear objetos .NET para bancos de dados relacionais.
SQL Server: Banco de dados relacional da Microsoft utilizado para armazenar dados das faturas.
C#: Linguagem de programação principal para a lógica de negócios e interação com o banco de dados.
Pré-requisitos
Visual Studio 2022 ou Visual Studio Code com .NET 6.0 SDK instalado.
SQL Server Express ou versão completa instalada localmente.
Diretório local configurado para receber arquivos XML de faturas.
Como Executar
Clone este repositório.
Abra o projeto no Visual Studio.
Verifique e ajuste a conexão com o banco de dados no arquivo appsettings.json.
Compile e execute o projeto.
Contribuições
Contribuições são bem-vindas! Sinta-se à vontade para abrir um PR ou uma issue para discutir novas funcionalidades, melhorias ou correções.