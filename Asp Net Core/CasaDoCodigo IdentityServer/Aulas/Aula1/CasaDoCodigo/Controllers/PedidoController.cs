using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModel;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IItemPedidoRepository itempedidoRepository;

        public PedidoController(IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository, IItemPedidoRepository itempedidoRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itempedidoRepository = itempedidoRepository;
        }

        public IActionResult Cadastro()
        {
            var pedido = pedidoRepository.GetPedido();

            if(pedido == null)
            {
                return RedirectToAction("Carrossel");
            }

            return View(pedido.Cadastro);
        }

        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrWhiteSpace(codigo))
            {
                pedidoRepository.AddItem(codigo);
            }

            var itens = pedidoRepository.GetPedido().Itens;
            var carrinho = new CarrinhoViewModel(itens);

            return View(carrinho);
        }

        public IActionResult Carrossel()
        {
            var produtos = produtoRepository.GetProdutos();
            return View(produtos);
        }

        [HttpPost]
        public IActionResult Resumo(Cadastro cadastro)
        {
            if(ModelState.IsValid)
            {
                var cadastroResumo = pedidoRepository.UpdateCadastro(cadastro);
                return View(cadastroResumo);
            }

            return RedirectToAction("Cadastro");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //validação para nao ter ação externa
        public UpdateQuantidadeResponse UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
            return pedidoRepository.UpdateQuantidade(itemPedido);
        }
    }
}