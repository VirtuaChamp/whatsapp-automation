using WhatsAppAutomation.Models.Messages.Requests;

namespace WhatsAppAutomation.Models.Messages.Responses;

public class MessageResponseModel : BaseModel
{
    public MessageResponseModel(bool success, MessageRequestModel? data, Exception? exception = null)
    {
        Data = data;
        Success = success;
        Exception = exception;
    }

    public bool Success { get; set; }
    public Exception? Exception { get; set; }
    public MessageRequestModel? Data { get; set; }
}