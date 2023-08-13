
using System.Text;
using System.Net.WebSockets;

namespace ANZ.Sky;

public class StatusClient {
    
    private ClientWebSocket ws = new ClientWebSocket();

    public StatusClient() {        
    }

    public event EventHandler<MessageEventArgs>? StatusChanged;

    public async void Connect(string serverAddress) {
        await ws.ConnectAsync(new Uri(serverAddress), CancellationToken.None);
        this.WaitForMessages();
    }

    public async void Disconnect() {
        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
    }

    private async void WaitForMessages() 
    {
        byte[] buf = new byte[1056];

        try{
            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(buf, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    this.Disconnect();
                }
                else
                {
                    string message = Encoding.ASCII.GetString(buf, 0, result.Count);
                    this.StatusChanged?.Invoke(this, new MessageEventArgs(message));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exiting with error", e);
        }
        finally 
        {
            if (ws.State == WebSocketState.Open)
            {
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);        
            }    
        }
    }
}