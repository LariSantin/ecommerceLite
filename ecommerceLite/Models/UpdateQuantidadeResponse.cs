using ecommerceLite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerceLite.Models
{
    public class UpdateQuantidadeResponse
    {
        public UpdateQuantidadeResponse(ItemCesta itemCesta, CarrinhoViewModel carrinhoViewModel)
        {
            ItemCesta = itemCesta;
            CarrinhoViewModel = carrinhoViewModel;
        }

        public ItemCesta ItemCesta { get; }
        public CarrinhoViewModel CarrinhoViewModel { get; }

    }
}
