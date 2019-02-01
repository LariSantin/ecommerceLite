using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerceLite.Models.ViewModels
{
    public class CarrinhoViewModel
    {
        public CarrinhoViewModel(IList<ItemCesta> itens)
        {
            Itens = itens;
        }

        public IList<ItemCesta> Itens { get; }

        public decimal Total => Itens.Sum(i => i.Quantidade * i.Produto.PrecoDeFabrica);

        public decimal NovoTotal => Itens.Sum(i => i.Quantidade * i.PrecoVenda);


    }
}
