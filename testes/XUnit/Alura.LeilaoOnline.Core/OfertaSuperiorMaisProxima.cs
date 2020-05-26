using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alura.LeilaoOnline.Core;

namespace LeilaoOnline.Core
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
        public int ValorDestino { get;  }

        public OfertaSuperiorMaisProxima(int valorDestino)
        {
            ValorDestino = valorDestino;
        }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                            .DefaultIfEmpty(new Lance(null, 0))
                            .Where(w => w.Valor > ValorDestino)
                            .OrderBy(o => o.Valor)
                            .FirstOrDefault();
        }
    }
}
