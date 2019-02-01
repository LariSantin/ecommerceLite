using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;



namespace ecommerceLite.Models
{
    [DataContract]
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

        public ItemCesta(Cesta pedido, Produto produto, int quantidade, decimal precoUnitario)
        {
            Pedido = pedido;
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
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
        [Required]
        [DataMember]
        public decimal PrecoUnitario { get; private set; }
        [DataMember]
        public decimal Subtotal => Quantidade * PrecoUnitario;

        [Required]
        [DataMember]
        public decimal PrecoVenda { get; private set; }

        [DataMember]
        public decimal NovoSubtotal => Quantidade * PrecoVenda;

        internal void AtualizaPrecoVEnda(decimal novopreco)
        {
            PrecoVenda = novopreco;
        }
        internal void AtualizaQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }
    }

    [DataContract]
    public class Cesta : BaseModel
    {
        public Cesta()
        {          
           
        }

        public List<ItemCesta> Itens { get; private set; } = new List<ItemCesta>();

    }

    [DataContract]
    public class adm : BaseModel
    {
        public adm(double despesasTotais, double margemLucro)
        {
            DespesasTotais = despesasTotais;
            MargemLucro = margemLucro;
        }
        [Required]
        [DataMember]
        public double DespesasTotais { get; private set; }

        [Required]
        [DataMember]
        public double MargemLucro { get; private set; }

        internal void AtualizaDespesa(double despesa)
        {
            DespesasTotais = despesa;
        }

        internal void AtualizaMargem(double margem)
        {
            MargemLucro = margem;
        }
    }
} 
