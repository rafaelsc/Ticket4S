using System;
using TechTalk.SpecFlow;

namespace Ticket4S.SpecTests
{
    [Binding]
    public class OUsuarioPodeComprarIngressosParaUmEventosSteps
    {
        [When(@"O Usuário selecionar um Evento")]
        public void QuandoOUsuarioSelecionarUmEvento()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"o Usário escolher um Tipo de Ingresso disponível")]
        public void QuandoOUsarioEscolherUmTipoDeIngressoDisponivel()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"o Usuário estiver deslogado")]
        public void QuandoOUsuarioEstiverDeslogado()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"o Usuário estiver logado")]
        public void QuandoOUsuarioEstiverLogado()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"os detalhes do Evento devem ser exibidos")]
        public void EntaoOsDetalhesDoEventoDevemSerExibidos()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"os Tipos de Ingressos devem ser todos listados em conjunto com a informação se ainda estão disponiveis para compra\.")]
        public void EntaoOsTiposDeIngressosDevemSerTodosListadosEmConjuntoComAInformacaoSeAindaEstaoDisponiveisParaCompra_()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"ele será solicitado que se logue")]
        public void EntaoEleSeraSolicitadoQueSeLogue()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"ele irá confirmar a compra de ingresso")]
        public void EntaoEleIraConfirmarACompraDeIngresso()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
