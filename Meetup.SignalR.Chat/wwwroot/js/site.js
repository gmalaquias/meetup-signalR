

$(function () {

    var url = "https://localhost:7024/chat";
    var conexao =

    $('.iniciar').click(function () {
        let usuario = $('.name');

        if (usuario.val() == "") {
            alert("Digite o usuario");
            return;
        }

        conexao = new signalR.HubConnectionBuilder()
            .withUrl(`${url}?usuario=` + usuario.val())
            .build();

        conexao.start().then(() => {
            $('#usuario').hide();
            $('.mensagem-box').show();
            $('#nomeUsuario').html("Olá, " + usuario.val())
        });

        conexao.on("ReceberMensagem", (remetente, destino, mensagem) => {
            $('#mensagens').append(`<p>${remetente} enviou para ${destino}: <strong>${mensagem}</strong><p>`);
        });
    });


    $('#enviar').click(function () {

        let destino = $('#destino');
        let mensagem = $('#mensagem');

        if (destino == "" || mensagem == "") {
            alert("erro");
            return;
        }

        conexao.send("EnviarMensagem", $('.name').val(), destino.val(), mensagem.val());

        $('#mensagem').val("");

    });


});