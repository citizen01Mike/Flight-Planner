using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public List<Airport> SearchForAirport(string airport)
        {
            airport = airport.Trim().ToLower();
            
            var airNormal = airport.Trim().ToUpper();

            var matchedAirports = _context
                .Airports
                .Where(a => a.AirportCode.Contains(airNormal) ||
                            a.City.Contains(airNormal) ||
                            a.Country.Contains(airNormal))
                .Distinct()
                .ToList();

            return matchedAirports;
        }
    }
}
