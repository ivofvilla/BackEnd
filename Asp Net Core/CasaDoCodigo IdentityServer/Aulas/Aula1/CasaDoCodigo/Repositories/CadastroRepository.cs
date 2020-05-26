using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public partial class CadastroRepository : BaseRepository<Cadastro>, ICadastroRepository
    {
        public CadastroRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public Cadastro Update(int cadastroId, Cadastro novoCadastro)
        {
            var cadastroDb = dbSet.Where(w => w.Id == cadastroId).SingleOrDefault();

            if(cadastroDb == null)
            {
                throw new ArgumentNullException("Cadastro não encontrado");
            }

            cadastroDb.Update(novoCadastro);
            contexto.SaveChanges();

            return cadastroDb;
        }
    }
}
