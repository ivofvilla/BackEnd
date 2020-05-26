using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web
{
    public class Catalogo : ICatalogo
    {
        public List<Livro> GetLivros()
        {
            var livros = new List<Livro>();
            livros.Add(new Livro("001", "Vampiro a mascara", 45.00m));
            livros.Add(new Livro("002", "Lobisomen o Apocalipse", 50.00m));
            livros.Add(new Livro("003", "Mago a ", 55.00m));

            return livros;
        }
    }
}
