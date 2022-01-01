using Microsoft.AspNetCore.SignalR;

namespace Meetup.SinalR.Server
{
    public class ChatHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            var usuario = Context.GetHttpContext().Request.Query["usuario"];
            
            await Groups.AddToGroupAsync(Context.ConnectionId, usuario);
            await Groups.AddToGroupAsync(Context.ConnectionId, "Todos");

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var usuario = Context.GetHttpContext().Request.Query["usuario"];
            
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, usuario);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Todos");
        }

        public async Task EnviarMensagem(string remetente, string destino, string mensagem)
        {
            var grupoHub = Clients.Group(destino);

            if (grupoHub == null)
                return;

            await grupoHub.SendAsync("ReceberMensagem", remetente, destino, mensagem);
        }
    }
}
