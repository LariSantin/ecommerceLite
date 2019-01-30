using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;



namespace ecommerceLite.Models
{

    public class BaseModel
    {
        [DataMember]
        public int Id { get; protected set; }
    }
  
    public class Produto : BaseModel
    {
        public Produto(string codigo, string nome, decimal precoDeFabrica, string descricao)
        {
            Codigo = codigo;
            Nome = nome;
            PrecoDeFabrica = precoDeFabrica;
            Descricao = descricao;
        }

        [Required]
        public string Codigo { get; private set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Nome deve ter no máximo 50 caracteres")]
        public string Nome { get; private set; }

        [Required]
        public decimal PrecoDeFabrica { get; private set; }

        [MaxLength(200, ErrorMessage = "Descrição deve ter no máximo 50 caracteres")]
        public string Descricao { get; private set; }

    }

    [DataContract]
    public class ItemCesta : BaseModel
    {
        public ItemCesta()
        {
        }

        public ItemCesta(Cesta pedido, Produto produto, int quantidade)
        {
            Pedido = pedido;
            Produto = produto;
            Quantidade = quantidade;
        }

        [Required]
        [DataMember]
        public Cesta Pedido { get; private set; }

        [Required]
        [DataMember]
        public Produto Produto { get; private set; }

        [Required]
        [DataMember]
        public int Quantidade { get; private set; }

        internal void AtualizaQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }
    }

    public class Cesta : BaseModel
    {
        public Cesta()
        {          
           
        }

        public List<ItemCesta> Itens { get; private set; } = new List<ItemCesta>();

        [Required]
        [DataMember]
        public double TotalCompra { get; private set; }

        [Required]
        [DataMember]
        public double DespesasTotais { get; private set; }

        [Required]
        [DataMember]
        public double MargemLucro { get; private set; }


    }
} 
