using System.Threading;
using System.Threading.Tasks;
using Core.Application.Trips.Commands.Create;
using Core.Application.Trips.Models.Views;
using Core.Common.Enums;
using Core.Domain.Entities;
using Core.Infrastructure.Persistance.SQLDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Trips.Commands.Create
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, int>
    {
        private readonly SQLDbCommandContext _db;

        public CreateTripCommandHandler(SQLDbCommandContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            request.Validate();

            var apiUser = new User(); // TODO: Implement IdentityService;


            var trip = new Trip
            {
                Host = apiUser,
                DepartureCityId = request.DepartureCityId,
                DestinationCityId = request.DestinationCityId,
                Description = request.Description.Trim(),
                StartDate = request.StartDate,
                Status = EntityStatus.Active
            };

            await _db.Trips.AddAsync(trip);
            await _db.SaveChangesAsync();

            return trip.Id;
        }
    }
}