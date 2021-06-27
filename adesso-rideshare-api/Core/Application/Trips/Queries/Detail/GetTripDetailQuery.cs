using Core.Application.Trips.Models.Views;
using FluentValidation;
using MediatR;

namespace Core.Application.Trips.Queries.Detail
{
    public class GetTripDetailQuery : IRequest<TripDetail>
    {
        public int Id { get; set; }
    }
}