using MediatR;
using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Buildings
{
    public class List
    {
        public class Query : IRequest<List<Building>>{}

        public class Handler : IRequestHandler<Query, List<Building>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Building>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Building.ToListAsync();
            }
        }
    }
}