using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Apartment
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ApartmentDTO Apartment { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Apartment).SetValidator(new ApartmentValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var aparment = await _context.Apartments.FindAsync(request.Apartment.Id);
                if (aparment == null) return null;

                _mapper.Map(request.Apartment, aparment);

                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Error al actualizar departamento");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}