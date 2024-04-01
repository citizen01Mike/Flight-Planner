using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Airport
{
    public class SearchAirportQuery(string airportName) : IRequest<ServiceResult>
    {
        public string AirportName { get; set; } = airportName;
    }
}
