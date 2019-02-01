using ecommerceLite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerceLite.Repository
{
    public interface IProdutoRepository
    {
        void SaveProdutos(List<Produto> produtos);
        IList<Produto> GetProdutos();

    }

    public class ProdutoRepository :BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet.Where(p=> p.PrecoDeFabrica != 0).ToList();
        }

        public void SaveProdutos(List<Produto> produtos)
        {
            foreach (var produto in produtos)
            {
                if (!dbSet.Where(p => p.Codigo == produto.Codigo).Any())
                {                   
                    dbSet.Add(new Produto(produto.Codigo, produto.Nome, produto.PrecoDeFabrica, produto.Descricao));
                }
                
            }
            contexto.SaveChanges();
        }

    }
}
