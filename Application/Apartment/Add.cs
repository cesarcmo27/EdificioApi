using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Persistence;
using Domain;
using AutoMapper;

namespace Application.Apartment
{
    public class Add
    {
        public class Command : IRequest<Result<Unit>>
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IExcelAccessor _excelAccessor;
            private readonly ILogger<Handler> _logger;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IExcelAccessor excelAccessor, ILogger<Handler> logger,IMapper mapper)
            {
                _context = context;
                _excelAccessor = excelAccessor;
                _logger = logger;
                 _mapper = mapper;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("inico carga masiva depas");
                var list = await _excelAccessor.GetDataApartment(request.File);

                if(list.Count == 0){
                    return Result<Unit>.Failure("Error al leer excel");
                }

                foreach (var depa in list)
                {
                    var data = _context.Apartments.Where(emp => emp.Name == depa.Name);
                    if(data.Any()){
                         return Result<Unit>.Failure("Ya existe el departamento =>"+depa.Name);
                    }

                }

                var apartmentData =_mapper.Map<List<Domain.Apartment>>(list);

                
                _context.Apartments.AddRange(apartmentData);

                var result = await _context.SaveChangesAsync() > 0 ;
                if(!result) return Result<Unit>.Failure("Error al insertar Departamentos");

                return Result<Unit>.Success(Unit.Value);

            }
        }
    }
}