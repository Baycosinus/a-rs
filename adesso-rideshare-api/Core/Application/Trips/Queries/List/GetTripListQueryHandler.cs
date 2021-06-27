using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Trips.Models.Views;
using Core.Common.Enums;
using Core.Domain.Entities;
using Core.Infrastructure.Persistance.SQLDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Trips.Queries.List
{
    public class GetTripListQueryHandler : IRequestHandler<GetTripListQuery, TripList>
    {
        private readonly SQLDbQueryContext _db;

        public GetTripListQueryHandler(SQLDbQueryContext db)
        {
            _db = db;
        }

        public async Task<TripList> Handle(GetTripListQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            
            var trips = _db.Trips.Include(trip => trip.Host)
                                 .Include(trip => trip.DepartureCity)
                                 .Include(trip => trip.DestinationCity)
                                 .Where(trip => trip.Status == EntityStatus.Active && trip.StartDate > now)
                                 .AsEnumerable()
                                 .Select(trip => new
                                 {
                                     Id = trip.Id,
                                     Description = trip.Description,
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
                                     Routes = GetRoutes(trip.DepartureCity, trip.DestinationCity)
                                 });

            if (request.DepartureCityId != default(Int32) && request.DestinationCityId != default(Int32))
            {
                var departureCity = await _db.Cities.FirstOrDefaultAsync(c => c.Id == request.DepartureCityId);
                var destinationCity = await _db.Cities.FirstOrDefaultAsync(c => c.Id == request.DestinationCityId);

                if (departureCity != null && destinationCity != null)
                {
                    trips = trips.Where(trip => trip.Routes.FindIndex(route => route == (departureCity.X, departureCity.Y)) < trip.Routes.FindIndex(route => route == (destinationCity.X, destinationCity.Y)));
                }
            }

            var tripsCount = trips.Count(trip => trip.StartDate > now);

            var tripList = trips.Take((request.Page - 1) * request.Take)
                                      .Select(trip => new TripList.Trip
                                      {
                                          Id = trip.Id,
                                          Description = trip.Description,
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
                                          }
                                      })
                                      .ToList();

            return new TripList
            {
                CurrentPage = request.Page,
                TotalPageCount = tripsCount / request.Take,
                Trips = tripList
            };
        }

        public static List<(int, int)> GetRoutes(City a, City b)
        {
            var route = new List<(int, int)>();

            var horizontalDistance = b.X - a.X;
            var verticalDistance = b.Y - a.Y;

            var currentDistance = Vector2.Distance(new Vector2(a.X, a.Y), new Vector2(b.X, b.Y));

            while (currentDistance != 0)
            {
                if (horizontalDistance != 0)
                {
                    a.X += horizontalDistance / Math.Abs(horizontalDistance);
                    horizontalDistance = b.X - a.X;

                    route.Add((a.X, a.Y));
                }

                if (verticalDistance != 0)
                {
                    a.Y += verticalDistance / Math.Abs(verticalDistance);
                    verticalDistance = b.Y - a.Y;
                    route.Add((a.X, a.Y));
                }

                currentDistance = Vector2.Distance(new Vector2(a.X, a.Y), new Vector2(b.X, b.Y));
            }

            return route;
        }
    }
}