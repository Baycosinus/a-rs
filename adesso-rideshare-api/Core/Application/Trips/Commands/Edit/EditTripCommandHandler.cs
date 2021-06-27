using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Trips.Commands.Edit;
using Core.Domain.Entities;
using Core.Infrastructure.Persistance.SQLDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Trips.Commands.Edit
{
    public class EditTripCommandHandler : IRequestHandler<EditTripCommand, int>
    {
        private readonly SQLDbCommandContext _db;

        public EditTripCommandHandler(SQLDbCommandContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(EditTripCommand request, CancellationToken cancellationToken)
        {
            request.Validate();

            var apiUser = new User(); // TODO;

            var trip = await _db.Trips.FirstOrDefaultAsync(trip => trip.Id == request.Id);

            if(trip.CurrentPassengerCount > default(Int32))
            {
                //TODO Throw Exception
            }

            trip.DestinationCityId = request.DestinationCityId;
            trip.DepartureCityId = request.DepartureCityId;
            trip.Description = request.Description;
            trip.MaximumPassengerCount = request.MaximumPassengerCount;
            trip.StartDate = request.StartDate;

            await _db.SaveChangesAsync();

            return trip.Id;
        }
    }
}