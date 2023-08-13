
namespace ANZ.Sky;

public class MessageEventArgs : EventArgs
{
    public MessageEventArgs(string message)
    {
        this.Message = message;
    }

    public string Message { get; private set; }
}