using ecommerceLite.Models;
using ecommerceLite.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerceLite
{
    public class DataService : IDataService
    {
        
        private readonly ApplicationContext contexto;
        private readonly IProdutoRepository produtoRepository;

        public DataService(ApplicationContext contexto, IProdutoRepository produtoRepository)
        {
            this.contexto = contexto;
            this.produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            contexto.Database.EnsureCreated();
            List<Produto> produtos = GetProdutos();

            produtoRepository.SaveProdutos(produtos);
        }

       

        private static List<Produto> GetProdutos()
        {
            var json = File.ReadAllText("produtos.json");
            var produtos = JsonConvert.DeserializeObject<List<Produto>>(json);
            return produtos;
        }
    }
}
