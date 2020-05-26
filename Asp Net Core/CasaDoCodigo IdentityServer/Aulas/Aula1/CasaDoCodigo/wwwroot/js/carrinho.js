class Carrinho {

    getData(btn) {

        let linha = $(btn).parents('[item-id]');
        let itemId = $(linha).attr('item-id');
        let qtd = $(linha).find('input').val();

        let data = {
            Id: itemId,
            Quantidade: qtd
        };

        return data;
    }

    postQuantidade(data) {

        let headers = {};
        let token = $("[name=__RequestVerificationToken]").val();

        headers['RequestVerificationToken'] = token;

        $.ajax({
            url: "/pedido/UpdateQuantidade",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            headers: headers
        }).done(function (response) {

            let itemPedido = response.itemPedido;
            let linhaItem = $("[item-id=" + itemPedido.id + "]");
            let carrinho = response.carrinho;

            linhaItem.find('input').val(itemPedido.quantidade);
            linhaItem.find('[subtotal]').html((itemPedido.quantidade * itemPedido.precoUnitario).duasCasas());

            if (itemPedido.quantidade === 0)
            {
                linhaItem.remove();
            }

            $('[numero-itens]').html("Total: " + carrinho.itens.length + " itens");
            $('[total]').html("Total: " + carrinho.total.duasCasas());

        });

    }

    updateQuantidade(btn) {

        let data = this.getData(btn);
        this.postQuantidade(data);
    }

    clickAdicionar(btn) {

        let data = this.getData(btn);
        data.Quantidade++;
        this.postQuantidade(data);
    }

    clickDecremento(btn) {

        let data = this.getData(btn);
        data.Quantidade--;
        this.postQuantidade(data);

    }
}

var carrinho = new Carrinho();

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.',',');
}