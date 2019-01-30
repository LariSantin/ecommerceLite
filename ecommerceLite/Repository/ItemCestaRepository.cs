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
    }
    public class ItemCestaRepository : BaseRepository<ItemCesta>, IItemCestaRepository
    {
        public ItemCestaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public ItemCesta GetItemCesta(int itemPedidoId)
        {
            return dbSet
                .Where(ic => ic.Id == itemPedidoId)
                .SingleOrDefault();
        }
    }
}
