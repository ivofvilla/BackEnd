using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Alura.LeilaoOnline.Core;

namespace LeilaoOnline.Testes
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //arranjo
            var valorNegativo = -100;

            //assert
            var msg = Assert.Throws<System.ArgumentException>(
                    //act
                    () => new Lance(null, valorNegativo)
                );

            Assert.Equal(msg.Message, "Valor do lance não pode ser negativo");
        }
    }
}
