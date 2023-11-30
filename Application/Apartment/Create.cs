using Persistence;

using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace Application.Apartment
{
    public class Create : IRequest<Result<ApartmentDTO>>
    {
        public ApartmentDTO apartment { get; set; }
    }
    public class CommandValidator : AbstractValidator<Create>
        {
            public CommandValidator()
            {
                RuleFor(x => x.apartment).SetValidator(new ApartmentValidator());
            }
        }
    public class Handler : IRequestHandler<Create, Result<ApartmentDTO>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<Result<ApartmentDTO>> Handle(Create request, CancellationToken cancellationToken)
        {
            var existApartment = await _context.Apartments.Where(apartment => apartment.Name == request.apartment.Name).ToListAsync();
            if (existApartment.Any())
            {
                return Result<ApartmentDTO>.Failure("Ya existe el departamento");
            }
            Domain.Apartment apartment= new Domain.Apartment();
            _mapper.Map(request.apartment, apartment);
            apartment.Id =Guid.NewGuid();


            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();
            var result = new ApartmentDTO();
            _mapper.Map(apartment, result);
            return Result<ApartmentDTO>.Success(result);

        }
    }

}