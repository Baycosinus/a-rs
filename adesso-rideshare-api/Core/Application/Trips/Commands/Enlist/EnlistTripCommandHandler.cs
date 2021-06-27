using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Trips.Commands.Enlist;
using Core.Common.Enums;
using Core.Domain.Entities;
using Core.Infrastructure.Persistance.SQLDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Trips.Commands.Enlist
{
    public class EnlistTripCommandHandler : IRequestHandler<EnlistTripCommand, int>
    {
        private readonly SQLDbCommandContext _db;

        public EnlistTripCommandHandler(SQLDbCommandContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(EnlistTripCommand request, CancellationToken cancellationToken)
        {
            var apiUser = new User(); // TODO;

            var trip = await _db.Trips.FirstOrDefaultAsync(trip => trip.Id == request.Id && trip.Status == EntityStatus.Passive);

            if (trip.CurrentPassengerCount > default(Int32))
            {
                //TODO Throw Exception
            }

            trip.Status = EntityStatus.Active;
            
            await _db.SaveChangesAsync();

            return trip.Id;
        }
    }
}