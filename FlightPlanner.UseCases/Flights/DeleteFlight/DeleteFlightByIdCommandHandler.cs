using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.DeleteFlight
{
    public class DeleteFlightByIdCommandHandler(IFlightService flightService)
        : IRequestHandler<DeleteFlightByIdCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteFlightByIdCommand request, CancellationToken cancellationToken)
        {
            var flight = flightService.GetFullFlightById(request.Id);

            if (flight != null)
            {
                flightService.Delete(flight);
            }

            return new ServiceResult(); 
        }
    }
}
