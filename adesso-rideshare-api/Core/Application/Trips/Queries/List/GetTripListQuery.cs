using MediatR;
using Core.Application.Trips.Models.Views;

namespace Core.Application.Trips.Queries.List
{
    public class GetTripListQuery : IRequest<TripList>
    {
        public int Page { get; set; } = 1;
        public int Take { get; set; } = 10;
        public int DepartureCityId { get; set; }
        public int DestinationCityId { get; set; }

        public void Validate()
        {
            // TODO
        }
    }
}