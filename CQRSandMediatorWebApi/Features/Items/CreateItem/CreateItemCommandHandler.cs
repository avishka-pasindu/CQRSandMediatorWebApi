using CQRSandMediatorWebApi.Data;
using CQRSandMediatorWebApi.Models;
using MediatR;

namespace CQRSandMediatorWebApi.Features.Items.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Guid>
    {
        private readonly AppDbContext _context;

        public CreateItemCommandHandler(AppDbContext context) { 
            _context = context;
        }
        public async Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Item { Name = request.Name, Description = request.Description };
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }
    }
}
