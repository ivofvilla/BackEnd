using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alura.LeilaoOnline.Core;

namespace LeilaoOnline.Core
{
    public class MaiorValor : IModalidadeAvaliacao
    {
        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                            .DefaultIfEmpty(new Lance(null, 0))
                            .OrderBy(o => o.Valor)
                            .LastOrDefault();
        }
    }
}
