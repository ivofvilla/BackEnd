using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LivrosRpg.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }
        public string CodigoEditora { get; set; }
        public string ISBN { get; set; }
        public bool Impresso { get; set; }
        public int IdSistema { get; set; }
        public int IdGenero { get; set; }
        public int IdVersao { get; set; }
        public byte[] Imagem { get; set; }
    }
}