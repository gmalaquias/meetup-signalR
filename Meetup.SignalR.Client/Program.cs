// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.SignalR.Client;

string url = "https://localhost:7024/chat";

Console.WriteLine("Digite seu usuario");
var usuario = Console.ReadLine();

var _connection = new HubConnectionBuilder()
              .WithUrl(url + "?usuario=" + usuario)
              .Build();

await _connection.StartAsync();

_connection.On("ReceberMensagem",(string remetente, string destino, string mensagem) =>
{
    Console.WriteLine($"Mensagem recebida de {remetente}: {mensagem}");
});

while (true)
{
    Console.WriteLine("Para qual usuario deseja enviar mensagem");
    var usuarioEnvio = Console.ReadLine();

    Console.WriteLine("Digite a mensagem");
    var mensagem = Console.ReadLine();

    await _connection.SendAsync("EnviarMensagem", usuario, usuarioEnvio, mensagem);
}
