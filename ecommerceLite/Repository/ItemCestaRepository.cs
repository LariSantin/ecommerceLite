using ecommerceLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerceLite.Repository
{
    public interface IItemCestaRepository
    {
        ItemCesta GetItemCesta(int itemPedidoId);
        void RemoveItemPedido(int itemPedidoId);
        IList<ItemCesta> GetItens(decimal id);

    }
    public class ItemCestaRepository : BaseRepository<ItemCesta>, IItemCestaRepository
    {
      
        public ItemCestaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public IList<ItemCesta> GetItens(decimal id)
        {

            // var pedido = GetPedidoId();

            return dbSet.Where(p => p.Id == id).ToList();
        }

        public ItemCesta GetItemCesta(int itemPedidoId)
        {

            return dbSet
                .Where(ic => ic.Id == itemPedidoId)
                .SingleOrDefault();
        }

        public void RemoveItemPedido(int itemPedidoId)
        {
            dbSet.Remove(GetItemCesta(itemPedidoId));
        }

        
    }
}
