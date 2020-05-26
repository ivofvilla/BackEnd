using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LivrosRpg.Dal.Context
{
    public class BancoContexto : DbContext
    {
        public BancoContexto() : base("connDB") { }

    }
}