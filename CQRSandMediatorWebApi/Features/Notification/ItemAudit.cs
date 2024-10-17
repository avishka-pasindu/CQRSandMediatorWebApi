using MediatR;

namespace CQRSandMediatorWebApi.Features.Notification
{
    public class ItemAudit : INotification
    {
        public string Message { get; set; }
    }
}
