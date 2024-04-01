using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight? GetFullFlightById(int id)
        {
            return _context.Flights
                .Include(flight => flight.From)
                .Include(flight => flight.To)
                .SingleOrDefault(flight => flight.Id == id);
        }

        public bool IsDuplicate(Flight flight)
        {
            return _context.Flights.Any(f =>
                f.DepartureTime == flight.DepartureTime &&
                f.ArrivalTime == flight.ArrivalTime &&
                f.To.AirportCode == flight.To.AirportCode &&
                f.To.City == flight.To.City &&
                f.To.Country  == flight.To.Country &&
                f.From.AirportCode == flight.From.AirportCode &&
                f.From.City == flight.From.City &&
                f.From.Country == flight.From.Country);
        }

        public bool IsFlightValid(Flight flight)
        {
            if (string.Compare(flight.ArrivalTime, flight.DepartureTime) <= 0)
            {
                return false;
            }
            if (flight.From.AirportCode == flight.To.AirportCode.Trim().ToUpper())
            {
                return false;
            }
            if (string.IsNullOrEmpty(flight.Carrier) ||
                string.IsNullOrEmpty(flight.From.AirportCode) ||
                string.IsNullOrEmpty(flight.From.City) ||
                string.IsNullOrEmpty(flight.From.Country) ||
                string.IsNullOrEmpty(flight.To.AirportCode) ||
                string.IsNullOrEmpty(flight.To.City) ||
                string.IsNullOrEmpty(flight.To.Country) ||
                string.IsNullOrEmpty(flight.ArrivalTime) ||
                string.IsNullOrEmpty(flight.DepartureTime))
            {
                return false;
            }

            return true;
        }

        public PageResult<Flight> SearchForFlight(SearchFlights request)
        {
            var flights = _context.Flights
                .Where(f => f.From.AirportCode == request.From &&
                            f.To.AirportCode == request.To)
                .ToList();

            return new PageResult<Flight>
            {
                Page = 0,
                TotalItems = flights.Count,
                Items = flights
            };
        }
        
        public void Clear()
        {
            _context.Flights.RemoveRange();
        }
    }
}
