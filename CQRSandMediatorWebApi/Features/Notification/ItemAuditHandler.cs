using MediatR;

namespace CQRSandMediatorWebApi.Features.Notification
{
    public class ItemAuditInitiatorHandler : INotificationHandler<ItemAudit>
    {
        private readonly ILogger<ItemAuditInitiatorHandler> _logger;
        public ItemAuditInitiatorHandler(ILogger<ItemAuditInitiatorHandler> logger) { 
          _logger = logger;
        }
        public Task Handle(ItemAudit itemAudit, CancellationToken cancellationToken)
        {
            _logger.LogInformation(itemAudit.Message);
            return Task.CompletedTask;
        }
    }

    public class ItemAuditCompleteHandler : INotificationHandler<ItemAudit>
    {
        private readonly ILogger<ItemAuditCompleteHandler> _logger;
        public ItemAuditCompleteHandler(ILogger<ItemAuditCompleteHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(ItemAudit itemAudit, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Completed : "+itemAudit.Message);
            return Task.CompletedTask;
        }
    }
}
