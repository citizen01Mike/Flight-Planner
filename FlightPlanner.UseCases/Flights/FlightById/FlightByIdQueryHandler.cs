using System.Net;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.FlightById
{
    public class FlightByIdQueryHandler(IFlightService flightService, IMapper mapper)
        : IRequestHandler<FlightByIdQuery, ServiceResult>
    {
        public async Task<ServiceResult> Handle(FlightByIdQuery request, CancellationToken cancellationToken)
        {
            var flight = flightService.GetFullFlightById(request.Id);
            var response = new ServiceResult();

            if (flight == null)
            {
                return new ServiceResult
                {
                    Status = HttpStatusCode.NotFound
                };
            }
            
            response.ResultObject = mapper.Map<AddFlightResponse>(flight);
            response.Status = HttpStatusCode.OK;

            return response;
        }
    }
}
