using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.FlightById
{
    public class FlightByIdQuery(int id) : IRequest<ServiceResult>
    {
        public int Id { get; set; } = id;
    }
}
