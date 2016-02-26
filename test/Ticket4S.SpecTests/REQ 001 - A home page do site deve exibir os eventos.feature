Funcionalidade: A home page do site deve exibir os eventos
	Eu, como usuário do Site devo poder
	Visualizar a lista de Eventos disponíveis.

Cenário: Preciso escolher o Evento que desejo comprar o Ingresso
	Quando Entar na Pagina Principal do Site
	Então o resultado deve conter uma lista de eventos disponiveis
	E a Listagem deve conter somente eventos futuros
	E quando um evento ainda tem ingressos deve ser exibito a opcão de comprar ingresso
	E quando um evento já se encontra esgotado deve ser exibito mas sem a opcão de comprar ingresso 