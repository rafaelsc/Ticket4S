using System;
using TechTalk.SpecFlow;

namespace Ticket4S.SpecTests
{
    [Binding]
    public class AHomePageDoSiteDeveExibirOsEventosSteps
    {
        [When(@"Entar na Pagina Principal do Site")]
        public void QuandoEntarNaPaginaPrincipalDoSite()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"o resultado deve conter uma lista de eventos disponiveis")]
        public void EntaoOResultadoDeveConterUmaListaDeEventosDisponiveis()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"a Listagem deve conter somente eventos futuros")]
        public void EntaoAListagemDeveConterSomenteEventosFuturos()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"quando um evento ainda tem ingressos deve ser exibito a opcão de comprar ingresso")]
        public void EntaoQuandoUmEventoAindaTemIngressosDeveSerExibitoAOpcaoDeComprarIngresso()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"quando um evento já se encontra esgotado deve ser exibito mas sem a opcão de comprar ingresso")]
        public void EntaoQuandoUmEventoJaSeEncontraEsgotadoDeveSerExibitoMasSemAOpcaoDeComprarIngresso()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
