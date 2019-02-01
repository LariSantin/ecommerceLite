using ecommerceLite.Models;
using ecommerceLite.Repository;
using ecommerceLite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerceLite.Controllers
{
    public class CestaController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly ICestaRepository cestaRepository;
        private readonly IItemCestaRepository itemCestaRepository;

        public CestaController(IProdutoRepository produtoRepository, ICestaRepository cestaRepository, IItemCestaRepository itemCestaRepository)
        {
            this.produtoRepository = produtoRepository;
            this.cestaRepository = cestaRepository;
            this.itemCestaRepository = itemCestaRepository;
        }

        public ActionResult ListaItens()
        {
            return View(produtoRepository.GetProdutos());
        }
        public ActionResult setDL()
        {
            return View();
        }
        public ActionResult NovoPreco()
        {
            var id = cestaRepository.getPedidoIdAux();

            return View(itemCestaRepository.GetItens(Convert.ToDecimal(id)));
        }
        public ActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                cestaRepository.AddItem(codigo);
            }
            List<ItemCesta> itens = cestaRepository.GetPedido().Itens;
            CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel(itens);
           
            return View(carrinhoViewModel);
        }

        [HttpPost]
        public UpdateQuantidadeResponse UpdateQuantidade([FromBody]ItemCesta itemCesta)
        {
            return cestaRepository.UpdateQuantidade(itemCesta);
        }

        [HttpPost]
        public void despesas([FromBody]adm despesa)
        {
              
            cestaRepository.AddDespesaLucro(despesa);
            // cestaRepository.AddPrecoVenda();
        }
    }
   
}
