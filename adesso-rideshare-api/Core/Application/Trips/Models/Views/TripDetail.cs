using System;
using Core.Domain.Entities;

namespace Core.Application.Trips.Models.Views
{
    public class TripDetail
    {
        public int Id { get; set; }
        public UserDetail Host { get; set; }
        public DateTime StartDate { get; set; }
        public CityDetail DepartureCity { get; set; }
        public CityDetail DestinationCity { get; set; }
        public string Description { get; set; }
        public int MaximumPassengerCount { get; set; }
        public int CurrentPassengerCount { get; set; }
    }
}