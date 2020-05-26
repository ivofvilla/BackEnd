using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IItemPedidoRepository itemPedidoRepository;
        private readonly ICadastroRepository cadastroRepository;

        public PedidoRepository(ApplicationContext contexto, 
                            IHttpContextAccessor contextAccessor,
                            IItemPedidoRepository itemPedidoRepository,
                            ICadastroRepository cadastroRepository) : base(contexto)
        {
            this.contextAccessor = contextAccessor;
            this.itemPedidoRepository = itemPedidoRepository;
            this.cadastroRepository = cadastroRepository;
        }

        private int? GetPedidoId()
        {
            return contextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }

        private void SetPedidoId(int pedidoId)
        {
            contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }

        public Pedido GetPedido()
        {
            var pedidoId = GetPedidoId();
            var pedido = dbSet
                        .Include(i => i.Itens)
                            .ThenInclude(i => i.Produto)
                        .Include(i => i.Cadastro)
                        .Where(w => w.Id == pedidoId).SingleOrDefault();

            if (pedido == null)
            {
                pedido = new Pedido();
                dbSet.Add(pedido);
                contexto.SaveChanges();
                SetPedidoId(pedido.Id);
            }

            return pedido;

        }

        public void AddItem(string codigo)
        {
            var produto = contexto.Set<Produto>()
                                .Where(p => p.Codigo == codigo)
                                .FirstOrDefault();

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            var pedido = GetPedido();

            var itemPedido = contexto.Set<ItemPedido>()
                                    .Where(w => w.Produto.Codigo == codigo && w.Pedido.Id == pedido.Id)
                                    .SingleOrDefault();

            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
                contexto.Set<ItemPedido>().Add(itemPedido);
                contexto.SaveChanges();
            }

        }

        public UpdateQuantidadeResponse UpdateQuantidade(ItemPedido itemPedido)
        {
            var itemPedidoSelecionado = itemPedidoRepository.GetItemPedido(itemPedido.Id);

            if (itemPedidoSelecionado == null)
            {
                throw new ArgumentNullException("Pedido não encontrado!");
            }

            itemPedidoSelecionado.AtualizaQuantidade(itemPedido.Quantidade);

            if (itemPedido.Quantidade == 0)
            {
                itemPedidoRepository.RemoveItemPedido(itemPedido.Id);
            }

            contexto.SaveChanges();

            var carrinho = new CarrinhoViewModel(GetPedido().Itens);

            var retorno = new UpdateQuantidadeResponse(itemPedidoSelecionado, carrinho);

            return retorno;
        }

        public Pedido UpdateCadastro(Cadastro cadastro)
        {
            var pedido = GetPedido();
            cadastroRepository.Update(pedido.Cadastro.Id, cadastro);

            return pedido;
        }
    }
}
