using MediatR;

namespace CQRSandMediatorWebApi.Features.Items.CreateItem
{
    public record CreateItemCommand(string Name, string Description) : IRequest<Guid>;
   
}
