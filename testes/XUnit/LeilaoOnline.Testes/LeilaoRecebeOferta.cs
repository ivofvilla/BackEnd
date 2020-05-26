using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;
using LeilaoOnline.Core;

namespace LeilaoOnline.Testes
{

    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(2, new double[] {800, 900})]
        [InlineData(4, new double[] { 100, 400,600,800 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int quantidade, double[] ofertas)
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            Interessada ultimoCliente = null;
            
            for(int i=0; i < ofertas.Length; i++)
            {
                if (i%2 ==0)
                {
                    ultimoCliente = fulano;
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    ultimoCliente = maria;
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }
            leilao.TerminaPregao();

            //act - método sob testes
            leilao.RecebeLance(maria, 1000);
            
            var valorObtido = leilao.Lances.Count();

            Assert.Equal(quantidade, valorObtido);

        }

        [Fact]
        public void NaoAceitaDoisLancesSeguidosDoMesmoCliente()
        {
            //arranjo
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulano, 800);
            
            //act - método sob testes
            leilao.RecebeLance(fulano, 1000);

            var valorObtido = leilao.Lances.Count();

            Assert.Equal(1, valorObtido);


        }
    }
}
