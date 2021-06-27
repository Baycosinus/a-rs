using System;
using FluentValidation;
using MediatR;

namespace Core.Application.Trips.Commands.Book
{
    public class Validator : AbstractValidator<BookTripCommand>
    {
        public Validator()
        {
            RuleFor(c => c.TripId)
                .NotEmpty();
        }
    }

    public class BookTripCommand : IRequest<int>
    {
        public int TripId { get; set; }
    }
}