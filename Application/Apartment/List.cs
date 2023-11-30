using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace Application.Apartment
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<ApartmentDTO>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ApartmentDTO>>>
        {
            private readonly IMapper _mapper;
            private readonly DataContext _context;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<PagedList<ApartmentDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query =  _context.Apartments
                .ProjectTo<ApartmentDTO>(_mapper.ConfigurationProvider)
                .AsQueryable();

                return Result<PagedList<ApartmentDTO>>.Success(
                    await PagedList<ApartmentDTO>.CreateAsync(query ,request.Params.PageNumber,
                        request.Params.PageSize)
                );
            }
        }
    }
}