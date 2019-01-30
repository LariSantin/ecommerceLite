

using ecommerceLite.Models;
using ecommerceLite.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ecommerceLite.Repository
{
    public interface ICestaRepository
    {
        Cesta GetPedido();
        void AddItem(string codigo);
        UpdateQuantidadeResponse UpdateQuantidade(ItemCesta itemCesta);
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
                itemPedido = new ItemCesta(pedido, produto, 1);
                contexto.Set<ItemCesta>()
                                .Add(itemPedido);

                contexto.SaveChanges();
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
                dbSet.Add(pedido);
                contexto.SaveChanges();
                SetPedidoId(pedido.Id);
            }

            return pedido;

        }

        public UpdateQuantidadeResponse UpdateQuantidade(ItemCesta itemCesta)
        {
            var itemCestaDb = itemCestaRepository.GetItemCesta(itemCesta.Id);

            if (itemCestaDb != null)
            {
                itemCestaDb.AtualizaQuantidade(itemCesta.Quantidade);
                contexto.SaveChanges();
                var carrinhoViewModel = new CarrinhoViewModel(GetPedido().Itens);

                return new UpdateQuantidadeResponse(itemCestaDb, carrinhoViewModel ); 
            }
            throw new ArgumentException("Item não encontrado");
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

