

using ecommerceLite.Models;
using ecommerceLite.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ecommerceLite.Repository
{
    public interface ICestaRepository
    {
        Cesta GetPedido();
        void AddItem(string codigo);
        UpdateQuantidadeResponse UpdateQuantidade(ItemCesta itemCesta);
        void AddDespesaLucro(adm despesaLucro);
        void AddPrecoVenda(int codigo);
        int? getPedidoIdAux();


    }
    public class CestaRepository : BaseRepository<Cesta>, ICestaRepository
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IItemCestaRepository itemCestaRepository;
      

        public CestaRepository(ApplicationContext contexto, IHttpContextAccessor contextAccessor,
            IItemCestaRepository itemCestaRepository) : base(contexto)
        {
            this.contextAccessor = contextAccessor;
            this.itemCestaRepository = itemCestaRepository;
           
        }

        public void AddItem(string codigo)
        {
            var produto = contexto.Set<Produto>()
                        .Where(p => p.Codigo == codigo)
                        .SingleOrDefault();

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }
            var pedido = GetPedido();
            var itemPedido = contexto.Set<ItemCesta>()
                                .Where(i => i.Produto.Codigo == codigo
                                    && i.Pedido.Id == pedido.Id)
                                .SingleOrDefault();

            if (itemPedido == null)
            {
                itemPedido = new ItemCesta(pedido, produto, 1, produto.PrecoDeFabrica);
                contexto.Set<ItemCesta>()
                                .Add(itemPedido);               
                contexto.SaveChanges();

                AddPrecoVenda(itemPedido.Id);

            }
        }

        public Cesta GetPedido()
        {
            var pedidoId = GetPedidoId();

            var pedido = dbSet
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .Where(p => p.Id == pedidoId)
                .SingleOrDefault();
            if (pedido == null)
            {                             
                pedido = new Cesta();
                contexto.Set<Cesta>()
                                .Add(pedido);
                contexto.SaveChanges();
                SetPedidoId(pedido.Id);
                
            }
            return pedido;
         
        }
        public void removeAdm()
        {
            var itensADM = contexto.Set<adm>().ToList();

            foreach (var item in itensADM)
            {
                contexto.Remove(item);
            }
        }

        public void AddDespesaLucro(adm despesaLucro)
        {
            removeAdm();
            if(despesaLucro == null)
            {
                despesaLucro.AtualizaDespesa(400.00);
                despesaLucro.AtualizaMargem(0.0);
                contexto.SaveChanges();
            }
            if (despesaLucro.DespesasTotais != 0 && despesaLucro.MargemLucro != 0)
            {
                var cesta = new adm(despesaLucro.DespesasTotais, despesaLucro.MargemLucro);
                contexto.Set<adm>()
                                    .Add(cesta);
                contexto.SaveChanges();
            }
                       
          
        }     

        public void AddPrecoVenda(int codigo)
        {
            var pedido = GetPedidoId();
            var admExiste = contexto.Set<adm>().Where(p => p.Id != null).SingleOrDefault();

            var cestaExiste = contexto.Set<Cesta>()
                               .Where(i => i.Id == pedido)
                               .SingleOrDefault();
            var itemcestaExiste = contexto.Set<ItemCesta>()
                               .Where(i => i.Id == codigo)
                               .SingleOrDefault();

            var quantidadeItens = contexto.Set<Produto>().Count();

            if (cestaExiste != null && itemcestaExiste != null)
            {
                decimal rateio;
                decimal novoPreco;

                if (admExiste == null)
                {
                    rateio = Convert.ToDecimal(400.00) / quantidadeItens;
                }
                else
                {
                    rateio = Convert.ToDecimal(admExiste.DespesasTotais) / quantidadeItens;
                }
                
                decimal preco = rateio + itemcestaExiste.PrecoUnitario;
                if (admExiste == null)
                {
                    novoPreco = preco;
                }
                else
                {
                    novoPreco = preco * Convert.ToDecimal((1 + admExiste.MargemLucro / 100));
                }
               
                var precoFormat = novoPreco.ToString("N2");

                itemcestaExiste.AtualizaPrecoVEnda(Convert.ToDecimal(precoFormat));
                contexto.SaveChanges();
            }
        }

        public UpdateQuantidadeResponse UpdateQuantidade(ItemCesta itemCesta)
        {
         
            Console.WriteLine("-----------------------------------------");
            var itemCestaDb = itemCestaRepository.GetItemCesta(itemCesta.Id);
           
            if (itemCestaDb != null)
            {
                itemCestaDb.AtualizaQuantidade(itemCesta.Quantidade);
                if (itemCesta.Quantidade == 0)
                {
                    itemCestaRepository.RemoveItemPedido(itemCesta.Id);
                }
                contexto.SaveChanges();
                var carrinhoViewModel = new CarrinhoViewModel(GetPedido().Itens);

                return new UpdateQuantidadeResponse(itemCestaDb, carrinhoViewModel ); 
            }
            throw new ArgumentException("Item não encontrado");
        }
        public int? getPedidoIdAux()
        {
           return GetPedidoId();
        }

        private int? GetPedidoId()
        {
            return contextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }

        private void SetPedidoId(int pedidoId)
        {
            contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }

       
    }
}

