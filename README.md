# Projeto .NET Core com Docker e SQL Server

Este � um projeto .NET Core configurado para rodar em um ambiente Docker. Abaixo est�o as instru��es para rodar o projeto utilizando Docker.

## Pr�-requisitos

Antes de come�ar, voc� precisa ter as seguintes ferramentas instaladas no seu sistema:

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Passos para Configura��o e Execu��o

Clone o reposit�rio para sua m�quina local:

```bash
git clone https://github.com/theusccs111/TaskManager
cd TaskManager
```

Certifique-se de que o Docker e o Docker Compose est�o instalados corretamente. Para isso, abra o terminal e execute:

```bash
docker --version
docker-compose --version
```

Com o Docker configurado, execute os comandos para subir no seu docker hub

```bash
docker build -t theusccs111/taskmanager -f TaskManager.Web/Dockerfile .

docker push theusccs111/taskmanager
```

e para rodar a imagem no container

```bash
docker run -d -p 5000:80 theusccs111/taskmanager
```

as apis est�o disponiveis em http://localhost:5000/

### 2. Perguntas ao PO

- O projeto ter� autentica��o e gest�o de acesso ? Porque hoje n�o tem nenhum controle dessa parte como Token JWT por exemplo.
- Quais outros relat�rios o ajudaria a analisar os usu�rios com suas tarefas, como algum �ndice de produtividade.
- E acho que seria melhor poder trocar a prioridade da tarefa, porque vai que o usu�rio cria ela errada, ent�o tem que excluir essa e criar outra ?
- Devemos incluir notifica��es para outros usu�rios caso um coment�rio seja adicionado a uma tarefa?
- Como ser� gerenciado o acesso por fun��o para funcionalidades espec�ficas, como os relat�rios de desempenho para "gerentes"?
- Existe algum limite de tamanho para coment�rios nas tarefas?
- Al�m do n�mero m�dio de tarefas conclu�das, existem outras m�tricas de desempenho que o time deseja monitorar?
- Os relat�rios devem incluir filtros ou agrupamentos espec�ficos (ex.: por projeto, por data, etc.)?
- H� requisitos de exporta��o desses relat�rios (Excel)?
- Haver� algum controle de depend�ncias entre tarefas, como uma tarefa que s� pode ser iniciada ap�s outra ser conclu�da?
- O limite de 20 tarefas por projeto pode ser configur�vel?

### 3. Melhorias

- Implementar o micro servi�o para melhorar a escalabilidade.
- Implementar a autentica��o JWT
- Adicionar um cache distribu�do (como Redis) para armazenar dados de consulta de alta frequ�ncia e reduzir a carga no banco de dados.
- Implementar alertas integrados com plataformas de comunica��o do time (ex. Slack, Teams) para notifica��es autom�ticas de erros cr�ticos.
- Verificar �ndices nas tabelas usadas com mais frequ�ncia para otimizar buscas, especialmente para relat�rios e consultas de alto volume.
- Adicionar suporte robusto a pagina��o e filtragem nas listas de projetos e tarefas para melhorar a experi�ncia de navega��o e o tempo de resposta em consultas.