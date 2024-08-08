namespace PelatihanHari2Api.Utils;

public class MessageResponse
{
    public string Message { get; set; }
    public bool Status { get; set; }
    public object Data { get; set; }
    
    public MessageResponse(string message, bool status, object data)
    {
        Message = message;
        Status = status;
        Data = data;
    }
    
    public MessageResponse(string message, bool status)
    {
        Message = message;
        Status = status;
    }
    
    public MessageResponse(string message)
    {
        Message = message;
    }
    
    public MessageResponse()
    {
    }
}