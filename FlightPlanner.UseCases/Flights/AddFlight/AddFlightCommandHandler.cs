using System.Net;
using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using FluentValidation;
using MediatR;

namespace FlightPlanner.UseCases.Flights.AddFlight
{
    public class AddFlightCommandHandler(
        IFlightService flightService,
        IMapper mapper,
        IValidator<AddFlightRequest> validator)
        : IRequestHandler<AddFlightCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddFlightCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.AddFlightRequest, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ServiceResult
                {
                    ResultObject = validationResult.Errors,
                    Status = HttpStatusCode.BadRequest
                };
            }

            var flight = mapper.Map<Flight>(request.AddFlightRequest);

            if (flightService.IsDuplicate(flight))
            {
                return new ServiceResult
                {
                    ResultObject = validationResult.Errors,
                    Status = HttpStatusCode.Conflict
                };
            }

            if (!flightService.IsFlightValid(flight))
            {
                return new ServiceResult
                {
                    ResultObject = validationResult.Errors,
                    Status = HttpStatusCode.BadRequest
                };
            }

            flightService.Create(flight);

            return new ServiceResult
            {
                ResultObject = mapper.Map<AddFlightResponse>(flight),
                Status = HttpStatusCode.Created
            };
        }
    }
}
