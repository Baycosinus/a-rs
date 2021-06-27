using System.Threading.Tasks;
using Core.Application.Trips.Commands.Book;
using Core.Application.Trips.Commands.Create;
using Core.Application.Trips.Commands.Delist;
using Core.Application.Trips.Commands.Edit;
using Core.Application.Trips.Commands.Enlist;
using Core.Application.Trips.Models.Views;
using Core.Application.Trips.Queries.Detail;
using Core.Application.Trips.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.Services.Controllers
{
    [Route("trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        readonly IMediator _mediator;

        public TripsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<TripList> TripList(GetTripListQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("{Id}")]
        public async Task<TripDetail> GetTripDetail(GetTripDetailQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> CreateTrip(CreateTripCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("{Id}/bookings")]
        public async Task<int> BookTrip(BookTripCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{Id}")]
        public async Task<int> EditTrip(EditTripCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{Id}/enlist")]
        public async Task<int> EnlistTrip(EnlistTripCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{Id}/delist")]
        public async Task<int> DelistTrip(DelistTripCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}