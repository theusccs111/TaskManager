# Projeto .NET Core com Docker e SQL Server

Este é um projeto .NET Core configurado para rodar em um ambiente Docker. Abaixo estão as instruções para rodar o projeto utilizando Docker.

## Pré-requisitos

Antes de começar, você precisa ter as seguintes ferramentas instaladas no seu sistema:

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Passos para Configuração e Execução

Clone o repositório para sua máquina local:

```bash
git clone https://github.com/theusccs111/TaskManager
cd TaskManager
```

Certifique-se de que o Docker e o Docker Compose estão instalados corretamente. Para isso, abra o terminal e execute:

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

as apis estão disponiveis em http://localhost:5000/

### 2. Perguntas ao PO

- O projeto terá autenticação e gestão de acesso ? Porque hoje não tem nenhum controle dessa parte como Token JWT por exemplo.
- Quais outros relatórios o ajudaria a analisar os usuários com suas tarefas, como algum índice de produtividade.
- E acho que seria melhor poder trocar a prioridade da tarefa, porque vai que o usuário cria ela errada, então tem que excluir essa e criar outra ?
- Devemos incluir notificações para outros usuários caso um comentário seja adicionado a uma tarefa?
- Como será gerenciado o acesso por função para funcionalidades específicas, como os relatórios de desempenho para "gerentes"?
- Existe algum limite de tamanho para comentários nas tarefas?
- Além do número médio de tarefas concluídas, existem outras métricas de desempenho que o time deseja monitorar?
- Os relatórios devem incluir filtros ou agrupamentos específicos (ex.: por projeto, por data, etc.)?
- Há requisitos de exportação desses relatórios (Excel)?
- Haverá algum controle de dependências entre tarefas, como uma tarefa que só pode ser iniciada após outra ser concluída?
- O limite de 20 tarefas por projeto pode ser configurável?

### 3. Melhorias

- Implementar o micro serviço para melhorar a escalabilidade.
- Implementar a autenticação JWT
- Adicionar um cache distribuído (como Redis) para armazenar dados de consulta de alta frequência e reduzir a carga no banco de dados.
- Implementar alertas integrados com plataformas de comunicação do time (ex. Slack, Teams) para notificações automáticas de erros críticos.
- Verificar índices nas tabelas usadas com mais frequência para otimizar buscas, especialmente para relatórios e consultas de alto volume.
- Adicionar suporte robusto a paginação e filtragem nas listas de projetos e tarefas para melhorar a experiência de navegação e o tempo de resposta em consultas.