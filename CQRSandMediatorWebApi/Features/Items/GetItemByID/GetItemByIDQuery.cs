using CQRSandMediatorWebApi.Models;
using MediatR;

namespace CQRSandMediatorWebApi.Features.Items.GetItemByID
{
    public record GetItemByIDQuery (Guid Id) : IRequest<Item?>; 
   
}
