Funcionalidade: O usuário pode comprar ingressos para um eventos
	Eu, como usuário do Site devo poder
	Comprar ingressos para um Evento.

Cenário: Escolhido o Evento que desejo comprar ingressos.
	Quando O Usuário selecionar um Evento
	Então os detalhes do Evento devem ser exibidos
	E os Tipos de Ingressos devem ser todos listados em conjunto com a informação se ainda estão disponiveis para compra.
	
	Quando o Usário escolher um Tipo de Ingresso disponível
	E o Usuário estiver deslogado
	Então ele será solicitado que se logue
	Entao ele irá confirmar a compra de ingresso

	Quando o Usário escolher um Tipo de Ingresso disponível
	E o Usuário estiver logado
	Então ele irá confirmar a compra de ingresso

	  