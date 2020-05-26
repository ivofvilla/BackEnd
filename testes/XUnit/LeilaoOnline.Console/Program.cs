using Alura.LeilaoOnline.Core;
using LeilaoOnline.Core;
using System;

namespace LeilaoOnline.Console
{
    class Program
    {
        private static void Verifica(double esperado, double obtido)
        {
            var cor = System.Console.ForegroundColor;
            if (esperado == obtido)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Teste ok");
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"deu ruim! Esperado {esperado}, obtido: {obtido}");
            }

            System.Console.ForegroundColor = cor;
        }

        private static void LeilaoComVariosLances()
        {
            //arranjo - cenário
            var modalidae = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidae);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 990);

            //act - método sob testes
            leilao.TerminaPregao();

            //assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);
        }

        private static void LeilaoComUmLance()
        {
            //arranjo - cecnário
            var modalidae = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidae);
            var fulano = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulano, 800); 

            //act - método sob testes
            leilao.TerminaPregao();

            //assert
            var valorEsperado = 800;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);
        }

        static void Main(string[] args)
        {
            //LeilaoComVariosLances();
            //LeilaoComUmLance();
            
            System.Console.ReadKey();
        }
    }
}
