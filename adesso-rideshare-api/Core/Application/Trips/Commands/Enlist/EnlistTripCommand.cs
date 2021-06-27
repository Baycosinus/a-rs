using FluentValidation;
using MediatR;

namespace Core.Application.Trips.Commands.Enlist
{
    public class Validator : AbstractValidator<EnlistTripCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }

    public class EnlistTripCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}