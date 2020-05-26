using Demo_Asserts;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class JuntarNomesTestes
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RetornaNomeCompleto()
        {
            var sut = new JuntarNomes().Juntar("Ivo", "Villa");
            
            Assert.That(sut, Is.EqualTo("Ivo Villa"));
        }

        [Test]
        public void RetornaNomeCompletoCaseSensitive()
        {
            var sut = new JuntarNomes().Juntar("ivo", "villa");

            Assert.That(sut, Is.EqualTo("IVO VILLA").IgnoreCase);
        }
        
        [Test]
        public void RetornaNomeCompletoCaseDiferente()
        {
            var sut = new JuntarNomes().Juntar("ivo", "villa");

            Assert.That(sut, Is.Not.EqualTo("IVO VILA").IgnoreCase);
        }
    }
}