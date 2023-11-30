using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Groups
{
    public class Details
    {
        public class Query : IRequest<Result<GroupDTO>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<GroupDTO>>
        {
            private readonly DataContext _context;
            public IMapper _mapper { get; }

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<GroupDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var group = await _context.Groups
                .ProjectTo<GroupDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == request.Id);
                //FindAsync(request.Id);
                return Result<GroupDTO>.Success(group);
            }
        }
    }
}