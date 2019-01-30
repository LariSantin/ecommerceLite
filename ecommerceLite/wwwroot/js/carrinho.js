﻿class Carrinho {
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
        var itemId = $(linhaDoItem).attr('itemId');
        var novaQtde = $(linhaDoItem).find('input').val();

        return {
            Id: itemId,
            Quantidade: novaQtde
        }
    }

    postQuantidade(data) {
        $.ajax({
            url: '/cesta/updatequantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)

        }).done(function (response) {
            let itemPedido = response.itemPedido;
            let linhaDoItem = $('[item-id=' + itemPedido.Id + ']')
            linhaDoItem.find('input').val(itemPedido.Quantidade);

            linhaDoItem.find('[subtotal]').html(itemPedido.Total);
        });
    }
}

var carrinho = new Carrinho();

Number.prototype.duascasas = function () {
    return this.toFixed(2).replace('.', ',');
}