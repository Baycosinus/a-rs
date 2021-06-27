using System;
using FluentValidation;
using MediatR;

namespace Core.Application.Trips.Commands.Create
{
    public class Validator : AbstractValidator<CreateTripCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Description)
                .MaximumLength(3000);
            RuleFor(c => c.StartDate)
                .NotEmpty();
            RuleFor(c => c.DepartureCityId)
                .NotEmpty();
            RuleFor(c => c.DestinationCityId)
                .NotEmpty();
        }
    }

    public class CreateTripCommand : IRequest<int>
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int MaximumPassengerCount { get; set; }
        public int DepartureCityId { get; set; }
        public int DestinationCityId { get; set; }


        public void Validate()
        {
            // TODO
        }
    }
}