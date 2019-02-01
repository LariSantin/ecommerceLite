class Carrinho {

    PegaDespesaMargem() {

        var despesa = document.getElementById("despesas").value;
        var margem = document.getElementById("margem").value;
        console.log(margem)
        if (despesa.length == 0) {
            despesa = 400.00;
        }
        if (margem.length == 0) {
            margem = 0.0;
        }
        
        var data = {
            DespesasTotais: despesa,
            MargemLucro: margem
        }
        console.log(data);
        
        $.ajax({
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            url: '/cesta/despesas'
        }).done(function (response) {
            console.log(response);
                       
        });       
   
       // $('#Margem').val('Margem' + margem)

      
        window.location.href = "listaitens";
    }

    clickIncremento(btn) {

        let data = this.getData(btn);
        data.Quantidade++;
        this.postQuantidade(data);

    }
    clickDecremento(btn) {
        let data = this.getData(btn);
        data.Quantidade--;
        this.postQuantidade(data)
    }
    updateQuantidade(input) {
        let data = this.getData(input);
        this.postQuantidade(data);
    }

    getData(elemento) {
        var linhaDoItem = $(elemento).parents('[item-id]');
        var itemId = $(linhaDoItem).attr('item-id');
        var quantidade = $(linhaDoItem).find('input').val();

        return {
            Id: itemId,
            Quantidade: quantidade
        }
    }

    postQuantidade(data) {
        console.log(data)
        // console.log(data.Id)
        // console.log(JSON.stringify(data))

        $.ajax({
            url: '/cesta/updatequantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (response) {
            console.log(response);

            let itemPedido = response.itemCesta;
            let linhaDoItem = $('[item-id=' + itemPedido.id + ']')
            linhaDoItem.find('input').val(itemPedido.quantidade);

            linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duascasas());
            linhaDoItem.find('[novosubtotal]').html('Novo subtotal: ' + (itemPedido.novoSubtotal).duascasas());

           
            let carrinhoViewModel = response.carrinhoViewModel

            $('[numero-itens]').html('Total: ' + carrinhoViewModel.itens.length + ' itens');
            $('[total]').html(carrinhoViewModel.total);
            $('[novototal]').html(carrinhoViewModel.novoTotal);

            if (itemPedido.quantidade <= 0) {
                linhaDoItem.remove();
            }

        });
    }
    }

    var carrinho = new Carrinho();

    Number.prototype.duascasas = function () {
        return this.toFixed(2).replace('.', ',');
    }