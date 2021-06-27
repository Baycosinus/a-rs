using FluentValidation;
using MediatR;

namespace Core.Application.Trips.Commands.Delist
{
    public class Validator : AbstractValidator<DelistTripCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }

    public class DelistTripCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}