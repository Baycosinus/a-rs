using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Common.Enums;
using Core.Domain.Entities;
using Core.Infrastructure.Persistance.SQLDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Application.Trips.Commands.Book
{
    public class BookTripCommandHandler : IRequestHandler<BookTripCommand, int>
    {
        private readonly SQLDbCommandContext _db;

        public BookTripCommandHandler(SQLDbCommandContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(BookTripCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var apiUser = new User(); // TODO;

            // trip must be available
            // user must not have active booking for this trip
            // user must not be the host of this or any other trip during that time

            var userHasTripAsHost = await _db.Trips.AnyAsync(trip => trip.HostId == apiUser.Id && trip.StartDate == now);
            var userHasTripAsPassenger = await _db.Trips.Include(trip => trip.TripBookings)
                                                        .AnyAsync(trip => trip.Status == EntityStatus.Active &&
                                                                          trip.StartDate == now &&
                                                                          trip.TripBookings.Any(tripBooking => tripBooking.PassengerId == apiUser.Id && 
                                                                                                               tripBooking.Status == EntityStatus.Active));

            if (userHasTripAsHost || userHasTripAsPassenger)
            {
                // TRIP CANNOT BE BOOKED EXCEPTION
            }

            var trip = await _db.Trips.Include(trip => trip.Host)
                                      .FirstOrDefaultAsync(trip => trip.Id == request.TripId && trip.Status == EntityStatus.Active);

            if (trip == null)
            {
                // NULL EXCEPTION
            }
            
            if (trip.StartDate < now)
            {
                // EXPIRED EXCEPTION
            }

            if (trip.CurrentPassengerCount >= trip.MaximumPassengerCount)
            {
                // TRIP IS FULL EXCEPTION
            }

            if (trip.HostId == apiUser.Id)
            {
                // DUDEWTFEXCEPTION
            }

            var tripBooking = new TripBooking
            {
                TripId = trip.Id,
                PassengerId = apiUser.Id,
                Status = EntityStatus.Active
            };

            _db.TripBookings.Add(tripBooking);

            trip.CurrentPassengerCount = _db.TripBookings.Count(tripBooking => tripBooking.TripId == trip.Id && tripBooking.Status == EntityStatus.Active);

            await _db.SaveChangesAsync();

            return tripBooking.Id;
        }
    }
}