using System.Net;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.GetFlight
{
    public class GetFlightQueryHandler(
        IFlightService flightService,
        IMapper mapper) : IRequestHandler<GetFlightQuery, ServiceResult>
    {
        public async Task<ServiceResult> Handle(
            GetFlightQuery request, 
            CancellationToken cancellationToken)
        {
            var flight = flightService.GetFullFlightById(request.Id);
            var response = new ServiceResult();

            if (flight == null)
            {
                response.Status = HttpStatusCode.NotFound;
                return response;
            }

            response.ResultObject = mapper.Map<AddFlightResponse>(flight);
            response.Status = HttpStatusCode.OK;
            
            return response;
        }
    }
}
