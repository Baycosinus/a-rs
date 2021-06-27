using System.Threading;
using System.Threading.Tasks;
using Core.Application.Trips.Models.Views;
using Core.Application.Trips.Queries.Detail;
using Core.Infrastructure.Persistance.SQLDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Trips.Queries.Detail
{
    public class GetTripDetailQueryHandler : IRequestHandler<GetTripDetailQuery, TripDetail>
    {
        private readonly SQLDbQueryContext _db;

        public GetTripDetailQueryHandler(SQLDbQueryContext db)
        {
            _db = db;
        }

        public async Task<TripDetail> Handle(GetTripDetailQuery request, CancellationToken cancellationToken)
        {
            var trip = await _db.Trips.Include(trip => trip.Host)
                                      .Include(trip => trip.DepartureCity)
                                      .Include(trip => trip.DestinationCity)
                                      .FirstOrDefaultAsync(trip => trip.Id == request.Id);

            return new TripDetail
            {
                Host = new UserDetail
                {
                    FirstName = trip.Host.FirstName,
                    LastName = trip.Host.LastName
                },
                StartDate = trip.StartDate,
                DepartureCity = new CityDetail
                {
                    Name = trip.DepartureCity.Name
                },
                DestinationCity = new CityDetail
                {
                    Name = trip.DestinationCity.Name
                },
                Description = trip.Description,
                MaximumPassengerCount = trip.MaximumPassengerCount,
                CurrentPassengerCount = trip.CurrentPassengerCount
            };
        }
    }
}