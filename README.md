
# Teste Duett Software

Sistema de login, criação de usuarios, login e logout.
O Administrador pode visualizar e deletar outros usuarios.
O usuario comum pode apenas logar, deslogar e visualizar a pagina inicial.

## Como rodar o projeto

> Abra a solução, defina "UserControl.Api" como projeto de inicialização. Clique em "Ferramentas > Gerenciador de Pacotes do NuGet > Console de Gerenciador de Pacotes",
na aba "Projeto padrão", selecione UserControl.Identity.
Execute o comando `Add-Migration InitialMigration`, após finalizar, execute `Update-Database`
Isso gerará um usuário admin e um usuário comum.

Após isso, clique com o botão direito na solução e selecione `Configurar Projetos de Inicialização` e selecione `Varios Projetos de Inicialização`, selecione Api e UI para iniciarem juntos.

Ao rodar, o Swagger e a UI em Blazor serão iniciados.

### Usuarios padrões

email: `admin@duett.com`
senha: `$Admin1`

email: `user@duett.com`
senha: `$User1`