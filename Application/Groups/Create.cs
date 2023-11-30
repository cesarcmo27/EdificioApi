using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Groups
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Group Group { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Group).SetValidator(new GroupValidator());
            }
        }

        public class Handler : IRequestHandler<Command,Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly ILogger<Handler> _logger;

            public Handler(DataContext context, ILogger<Handler> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("creando -----");
                _context.Groups.Add(request.Group);
                var result = await _context.SaveChangesAsync() > 0 ;
                if(!result) return Result<Unit>.Failure("Error al insertar Grupo");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}