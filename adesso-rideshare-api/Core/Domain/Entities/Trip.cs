using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public int HostId { get; set; }
        public int DepartureCityId { get; set; }
        public int DestinationCityId { get; set; }
        public string Description { get; set; }
        public int MaximumPassengerCount { get; set; }
        public int CurrentPassengerCount { get; set; }
        public DateTime StartDate { get; set; }
        public int Status { get; set; }

        public virtual User Host { get; set; }
        public virtual IList<TripBooking> TripBookings { get; set; }
        public virtual City DepartureCity { get; set; }
        public virtual City DestinationCity { get; set; }
    }
}