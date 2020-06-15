***********************
   Scorecard App
***********************

Descrição: POC de aplicação com busca no elasticsearch, dashboard para visualização de eventos e
controle de acesso.

Tecnologias utilizadas:

- Asp Net
- C#
- .Net Core 3.1
- Razor Pages
- jQuery
- SQLite
- Elasticsearch 7.7.1
- Logstash 6.2.4
- Kibana 7.7.1

Instruções de instalação:

1. Elasticsearch:

1.1 Substitua o arquivo config/elasticsearch.yml, no direrório onde o elasticsearch está instalado
pelo arquivo arquivo elastic/elasticsearch.yml, no diretório da aplicação.

1.2 Inicialize o elasticsearch

1.3 Crie os template de leitura de csv e do log de buscas:

1.3.1 Realize uma requisição post para localhost:9200/_template/scorecard_template,
colocando o conteúdo do arquivo elastic/elasticsearch-template-scorecard.json no corpo da requisição

1.3.1 Realize uma requisição post para localhost:9200/_template/scorecard_search_template,
colocando o conteúdo do arquivo elastic/elasticsearch-template-scorecard-search.json no corpo da requisição

1.4 Crie os índices:

1.4.1 Realize uma requisição PUT para localhost:9200/scorecard
1.4.2 Realize uma requisição PUT para localhost:9200/scorecard-search

2. Logstash:

2.1 O Logstash não necessita de nenhuma configuração especial. As configurações de fábrica
devem ser utilizadas em sua inicialização.

2.2 Carregamento dos dados do csv para o elasticsearch:
2.2.1 Edite o arquivo elastic/scorecard-data.conf, na linha 3, para informar o caminho para os
arquivos .csv que serão importados para o índice.
2.2.2 Inicialize o Logstash utilizando o arquivo elastic/scorecard-data.conf
2.2.3 Quando o carregamento tiver sido concluído, esta instância do Logstash poderá ser fechada.

2.3 Inicialização do Logstash para log de buscas
O sistema irá usar o logstash para logar as buscas realizadas. Para isso:

2.3.1 Inicialize o Logstash utilizando o arquivo elastic/logstash-http.conf

3. Kibana:

3.1 Inicialize o kibana com as configurações de fábrica
3.2 Acesse o kibana e em Management/Saved Objects, importe o arquivo elastic/kibana-dashboards.ndjson, para copiar os gráficos e a dashboard.

4. Instale a aplicação web no iss. Copie a pasta publish/scorecard para o iis.

5. Acesse a aplicação em localhost/scorecard

Caso ocorra algum problema para rodar a aplicação. Peço que a execute pelo visual studio.
