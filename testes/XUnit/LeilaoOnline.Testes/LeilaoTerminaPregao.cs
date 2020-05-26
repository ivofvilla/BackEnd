using Alura.LeilaoOnline.Core;
using LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeilaoOnline.Testes
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250 , new int[] { 800, 1150, 1400 ,1250 })]
        public void RetornaValorSuperiorMaisProxioLeilaoNessaModalidade(int valorDestino, int valorEsperado, int[] ofertas)
        {
            //arranjo - cecnário
            IModalidadeAvaliacao modalide = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Vanh Gogh", modalide);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }

            //act - método sob testes
            leilao.TerminaPregao();

            //assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        [InlineData(1000, new double[] { 800, 900, 990, 1000 })]
        public void RetornaMaiorValorDadoLeilaoComLeilaoPeloMenosUmLance(double? valorEsperado, double[] ofertas)
        {
            //arranjo - cenário
            var modalidae = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidae);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }

            //act - método sob testes
            leilao.TerminaPregao();

            //assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionQuandoLeilaoNaoForIniciado()
        {
            //arranjo - cecnário
            var modalidae = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidae);
            leilao.IniciaPregao();

            //assert
            var excecaoObtida = Assert.Throws<System.InvalidOperationException>(
                //act - método sob testes
                () => leilao.TerminaPregao()
            );

            Assert.Equal("Nao pode encerrar o pregao se nao iniciar", excecaoObtida.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoComLeilaoSemLance()
        {
            //arranjo - cenário
            var modalidae = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidae);
            leilao.IniciaPregao();

            //act - método sob testes
            leilao.TerminaPregao();

            //assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

    }
}

