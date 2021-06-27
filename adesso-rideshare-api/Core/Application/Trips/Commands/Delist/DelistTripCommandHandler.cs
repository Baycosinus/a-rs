using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Trips.Commands.Delist;
using Core.Common.Enums;
using Core.Domain.Entities;
using Core.Infrastructure.Persistance.SQLDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Trips.Commands.Delist
{
    public class DelistTripCommandHandler : IRequestHandler<DelistTripCommand, int>
    {
        private readonly SQLDbCommandContext _db;

        public DelistTripCommandHandler(SQLDbCommandContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(DelistTripCommand request, CancellationToken cancellationToken)
        {
            var apiUser = new User(); // TODO;

            var trip = await _db.Trips.FirstOrDefaultAsync(trip => trip.Id == request.Id && trip.Status == EntityStatus.Active);

            if (trip.CurrentPassengerCount > default(Int32))
            {
                //TODO Throw Exception
            }

            trip.Status = EntityStatus.Passive;

            await _db.SaveChangesAsync();

            return trip.Id;
        }
    }
}