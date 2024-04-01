using FlightPlanner.Core.Models;
using FlightPlanner.Models;
using FlightPlanner.Storage;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight? GetFullFlightById(int id);
        public bool IsDuplicate(Flight flight);
        public bool IsFlightValid(Flight flight);
        public PageResult<Flight> SearchForFlight(SearchFlights request);
    }
}
