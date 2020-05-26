using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeilaoOnline.Core
{
    public interface IModalidadeAvaliacao
    {
        Lance Avalia(Leilao leilao);
    }
}
