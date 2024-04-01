using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services;

public interface IAirportService
{
    public List<Airport> SearchForAirport(string airport);
}