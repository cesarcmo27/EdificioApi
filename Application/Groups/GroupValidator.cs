using Domain;
using FluentValidation;

namespace Application.Groups
{
    public class GroupValidator : AbstractValidator<Group>
    {
        public GroupValidator()
        {
             RuleFor(x => x.Name).NotEmpty().WithMessage("El grupo no puede estar vacio");
             RuleFor(x => x.Status).NotEmpty().WithMessage("El estado no puede estar vacio")
                                   .InclusiveBetween(1,2).WithMessage("no esta en rango");
             
        }
    }
}