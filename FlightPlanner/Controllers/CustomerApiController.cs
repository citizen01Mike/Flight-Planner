using FlightPlanner.Extensions;
using FlightPlanner.Models;
using FlightPlanner.UseCases.Airport;
using FlightPlanner.UseCases.Flights.FindFlight;
using FlightPlanner.UseCases.Flights.FlightById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public async Task<IActionResult> GetFlightById(int id)
        {
            return (await _mediator.Send(new FlightByIdQuery(id))).ToActionResult();
        }
       
        [HttpGet]
        [Route("airports")]
        
        public async Task<IActionResult> CustomerSearchAirport(string search)
        {
            return (await _mediator.Send(new SearchAirportQuery(search))).ToActionResult();
        }

        [HttpPost]
        [Route("flights/search")]
        public async Task<IActionResult> SearchForFlight(SearchFlights request)
        {
            return (await _mediator.Send(new SearchForFlightQuery(request))).ToActionResult();
        }
    }
}
