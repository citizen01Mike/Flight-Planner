using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;
using System.Net;

namespace FlightPlanner.UseCases.Flights.FindFlight
{
    public class SearchForFlightQueryHandler(IFlightService flightService)
        : IRequestHandler<SearchForFlightQuery, ServiceResult>
    {
        public async Task<ServiceResult> Handle(SearchForFlightQuery search, CancellationToken cancellationToken)
        {
            if (search.FlightSearch.To == search.FlightSearch.From)
            {
                return new ServiceResult
                {
                    Status = HttpStatusCode.BadRequest
                };
            }
            
            var flight = flightService.SearchForFlight(search.FlightSearch); 
            
            return new ServiceResult
            {
                ResultObject = flightService.SearchForFlight(search.FlightSearch),
                Status = HttpStatusCode.OK
            };
        }
    }
}
