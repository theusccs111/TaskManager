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
- As tarefas poderia de alguma forma sincronizar com algum dispositivo de chat como Teams, Slack, para todos verem as altera��es.


### 3. Melhorias

- Implementaria autentica��o JWT e controle de sess�o
- Implementaria a parte de Front-end em Angular com Antdesign para facilitar o uso
- Acredito que seria melhor uma classifica��o para a tarefa como Back-end, Front-end, Analise, Reuni�o para poder criar relat�rios baseado nisso e assim dizer em qual tipo de tarefa foi gasto mais tempo
- Implementaria o cadastro de usu�rio