using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Groups
{
    public class List
    {
        public class Query : IRequest<Result<List<GroupDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<GroupDTO>>>
        {
            private readonly DataContext _context;
            public readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<GroupDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var groups = await _context.Groups
                .ProjectTo<GroupDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
                //.Include(a => a.Categories).ToListAsync();

               // var groupMapper = _mapper.Map<List<GroupDTO>>(groups);

                return Result<List<GroupDTO>>.Success(groups);
            }
        }
    }
}