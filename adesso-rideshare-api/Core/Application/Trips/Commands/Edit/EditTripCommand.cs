using System;
using Core.Application.Trips.Models.Views;
using FluentValidation;
using MediatR;

namespace Core.Application.Trips.Commands.Edit
{
    public class Validator : AbstractValidator<EditTripCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
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

    public class EditTripCommand : IRequest<int>
    {
        public int Id { get; set; }
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