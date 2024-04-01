using FlightPlanner.Models;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.FindFlight
{
    public class SearchForFlightQuery(SearchFlights flightSearch) : IRequest<ServiceResult>
    {
        public SearchFlights FlightSearch { get; set; } = flightSearch;
    }
}
