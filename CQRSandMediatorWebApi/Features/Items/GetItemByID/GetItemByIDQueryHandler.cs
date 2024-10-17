using CQRSandMediatorWebApi.Data;
using CQRSandMediatorWebApi.Models;
using MediatR;

namespace CQRSandMediatorWebApi.Features.Items.GetItemByID
{
    public class GetItemByIDQueryHandler : IRequestHandler<GetItemByIDQuery, Item>
    {
        private readonly AppDbContext _context;

        public GetItemByIDQueryHandler(AppDbContext context) { 
            _context = context;
        }
        public async Task<Item?> Handle(GetItemByIDQuery request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(request.Id);
            return item;
        }
    }

}
