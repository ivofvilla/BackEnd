using Calculadora;
using NUnit.Framework;

namespace Tests
{
    public class CalculadoraTests
    {

        [TestFixture]
        public class CalculadoraSimplesTest
        {
            [SetUp]
            public void Setup()
            {

            }

            [Test]
            public void SomaDoisNumeros()
            {
                var sut = new CalculadoraSimples().Adicionar(5, 5);

                Assert.AreEqual(sut, 10);
            }

            [Test]
            public void MultiplicacaoDoisNumeros()
            {
                var sut = new CalculadoraSimples().Multipicacao(5, 5);

                Assert.AreEqual(sut, 25);
            }
            
            [Test]
            public void DivisaoDoisNumeros()
            {
                var sut = new CalculadoraSimples().Divisao(10, 5);

                Assert.AreEqual(sut, 2);
            }

            [Test]
            public void SubtracaoDoisNumeros()
            {
                var sut = new CalculadoraSimples().Substracao(5, 5);

                Assert.AreEqual(sut, 0);
            }

            [Test]
            public void RestpDoisNumeros()
            {
                var sut = new CalculadoraSimples().Resto(5, 5);

                Assert.AreEqual(sut, 0);
            }

        }
    }
}