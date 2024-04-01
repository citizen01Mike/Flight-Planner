using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.DeleteFlight
{
    public class DeleteFlightByIdCommand(int id) : IRequest<ServiceResult>
    {
        public int Id { get; set; } = id;
    }
}
